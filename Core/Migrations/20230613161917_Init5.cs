using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DMS.Data.Migrations
{
    /// <inheritdoc />
    public partial class Init5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Document_DocumentSource_SourceId",
                table: "Document");

            migrationBuilder.DropTable(
                name: "DocumentSource");

            migrationBuilder.DropIndex(
                name: "IX_Document_SourceId",
                table: "Document");

            migrationBuilder.DropColumn(
                name: "SourceId",
                table: "Document");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "SourceId",
                table: "Document",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "DocumentSource",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentSource", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Document_SourceId",
                table: "Document",
                column: "SourceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Document_DocumentSource_SourceId",
                table: "Document",
                column: "SourceId",
                principalTable: "DocumentSource",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
