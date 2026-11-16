
using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace appServicosCostura.Infrastructure.Migrations
{
    public partial class AdicionarTiposServicosEPecas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tipospecas",
                columns: table => new
                {
                    id = table.Column<Guid>(
                        type: "uuid",
                        nullable: false),
                    nome = table.Column<string>(
                        type: "character varying(100)",
                        maxLength: 100,
                        nullable: false),
                    ativo = table.Column<bool>(
                        type: "boolean",
                        nullable: false),
                    criado_em_utc = table.Column<DateTime>(
                        type: "timestamp with time zone",
                        nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tipospecas", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "tiposservicos",
                columns: table => new
                {
                    id = table.Column<Guid>(
                        type: "uuid",
                        nullable: false),
                    nome = table.Column<string>(
                        type: "character varying(100)",
                        maxLength: 100,
                        nullable: false),
                    ativo = table.Column<bool>(
                        type: "boolean",
                        nullable: false),
                    criado_em_utc = table.Column<DateTime>(
                        type: "timestamp with time zone",
                        nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tiposservicos", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tipospecas_nome",
                table: "tipospecas",
                column: "nome",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tiposservicos_nome",
                table: "tiposservicos",
                column: "nome",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "tipospecas");
            migrationBuilder.DropTable(name: "tiposservicos");
        }
    }
}
