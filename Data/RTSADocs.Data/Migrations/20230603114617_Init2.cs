using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RTSADocs.Data.Migrations
{
    /// <inheritdoc />
    public partial class Init2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Path",
                table: "Library");

            migrationBuilder.RenameColumn(
                name: "FileName",
                table: "PageFile",
                newName: "Path");

            migrationBuilder.RenameColumn(
                name: "FileFormat",
                table: "PageFile",
                newName: "Format");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Path",
                table: "PageFile",
                newName: "FileName");

            migrationBuilder.RenameColumn(
                name: "Format",
                table: "PageFile",
                newName: "FileFormat");

            migrationBuilder.AddColumn<string>(
                name: "Path",
                table: "Library",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
