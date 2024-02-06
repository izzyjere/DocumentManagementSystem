using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DMS.Data.Migrations
{
    /// <inheritdoc />
    public partial class Init7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "DocumentId",
                table: "DocumentArchive",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "DocumentArchive",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_DocumentArchive_DocumentId",
                table: "DocumentArchive",
                column: "DocumentId");

            migrationBuilder.AddForeignKey(
                name: "FK_DocumentArchive_Document_DocumentId",
                table: "DocumentArchive",
                column: "DocumentId",
                principalTable: "Document",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DocumentArchive_Document_DocumentId",
                table: "DocumentArchive");

            migrationBuilder.DropIndex(
                name: "IX_DocumentArchive_DocumentId",
                table: "DocumentArchive");

            migrationBuilder.DropColumn(
                name: "DocumentId",
                table: "DocumentArchive");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "DocumentArchive");
        }
    }
}
