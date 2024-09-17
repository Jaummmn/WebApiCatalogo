using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApiCurso.Migrations
{
    /// <inheritdoc />
    public partial class AjusteDbProdutos2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ProdutoNome",
                table: "Produtos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProdutoNome",
                table: "Produtos");
        }
    }
}
