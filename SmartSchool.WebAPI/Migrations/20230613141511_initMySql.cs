using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SmartSchool.WebAPI.Migrations
{
    public partial class initMySql : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Alunos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Matricula = table.Column<int>(nullable: false),
                    Nome = table.Column<string>(nullable: true),
                    Sobrenome = table.Column<string>(nullable: true),
                    Telefone = table.Column<string>(nullable: true),
                    DataNasc = table.Column<DateTime>(nullable: false),
                    DataIni = table.Column<DateTime>(nullable: false),
                    DataFim = table.Column<DateTime>(nullable: true),
                    Ativo = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Alunos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cursos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cursos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Professores",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Registro = table.Column<int>(nullable: false),
                    Nome = table.Column<string>(nullable: true),
                    Sobrenome = table.Column<string>(nullable: true),
                    Telefone = table.Column<string>(nullable: true),
                    DataIni = table.Column<DateTime>(nullable: false),
                    DataFim = table.Column<DateTime>(nullable: true),
                    Ativo = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Professores", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AlunosCursos",
                columns: table => new
                {
                    AlunoId = table.Column<int>(nullable: false),
                    CursoId = table.Column<int>(nullable: false),
                    DataIni = table.Column<DateTime>(nullable: false),
                    DataFim = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlunosCursos", x => new { x.AlunoId, x.CursoId });
                    table.ForeignKey(
                        name: "FK_AlunosCursos_Alunos_AlunoId",
                        column: x => x.AlunoId,
                        principalTable: "Alunos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AlunosCursos_Cursos_CursoId",
                        column: x => x.CursoId,
                        principalTable: "Cursos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Disciplinas",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(nullable: true),
                    CargaHoraria = table.Column<int>(nullable: false),
                    PrerequisitoId = table.Column<int>(nullable: true),
                    ProfessorId = table.Column<int>(nullable: false),
                    CursoId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Disciplinas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Disciplinas_Cursos_CursoId",
                        column: x => x.CursoId,
                        principalTable: "Cursos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Disciplinas_Disciplinas_PrerequisitoId",
                        column: x => x.PrerequisitoId,
                        principalTable: "Disciplinas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Disciplinas_Professores_ProfessorId",
                        column: x => x.ProfessorId,
                        principalTable: "Professores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AlunosDisciplinas",
                columns: table => new
                {
                    AlunoId = table.Column<int>(nullable: false),
                    DisciplinaId = table.Column<int>(nullable: false),
                    DataIni = table.Column<DateTime>(nullable: false),
                    DataFim = table.Column<DateTime>(nullable: true),
                    Nota = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlunosDisciplinas", x => new { x.AlunoId, x.DisciplinaId });
                    table.ForeignKey(
                        name: "FK_AlunosDisciplinas_Alunos_AlunoId",
                        column: x => x.AlunoId,
                        principalTable: "Alunos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AlunosDisciplinas_Disciplinas_DisciplinaId",
                        column: x => x.DisciplinaId,
                        principalTable: "Disciplinas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Alunos",
                columns: new[] { "Id", "Ativo", "DataFim", "DataIni", "DataNasc", "Matricula", "Nome", "Sobrenome", "Telefone" },
                values: new object[,]
                {
                    { 1, true, null, new DateTime(2023, 6, 13, 11, 15, 10, 766, DateTimeKind.Local).AddTicks(2560), new DateTime(2002, 5, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Marta", "Kent", "47992665812" },
                    { 2, true, null, new DateTime(2023, 6, 13, 11, 15, 10, 766, DateTimeKind.Local).AddTicks(5476), new DateTime(2003, 9, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, "Paula", "Costa", "47993651177" },
                    { 3, true, null, new DateTime(2023, 6, 13, 11, 15, 10, 766, DateTimeKind.Local).AddTicks(5540), new DateTime(2001, 5, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, "Laura", "Antônia", "47984036767" },
                    { 4, true, null, new DateTime(2023, 6, 13, 11, 15, 10, 766, DateTimeKind.Local).AddTicks(5549), new DateTime(2002, 10, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, "Julia", "Fernandes", "47991058946" },
                    { 5, true, null, new DateTime(2023, 6, 13, 11, 15, 10, 766, DateTimeKind.Local).AddTicks(5558), new DateTime(2000, 10, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, "Lucas", "Prim", "47999633156" },
                    { 6, true, null, new DateTime(2023, 6, 13, 11, 15, 10, 766, DateTimeKind.Local).AddTicks(5570), new DateTime(2003, 2, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 6, "Paulo", "Alvarez", "47991699244" },
                    { 7, true, null, new DateTime(2023, 6, 13, 11, 15, 10, 766, DateTimeKind.Local).AddTicks(5577), new DateTime(2002, 5, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), 7, "Henrique", "Fernandes", "47984391966" }
                });

            migrationBuilder.InsertData(
                table: "Cursos",
                columns: new[] { "Id", "Nome" },
                values: new object[,]
                {
                    { 1, "Tecnologia da Informação" },
                    { 2, "Sistemas de Informação" },
                    { 3, "Ciência da Computação" }
                });

            migrationBuilder.InsertData(
                table: "Professores",
                columns: new[] { "Id", "Ativo", "DataFim", "DataIni", "Nome", "Registro", "Sobrenome", "Telefone" },
                values: new object[,]
                {
                    { 1, true, null, new DateTime(2023, 6, 13, 11, 15, 10, 760, DateTimeKind.Local).AddTicks(2721), "Lauro", 1, "Oliveira", "47984035277" },
                    { 2, true, null, new DateTime(2023, 6, 13, 11, 15, 10, 761, DateTimeKind.Local).AddTicks(3004), "Roberto", 2, "Soares", "4799331208" },
                    { 3, true, null, new DateTime(2023, 6, 13, 11, 15, 10, 761, DateTimeKind.Local).AddTicks(3061), "Ronaldo", 3, "Marconi", "4792551499" },
                    { 4, true, null, new DateTime(2023, 6, 13, 11, 15, 10, 761, DateTimeKind.Local).AddTicks(3064), "Rodrigo", 4, "Carvalho", "47988203361" },
                    { 5, true, null, new DateTime(2023, 6, 13, 11, 15, 10, 761, DateTimeKind.Local).AddTicks(3073), "Alexandre", 5, "Montanha", "47991289255" }
                });

            migrationBuilder.InsertData(
                table: "Disciplinas",
                columns: new[] { "Id", "CargaHoraria", "CursoId", "Nome", "PrerequisitoId", "ProfessorId" },
                values: new object[,]
                {
                    { 1, 0, 1, "Matemática", null, 1 },
                    { 2, 0, 3, "Matemática", null, 1 },
                    { 3, 0, 3, "Física", null, 2 },
                    { 4, 0, 1, "Português", null, 3 },
                    { 5, 0, 1, "Inglês", null, 4 },
                    { 6, 0, 2, "Inglês", null, 4 },
                    { 7, 0, 3, "Inglês", null, 4 },
                    { 8, 0, 1, "Programação", null, 5 },
                    { 9, 0, 2, "Programação", null, 5 },
                    { 10, 0, 3, "Programação", null, 5 }
                });

            migrationBuilder.InsertData(
                table: "AlunosDisciplinas",
                columns: new[] { "AlunoId", "DisciplinaId", "DataFim", "DataIni", "Nota" },
                values: new object[,]
                {
                    { 2, 1, null, new DateTime(2023, 6, 13, 11, 15, 10, 766, DateTimeKind.Local).AddTicks(8014), null },
                    { 4, 5, null, new DateTime(2023, 6, 13, 11, 15, 10, 766, DateTimeKind.Local).AddTicks(8031), null },
                    { 2, 5, null, new DateTime(2023, 6, 13, 11, 15, 10, 766, DateTimeKind.Local).AddTicks(8020), null },
                    { 1, 5, null, new DateTime(2023, 6, 13, 11, 15, 10, 766, DateTimeKind.Local).AddTicks(8011), null },
                    { 7, 4, null, new DateTime(2023, 6, 13, 11, 15, 10, 766, DateTimeKind.Local).AddTicks(8116), null },
                    { 6, 4, null, new DateTime(2023, 6, 13, 11, 15, 10, 766, DateTimeKind.Local).AddTicks(8109), null },
                    { 5, 4, null, new DateTime(2023, 6, 13, 11, 15, 10, 766, DateTimeKind.Local).AddTicks(8032), null },
                    { 4, 4, null, new DateTime(2023, 6, 13, 11, 15, 10, 766, DateTimeKind.Local).AddTicks(8029), null },
                    { 1, 4, null, new DateTime(2023, 6, 13, 11, 15, 10, 766, DateTimeKind.Local).AddTicks(7983), null },
                    { 7, 3, null, new DateTime(2023, 6, 13, 11, 15, 10, 766, DateTimeKind.Local).AddTicks(8114), null },
                    { 5, 5, null, new DateTime(2023, 6, 13, 11, 15, 10, 766, DateTimeKind.Local).AddTicks(8034), null },
                    { 6, 3, null, new DateTime(2023, 6, 13, 11, 15, 10, 766, DateTimeKind.Local).AddTicks(8104), null },
                    { 7, 2, null, new DateTime(2023, 6, 13, 11, 15, 10, 766, DateTimeKind.Local).AddTicks(8113), null },
                    { 6, 2, null, new DateTime(2023, 6, 13, 11, 15, 10, 766, DateTimeKind.Local).AddTicks(8037), null },
                    { 3, 2, null, new DateTime(2023, 6, 13, 11, 15, 10, 766, DateTimeKind.Local).AddTicks(8023), null },
                    { 2, 2, null, new DateTime(2023, 6, 13, 11, 15, 10, 766, DateTimeKind.Local).AddTicks(8015), null },
                    { 1, 2, null, new DateTime(2023, 6, 13, 11, 15, 10, 766, DateTimeKind.Local).AddTicks(7098), null },
                    { 7, 1, null, new DateTime(2023, 6, 13, 11, 15, 10, 766, DateTimeKind.Local).AddTicks(8111), null },
                    { 6, 1, null, new DateTime(2023, 6, 13, 11, 15, 10, 766, DateTimeKind.Local).AddTicks(8035), null },
                    { 4, 1, null, new DateTime(2023, 6, 13, 11, 15, 10, 766, DateTimeKind.Local).AddTicks(8028), null },
                    { 3, 1, null, new DateTime(2023, 6, 13, 11, 15, 10, 766, DateTimeKind.Local).AddTicks(8022), null },
                    { 3, 3, null, new DateTime(2023, 6, 13, 11, 15, 10, 766, DateTimeKind.Local).AddTicks(8025), null },
                    { 7, 5, null, new DateTime(2023, 6, 13, 11, 15, 10, 766, DateTimeKind.Local).AddTicks(8117), null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AlunosCursos_CursoId",
                table: "AlunosCursos",
                column: "CursoId");

            migrationBuilder.CreateIndex(
                name: "IX_AlunosDisciplinas_DisciplinaId",
                table: "AlunosDisciplinas",
                column: "DisciplinaId");

            migrationBuilder.CreateIndex(
                name: "IX_Disciplinas_CursoId",
                table: "Disciplinas",
                column: "CursoId");

            migrationBuilder.CreateIndex(
                name: "IX_Disciplinas_PrerequisitoId",
                table: "Disciplinas",
                column: "PrerequisitoId");

            migrationBuilder.CreateIndex(
                name: "IX_Disciplinas_ProfessorId",
                table: "Disciplinas",
                column: "ProfessorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AlunosCursos");

            migrationBuilder.DropTable(
                name: "AlunosDisciplinas");

            migrationBuilder.DropTable(
                name: "Alunos");

            migrationBuilder.DropTable(
                name: "Disciplinas");

            migrationBuilder.DropTable(
                name: "Cursos");

            migrationBuilder.DropTable(
                name: "Professores");
        }
    }
}
