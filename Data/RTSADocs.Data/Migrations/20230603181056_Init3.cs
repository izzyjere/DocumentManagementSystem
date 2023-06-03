using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RTSADocs.Data.Migrations
{
    /// <inheritdoc />
    public partial class Init3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Path",
                table: "FileStore",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "FileStore",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "FileStore",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedBy",
                table: "FileStore",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModifiedOn",
                table: "FileStore",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "FileStore");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "FileStore");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                table: "FileStore");

            migrationBuilder.DropColumn(
                name: "LastModifiedOn",
                table: "FileStore");

            migrationBuilder.AlterColumn<string>(
                name: "Path",
                table: "FileStore",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
