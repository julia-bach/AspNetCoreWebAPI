using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SmartSchool.WebAPI.Helpers;
using SmartSchool.WebAPI.Models;

namespace SmartSchool.WebAPI.Data
{
    public class Repository : IRepository
    {
        private readonly SmartContext _context;
        public Repository(SmartContext context)
        {
            _context = context;
        }
        
        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }
        
        public void Update<T>(T entity) where T : class
        {
            _context.Update(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() > 0);
        }

        // Alunos
        public async Task<PageList<Aluno>> GetAllAlunosAsync(PageParams pageParams, bool includeProfessor = false)
        {
            IQueryable<Aluno> query = _context.Alunos;
            if (includeProfessor)
            {
                query = query.Include(a => a.AlunosDisciplinas).ThenInclude(ad => ad.Disciplina).ThenInclude(d => d.Professor);
            }
            query = query.AsNoTracking().OrderBy(a => a.Id);
            if(!String.IsNullOrEmpty(pageParams.Nome))
            {
                query = query.Where(aluno => aluno.Nome.ToUpper().Contains(pageParams.Nome.ToUpper()) 
                || aluno.Sobrenome.ToUpper().Contains(pageParams.Nome.ToUpper()));
            }
            if(pageParams.Matricula > 0)
            {
                query = query.Where(aluno => aluno.Matricula == pageParams.Matricula);
            }
            if(pageParams.Ativo != null)
            {
                query = query.Where(aluno => aluno.Ativo == (pageParams.Ativo != 0));
            }
            // return await query.ToListAsync();
            return await PageList<Aluno>.CreateAsync(query, pageParams.PageNumber, pageParams.PageSize);
        }

        public Aluno[] GetAllAlunos(bool includeProfessor = false)
        {
            IQueryable<Aluno> query = _context.Alunos;
            if (includeProfessor)
            {
                query = query.Include(a => a.AlunosDisciplinas).ThenInclude(ad => ad.Disciplina).ThenInclude(d => d.Professor);
            }
            query = query.AsNoTracking().OrderBy(a => a.Id);
            return query.ToArray();
        }

        public Aluno[] GetAllAlunosByDisciplinaId(int disciplinaId, bool includeProfessor = false)
        {
            IQueryable<Aluno> query = _context.Alunos;
            if (includeProfessor)
            {
                query = query.Include(a => a.AlunosDisciplinas).ThenInclude(ad => ad.Disciplina).ThenInclude(d => d.Professor);
            }
            query = query.AsNoTracking().OrderBy(a => a.Id).Where(aluno => aluno.AlunosDisciplinas.Any(ad => ad.DisciplinaId == disciplinaId));
            return query.ToArray();
        }

        public Aluno GetAlunoById(int alunoId, bool includeProfessor = false)
        {
            IQueryable<Aluno> query = _context.Alunos;
            if (includeProfessor)
            {
                query = query.Include(a => a.AlunosDisciplinas).ThenInclude(ad => ad.Disciplina).ThenInclude(d => d.Professor);
            }
            query = query.AsNoTracking().OrderBy(a => a.Id).Where(aluno => aluno.Id == alunoId);
            return query.FirstOrDefault();
        }

        // Professores
        public async Task<PageList<Professor>> GetAllProfessoresAsync(PageParams pageParams, bool includeAlunos = false)
        {
            IQueryable<Professor> query = _context.Professores;
            if (includeAlunos)
            {
                query = query.Include(p => p.Discliplinas).ThenInclude(d => d.AlunosDisciplinas).ThenInclude(ad => ad.Aluno);
            }
            query = query.AsNoTracking().OrderBy(p => p.Id);
            if(!String.IsNullOrEmpty(pageParams.Nome))
            {
                query = query.Where(professor => professor.Nome.ToUpper().Contains(pageParams.Nome.ToUpper()) 
                || professor.Sobrenome.ToUpper().Contains(pageParams.Nome.ToUpper()));
            }
            if(pageParams.Registro > 0)
            {
                query = query.Where(professor => professor.Registro == pageParams.Registro);
            }
            if(pageParams.Ativo != null)
            {
                query = query.Where(professor => professor.Ativo == (pageParams.Ativo != 0));
            }
            // return query.ToArray();
            return await PageList<Professor>.CreateAsync(query, 2, 5);
        }

        public Professor[] GetAllProfessores(bool includeAlunos = false)
        {
            IQueryable<Professor> query = _context.Professores;
            if (includeAlunos)
            {
                query = query.Include(p => p.Discliplinas).ThenInclude(d => d.AlunosDisciplinas).ThenInclude(ad => ad.Aluno);
            }
            query = query.AsNoTracking().OrderBy(p => p.Id);
            return query.ToArray();
        }

        public Professor[] GetAllProfessoresByDisciplinaId(int disciplinaId, bool includeAlunos = false)
        {
            IQueryable<Professor> query = _context.Professores;
            if (includeAlunos)
            {
                query = query.Include(p => p.Discliplinas).ThenInclude(d => d.AlunosDisciplinas).ThenInclude(ad => ad.Aluno);
            }
            query = query.AsNoTracking().OrderBy(aluno => aluno.Id).Where(
                aluno => aluno.Discliplinas.Any(d => d.AlunosDisciplinas.Any(ad => ad.DisciplinaId == disciplinaId))
            );
            return query.ToArray();

        }

        public Professor GetProfessorById(int professorId, bool includeProfessor = false)
        {
            IQueryable<Professor> query = _context.Professores;
            if (includeProfessor)
            {
                query = query.Include(p => p.Discliplinas).ThenInclude(d => d.AlunosDisciplinas).ThenInclude(ad => ad.Aluno);
            }
            query = query.AsNoTracking().OrderBy(a => a.Id).Where(professor => professor.Id == professorId);
            return query.FirstOrDefault();
        }
    }
}