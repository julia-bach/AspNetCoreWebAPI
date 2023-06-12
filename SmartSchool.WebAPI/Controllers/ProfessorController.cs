using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartSchool.WebAPI.Data;
using SmartSchool.WebAPI.Dtos;
using SmartSchool.WebAPI.Helpers;
using SmartSchool.WebAPI.Models;

namespace SmartSchool.WebAPI.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/{version:apiVersion}/[controller]")]
    public class ProfessorController : ControllerBase
    {
        private readonly IRepository _repo;
        public readonly IMapper _mapper;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="repo"></param>
        /// <param name="mapper"></param>
        public ProfessorController(IRepository repo, IMapper mapper)
        {
            _mapper = mapper;
            _repo = repo;
        }


        /// <summary>
        /// Método responsável por retornar todos os professores.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery]PageParams pageParams)
        {
            var professores = await _repo.GetAllProfessoresAsync(pageParams, true);
            var professoresResult = _mapper.Map<IEnumerable<ProfessorDto>>(professores);
            Response.AddPagination(professores.CurrentPage, professores.PageSize, professores.TotalCount, professores.TotalPages);
            return Ok(professoresResult);
        }

        /// <summary>
        /// Método responsável por retornar professor por meio de um Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var professor = _repo.GetProfessorById(id);
            if (professor == null) return BadRequest("Professor not found.");
            var professorDto = _mapper.Map<ProfessorDto>(professor);
            return Ok(professorDto);
        }

        /// <summary>
        /// Método responsável por criar professor.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Post(ProfessorRegistrarDto model)
        {   
            var professor = _mapper.Map<Professor>(model);
            _repo.Add(professor);
            if (_repo.SaveChanges())
            {
                return Created($"/api/aluno/{model.Id}", _mapper.Map<ProfessorDto>(professor));
            }
            return BadRequest("An error ocurred trying to create Professor.");
        }

        /// <summary>
        /// Método responsável por editar professor por meio de um Id.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public IActionResult Put(int id, ProfessorRegistrarDto model)
        {
            var professor = _repo.GetProfessorById(id);
            if (professor == null) return BadRequest("Professor not found.");
            _mapper.Map(model, professor);
            _repo.Update(professor);
            if (_repo.SaveChanges())
            {
                return Created($"/api/aluno/{model.Id}", _mapper.Map<ProfessorDto>(professor));
            }
            return BadRequest("An error ocurred trying to update Professor.");
        }

        /// <summary>
        /// Método responsável por editar parcialmente um professor por meio de um Id.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPatch("{id}")]
        public IActionResult Patch(int id, ProfessorRegistrarDto model)
        {
            var professor = _repo.GetProfessorById(id);
            if (professor == null) return BadRequest("Professor not found.");
            _mapper.Map(model, professor);
            _repo.Update(professor);
            if (_repo.SaveChanges())
            {
                return Created($"/api/aluno/{model.Id}", _mapper.Map<ProfessorDto>(professor));
            }
            return BadRequest("An error ocurred trying to update Professor.");
        }

        /// <summary>
        /// Método responsável por deletar professor por meio de um Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var professor = _repo.GetProfessorById(id);
            if (professor == null) return BadRequest("You are trying to delete a Professor that does not exist.");
            _repo.Delete(professor);
            if (_repo.SaveChanges())
            {
                return Ok("Professor deleted.");
            }
            return BadRequest("An error ocurred trying to delete Professor.");
        } 
    }
}
