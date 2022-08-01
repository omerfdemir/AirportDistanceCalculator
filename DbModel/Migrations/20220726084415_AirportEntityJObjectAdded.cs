using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DbModel.Migrations
{
    public partial class AirportEntityJObjectAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AirportsJObject",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IATACode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    JSON = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AirportsJObject", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AirportsJObject");
        }
    }
}
