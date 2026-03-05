using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DocumentManagement.Domain.Migrations
{
    /// <inheritdoc />
    public partial class Kepegawaianv02 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Gelar",
                table: "Dosens");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Dosens",
                newName: "Id");

            migrationBuilder.AddColumn<string>(
                name: "Gelar",
                table: "Pegawais",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "TMT",
                table: "DosenTetaps",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Gelar",
                table: "Pegawais");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Dosens",
                newName: "ID");

            migrationBuilder.AlterColumn<DateTime>(
                name: "TMT",
                table: "DosenTetaps",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<string>(
                name: "Gelar",
                table: "Dosens",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
