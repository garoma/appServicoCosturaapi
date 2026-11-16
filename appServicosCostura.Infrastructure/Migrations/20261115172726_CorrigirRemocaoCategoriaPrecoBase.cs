using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace appServicosCostura.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CorrigirRemocaoCategoriaPrecoBase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "categoria",
                table: "Profissionais");

            migrationBuilder.DropColumn(
                name: "preco_base",
                table: "Profissionais");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "categoria",
                table: "Profissionais",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "preco_base",
                table: "Profissionais",
                type: "numeric",
                nullable: true);
        }
    }
}
