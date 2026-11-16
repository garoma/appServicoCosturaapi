using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace appServicosCostura.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AdicionarFotoClienteProfissional : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FotoUrl",
                table: "Profissionais",
                newName: "FotoContentType");

            migrationBuilder.RenameColumn(
                name: "FotoUrl",
                table: "Clientes",
                newName: "FotoContentType");

            migrationBuilder.AddColumn<byte[]>(
                name: "Foto",
                table: "Profissionais",
                type: "bytea",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "Foto",
                table: "Clientes",
                type: "bytea",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Foto",
                table: "Profissionais");

            migrationBuilder.DropColumn(
                name: "Foto",
                table: "Clientes");

            migrationBuilder.RenameColumn(
                name: "FotoContentType",
                table: "Profissionais",
                newName: "FotoUrl");

            migrationBuilder.RenameColumn(
                name: "FotoContentType",
                table: "Clientes",
                newName: "FotoUrl");
        }
    }
}
