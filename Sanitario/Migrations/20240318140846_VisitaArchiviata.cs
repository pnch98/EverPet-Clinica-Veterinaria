using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sanitario.Migrations
{
    /// <inheritdoc />
    public partial class VisitaArchiviata : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsArchiviato",
                table: "Visite",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsArchiviato",
                table: "Visite");
        }
    }
}
