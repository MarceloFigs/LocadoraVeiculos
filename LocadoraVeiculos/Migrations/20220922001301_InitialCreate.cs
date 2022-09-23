using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LocadoraVeiculos.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categoria",
                columns: table => new
                {
                    CodCategoria = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descrição = table.Column<string>(nullable: true),
                    ValorDiaria = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categoria", x => x.CodCategoria);
                });

            migrationBuilder.CreateTable(
                name: "Cliente",
                columns: table => new
                {
                    Cpf = table.Column<string>(nullable: false),
                    RG = table.Column<string>(nullable: true),
                    DtNascimento = table.Column<DateTime>(nullable: false),
                    Cnh = table.Column<string>(nullable: true),
                    Nome = table.Column<string>(nullable: true),
                    Endereço = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cliente", x => x.Cpf);
                });

            migrationBuilder.CreateTable(
                name: "Carro",
                columns: table => new
                {
                    Chassi = table.Column<string>(nullable: false),
                    Cor = table.Column<string>(nullable: true),
                    Modelo = table.Column<string>(nullable: true),
                    Marca = table.Column<string>(nullable: true),
                    Placa = table.Column<string>(nullable: true),
                    Ano = table.Column<int>(nullable: false),
                    CodCategoria = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carro", x => x.Chassi);
                    table.ForeignKey(
                        name: "FK_Carro_Categoria_CodCategoria",
                        column: x => x.CodCategoria,
                        principalTable: "Categoria",
                        principalColumn: "CodCategoria",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Alocação",
                columns: table => new
                {
                    Cpf = table.Column<string>(nullable: false),
                    Chassi = table.Column<string>(nullable: false),
                    HrSaida = table.Column<DateTime>(nullable: false),
                    DtSaida = table.Column<DateTime>(nullable: false),
                    HrEntrega = table.Column<DateTime>(nullable: false),
                    DtEntrega = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Alocação", x => new { x.Cpf, x.Chassi });
                    table.ForeignKey(
                        name: "FK_Alocação_Carro_Chassi",
                        column: x => x.Chassi,
                        principalTable: "Carro",
                        principalColumn: "Chassi",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Alocação_Cliente_Cpf",
                        column: x => x.Cpf,
                        principalTable: "Cliente",
                        principalColumn: "Cpf",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Alocação_Chassi",
                table: "Alocação",
                column: "Chassi");

            migrationBuilder.CreateIndex(
                name: "IX_Carro_CodCategoria",
                table: "Carro",
                column: "CodCategoria",
                unique: true,
                filter: "[CodCategoria] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Alocação");

            migrationBuilder.DropTable(
                name: "Carro");

            migrationBuilder.DropTable(
                name: "Cliente");

            migrationBuilder.DropTable(
                name: "Categoria");
        }
    }
}
