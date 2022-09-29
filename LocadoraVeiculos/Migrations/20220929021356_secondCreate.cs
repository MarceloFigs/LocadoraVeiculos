using Microsoft.EntityFrameworkCore.Migrations;

namespace LocadoraVeiculos.Migrations
{
    public partial class secondCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Carro_Categoria_CodCategoria",
                table: "Carro");

            migrationBuilder.DropIndex(
                name: "IX_Carro_CodCategoria",
                table: "Carro");

            migrationBuilder.AlterColumn<int>(
                name: "CodCategoria",
                table: "Carro",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Carro_CodCategoria",
                table: "Carro",
                column: "CodCategoria",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Carro_Categoria_CodCategoria",
                table: "Carro",
                column: "CodCategoria",
                principalTable: "Categoria",
                principalColumn: "CodCategoria",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Carro_Categoria_CodCategoria",
                table: "Carro");

            migrationBuilder.DropIndex(
                name: "IX_Carro_CodCategoria",
                table: "Carro");

            migrationBuilder.AlterColumn<int>(
                name: "CodCategoria",
                table: "Carro",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.CreateIndex(
                name: "IX_Carro_CodCategoria",
                table: "Carro",
                column: "CodCategoria",
                unique: true,
                filter: "[CodCategoria] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Carro_Categoria_CodCategoria",
                table: "Carro",
                column: "CodCategoria",
                principalTable: "Categoria",
                principalColumn: "CodCategoria",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
