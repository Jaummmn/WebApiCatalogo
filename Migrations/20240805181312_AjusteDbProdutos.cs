using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApiCurso.Migrations
{
    /// <inheritdoc />
    public partial class AjusteDbProdutos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "EmEstoque",
                table: "Produtos",
                type: "bit",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<float>(
                name: "EmEstoque",
                table: "Produtos",
                type: "real",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit");
        }
    }
}
