using Microsoft.EntityFrameworkCore.Migrations;
using System.Diagnostics.CodeAnalysis;

#nullable disable

namespace WebApiCurso.Migrations;

/// <inheritdoc />
public partial class popularTabela : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.InsertData(
            table: "Produtos",
            columns: new[] { "PrecoProduto", "EmEstoque", "DataCadastro", "CategoriaId", "ProdutoNome" },
            values: new object[,]
            {
                { 23.0m, true, DateTime.Now, 1, "LancheMac" },
                { 23.0m, true, DateTime.Now, 1, "LancheMac Super" },
                { 12.0m, true, DateTime.Now, 1, "LancheMac Mega" },
                { 4.0m, true, DateTime.Now, 2, "Coca Cola" },
                { 5.0m, true, DateTime.Now, 2, "Guarana" },
                { 19.0m, true, DateTime.Now, 3, "Pudim" },
            });
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        // Assumindo que ProdutoNome é único para remoção
        migrationBuilder.DeleteData(
            table: "Produtos",
            keyColumn: "ProdutoNome",
            keyValue: "LancheMac"
        );
    }
}