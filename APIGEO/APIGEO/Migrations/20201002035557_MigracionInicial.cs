using Microsoft.EntityFrameworkCore.Migrations;

namespace APIGEO.Migrations
{
    public partial class MigracionInicial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Puntos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Calle = table.Column<string>(nullable: true),
                    Numero = table.Column<string>(nullable: true),
                    Ciudad = table.Column<string>(nullable: true),
                    Codigo_postal = table.Column<string>(nullable: true),
                    Provincia = table.Column<string>(nullable: true),
                    Pais = table.Column<string>(nullable: true),
                    Latitud = table.Column<decimal>(nullable: false),
                    Longitud = table.Column<decimal>(nullable: false),
                    Geolocalizado = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Puntos", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Puntos");
        }
    }
}
