using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DocumentManagement.Domain.Migrations
{
    /// <inheritdoc />
    public partial class Kepegawain01 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Dosens_DosenTetaps_DosenTetapNIP",
                table: "Dosens");

            migrationBuilder.DropForeignKey(
                name: "FK_Dosens_Pegawais_NIP",
                table: "Dosens");

            migrationBuilder.DropForeignKey(
                name: "FK_DosenTetaps_Dosens_NIP",
                table: "DosenTetaps");

            migrationBuilder.DropForeignKey(
                name: "FK_Tendiks_Pegawais_NIP",
                table: "Tendiks");

            migrationBuilder.DropForeignKey(
                name: "FK_Tendiks_TendikTetaps_TendikTetapNIP",
                table: "Tendiks");

            migrationBuilder.DropForeignKey(
                name: "FK_TendikTetaps_Tendiks_NIP",
                table: "TendikTetaps");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TendikTetaps",
                table: "TendikTetaps");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tendiks",
                table: "Tendiks");

            migrationBuilder.DropIndex(
                name: "IX_Tendiks_TendikTetapNIP",
                table: "Tendiks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Pegawais",
                table: "Pegawais");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DosenTetaps",
                table: "DosenTetaps");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Dosens",
                table: "Dosens");

            migrationBuilder.DropIndex(
                name: "IX_Dosens_DosenTetapNIP",
                table: "Dosens");

            migrationBuilder.DropColumn(
                name: "NIP",
                table: "TendikTetaps");

            migrationBuilder.DropColumn(
                name: "NIP",
                table: "Tendiks");

            migrationBuilder.DropColumn(
                name: "TendikTetapNIP",
                table: "Tendiks");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Pegawais");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Pegawais");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "Pegawais");

            migrationBuilder.DropColumn(
                name: "DeletedDate",
                table: "Pegawais");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Pegawais");

            migrationBuilder.DropColumn(
                name: "ModifiedDate",
                table: "Pegawais");

            migrationBuilder.DropColumn(
                name: "NIP",
                table: "DosenTetaps");

            migrationBuilder.DropColumn(
                name: "NIP",
                table: "Dosens");

            migrationBuilder.DropColumn(
                name: "DosenTetapNIP",
                table: "Dosens");

            migrationBuilder.RenameColumn(
                name: "ModifiedBy",
                table: "Pegawais",
                newName: "Id");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "TendikTetaps",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "TMT",
                table: "TendikTetaps",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "TendikId",
                table: "TendikTetaps",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "Tendiks",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "PegawaiId",
                table: "Tendiks",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

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

            migrationBuilder.AlterColumn<string>(
                name: "Nama",
                table: "Pegawais",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "NIP",
                table: "Pegawais",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "Jabatan_Akademik",
                table: "DosenTetaps",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "DosenTetaps",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "DosenId",
                table: "DosenTetaps",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "ID",
                table: "Dosens",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "PegawaiId",
                table: "Dosens",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_TendikTetaps",
                table: "TendikTetaps",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tendiks",
                table: "Tendiks",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Pegawais",
                table: "Pegawais",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DosenTetaps",
                table: "DosenTetaps",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Dosens",
                table: "Dosens",
                column: "ID");

            migrationBuilder.CreateIndex(
                name: "IX_TendikTetaps_TendikId",
                table: "TendikTetaps",
                column: "TendikId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tendiks_PegawaiId",
                table: "Tendiks",
                column: "PegawaiId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DosenTetaps_DosenId",
                table: "DosenTetaps",
                column: "DosenId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Dosens_PegawaiId",
                table: "Dosens",
                column: "PegawaiId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Dosens_Pegawais_PegawaiId",
                table: "Dosens",
                column: "PegawaiId",
                principalTable: "Pegawais",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DosenTetaps_Dosens_DosenId",
                table: "DosenTetaps",
                column: "DosenId",
                principalTable: "Dosens",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tendiks_Pegawais_PegawaiId",
                table: "Tendiks",
                column: "PegawaiId",
                principalTable: "Pegawais",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TendikTetaps_Tendiks_TendikId",
                table: "TendikTetaps",
                column: "TendikId",
                principalTable: "Tendiks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Dosens_Pegawais_PegawaiId",
                table: "Dosens");

            migrationBuilder.DropForeignKey(
                name: "FK_DosenTetaps_Dosens_DosenId",
                table: "DosenTetaps");

            migrationBuilder.DropForeignKey(
                name: "FK_Tendiks_Pegawais_PegawaiId",
                table: "Tendiks");

            migrationBuilder.DropForeignKey(
                name: "FK_TendikTetaps_Tendiks_TendikId",
                table: "TendikTetaps");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TendikTetaps",
                table: "TendikTetaps");

            migrationBuilder.DropIndex(
                name: "IX_TendikTetaps_TendikId",
                table: "TendikTetaps");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tendiks",
                table: "Tendiks");

            migrationBuilder.DropIndex(
                name: "IX_Tendiks_PegawaiId",
                table: "Tendiks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Pegawais",
                table: "Pegawais");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DosenTetaps",
                table: "DosenTetaps");

            migrationBuilder.DropIndex(
                name: "IX_DosenTetaps_DosenId",
                table: "DosenTetaps");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Dosens",
                table: "Dosens");

            migrationBuilder.DropIndex(
                name: "IX_Dosens_PegawaiId",
                table: "Dosens");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "TendikTetaps");

            migrationBuilder.DropColumn(
                name: "TMT",
                table: "TendikTetaps");

            migrationBuilder.DropColumn(
                name: "TendikId",
                table: "TendikTetaps");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Tendiks");

            migrationBuilder.DropColumn(
                name: "PegawaiId",
                table: "Tendiks");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "DosenTetaps");

            migrationBuilder.DropColumn(
                name: "DosenId",
                table: "DosenTetaps");

            migrationBuilder.DropColumn(
                name: "ID",
                table: "Dosens");

            migrationBuilder.DropColumn(
                name: "PegawaiId",
                table: "Dosens");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Pegawais",
                newName: "ModifiedBy");

            migrationBuilder.AddColumn<string>(
                name: "NIP",
                table: "TendikTetaps",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "NIP",
                table: "Tendiks",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

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
                name: "Nama",
                table: "Pegawais",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "NIP",
                table: "Pegawais",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedBy",
                table: "Pegawais",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "Pegawais",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "DeletedBy",
                table: "Pegawais",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedDate",
                table: "Pegawais",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Pegawais",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDate",
                table: "Pegawais",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<string>(
                name: "Jabatan_Akademik",
                table: "DosenTetaps",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "NIP",
                table: "DosenTetaps",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "NIP",
                table: "Dosens",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DosenTetapNIP",
                table: "Dosens",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_TendikTetaps",
                table: "TendikTetaps",
                column: "NIP");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tendiks",
                table: "Tendiks",
                column: "NIP");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Pegawais",
                table: "Pegawais",
                column: "NIP");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DosenTetaps",
                table: "DosenTetaps",
                column: "NIP");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Dosens",
                table: "Dosens",
                column: "NIP");

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
                name: "FK_DosenTetaps_Dosens_NIP",
                table: "DosenTetaps",
                column: "NIP",
                principalTable: "Dosens",
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

            migrationBuilder.AddForeignKey(
                name: "FK_TendikTetaps_Tendiks_NIP",
                table: "TendikTetaps",
                column: "NIP",
                principalTable: "Tendiks",
                principalColumn: "NIP",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
