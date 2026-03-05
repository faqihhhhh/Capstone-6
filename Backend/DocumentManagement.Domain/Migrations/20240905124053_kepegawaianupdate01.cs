using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DocumentManagement.Domain.Migrations
{
    /// <inheritdoc />
    public partial class kepegawaianupdate01 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Dosens_Pegawais_NIP",
                table: "Dosens");

            migrationBuilder.DropForeignKey(
                name: "FK_Tendiks_Pegawais_NIP",
                table: "Tendiks");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "TendikTetaps");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "TendikTetaps");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "TendikTetaps");

            migrationBuilder.DropColumn(
                name: "DeletedDate",
                table: "TendikTetaps");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "TendikTetaps");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "TendikTetaps");

            migrationBuilder.DropColumn(
                name: "ModifiedDate",
                table: "TendikTetaps");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Tendiks");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Tendiks");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "Tendiks");

            migrationBuilder.DropColumn(
                name: "DeletedDate",
                table: "Tendiks");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Tendiks");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "Tendiks");

            migrationBuilder.DropColumn(
                name: "ModifiedDate",
                table: "Tendiks");

            migrationBuilder.AlterColumn<string>(
                name: "Jenis_Tendik",
                table: "Tendiks",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "TendikTetapNIP",
                table: "Tendiks",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Tempat_Lahir",
                table: "Pegawais",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Telepon_Darurat",
                table: "Pegawais",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Status_Pernikahan",
                table: "Pegawais",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Pendidikan_Terakhir",
                table: "Pegawais",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Nama",
                table: "Pegawais",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Jenis_Pegawai",
                table: "Pegawais",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Jenis_Kelamin",
                table: "Pegawais",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<DateTime>(
                name: "TMT",
                table: "DosenTetaps",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<string>(
                name: "Jabatan_Akademik",
                table: "DosenTetaps",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Jenis_Dosen",
                table: "Dosens",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "DosenTetapNIP",
                table: "Dosens",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tendiks_TendikTetapNIP",
                table: "Tendiks",
                column: "TendikTetapNIP");

            migrationBuilder.CreateIndex(
                name: "IX_Dosens_DosenTetapNIP",
                table: "Dosens",
                column: "DosenTetapNIP");

            migrationBuilder.AddForeignKey(
                name: "FK_Dosens_DosenTetaps_DosenTetapNIP",
                table: "Dosens",
                column: "DosenTetapNIP",
                principalTable: "DosenTetaps",
                principalColumn: "NIP");

            migrationBuilder.AddForeignKey(
                name: "FK_Dosens_Pegawais_NIP",
                table: "Dosens",
                column: "NIP",
                principalTable: "Pegawais",
                principalColumn: "NIP",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tendiks_Pegawais_NIP",
                table: "Tendiks",
                column: "NIP",
                principalTable: "Pegawais",
                principalColumn: "NIP",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tendiks_TendikTetaps_TendikTetapNIP",
                table: "Tendiks",
                column: "TendikTetapNIP",
                principalTable: "TendikTetaps",
                principalColumn: "NIP");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Dosens_DosenTetaps_DosenTetapNIP",
                table: "Dosens");

            migrationBuilder.DropForeignKey(
                name: "FK_Dosens_Pegawais_NIP",
                table: "Dosens");

            migrationBuilder.DropForeignKey(
                name: "FK_Tendiks_Pegawais_NIP",
                table: "Tendiks");

            migrationBuilder.DropForeignKey(
                name: "FK_Tendiks_TendikTetaps_TendikTetapNIP",
                table: "Tendiks");

            migrationBuilder.DropIndex(
                name: "IX_Tendiks_TendikTetapNIP",
                table: "Tendiks");

            migrationBuilder.DropIndex(
                name: "IX_Dosens_DosenTetapNIP",
                table: "Dosens");

            migrationBuilder.DropColumn(
                name: "TendikTetapNIP",
                table: "Tendiks");

            migrationBuilder.DropColumn(
                name: "DosenTetapNIP",
                table: "Dosens");

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedBy",
                table: "TendikTetaps",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "TendikTetaps",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "DeletedBy",
                table: "TendikTetaps",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedDate",
                table: "TendikTetaps",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "TendikTetaps",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<Guid>(
                name: "ModifiedBy",
                table: "TendikTetaps",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDate",
                table: "TendikTetaps",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<int>(
                name: "Jenis_Tendik",
                table: "Tendiks",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedBy",
                table: "Tendiks",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "Tendiks",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "DeletedBy",
                table: "Tendiks",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedDate",
                table: "Tendiks",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Tendiks",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<Guid>(
                name: "ModifiedBy",
                table: "Tendiks",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDate",
                table: "Tendiks",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<string>(
                name: "Tempat_Lahir",
                table: "Pegawais",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Telepon_Darurat",
                table: "Pegawais",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "Status_Pernikahan",
                table: "Pegawais",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "Pendidikan_Terakhir",
                table: "Pegawais",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Nama",
                table: "Pegawais",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "Jenis_Pegawai",
                table: "Pegawais",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "Jenis_Kelamin",
                table: "Pegawais",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "TMT",
                table: "DosenTetaps",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Jabatan_Akademik",
                table: "DosenTetaps",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Jenis_Dosen",
                table: "Dosens",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddForeignKey(
                name: "FK_Dosens_Pegawais_NIP",
                table: "Dosens",
                column: "NIP",
                principalTable: "Pegawais",
                principalColumn: "NIP",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Tendiks_Pegawais_NIP",
                table: "Tendiks",
                column: "NIP",
                principalTable: "Pegawais",
                principalColumn: "NIP",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
