using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DocumentManagement.Domain.Migrations
{
    /// <inheritdoc />
    public partial class AddKepegawaian : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Pegawais",
                columns: table => new
                {
                    NIP = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Nama = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TanggalLahir = table.Column<DateTime>(name: "Tanggal_Lahir", type: "datetime2", nullable: false),
                    TempatLahir = table.Column<string>(name: "Tempat_Lahir", type: "nvarchar(max)", nullable: true),
                    JenisKelamin = table.Column<int>(name: "Jenis_Kelamin", type: "int", nullable: false),
                    Alamat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TeleponDarurat = table.Column<string>(name: "Telepon_Darurat", type: "nvarchar(max)", nullable: true),
                    PendidikanTerakhir = table.Column<int>(name: "Pendidikan_Terakhir", type: "int", nullable: false),
                    StatusPernikahan = table.Column<int>(name: "Status_Pernikahan", type: "int", nullable: false),
                    NomorBPJS = table.Column<string>(name: "Nomor_BPJS", type: "nvarchar(max)", nullable: true),
                    NomorNPWP = table.Column<string>(name: "Nomor_NPWP", type: "nvarchar(max)", nullable: true),
                    NomorPaspor = table.Column<string>(name: "Nomor_Paspor", type: "nvarchar(max)", nullable: true),
                    JenisPegawai = table.Column<int>(name: "Jenis_Pegawai", type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pegawais", x => x.NIP);
                });

            migrationBuilder.CreateTable(
                name: "Dosens",
                columns: table => new
                {
                    NIP = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    JenisDosen = table.Column<int>(name: "Jenis_Dosen", type: "int", nullable: false),
                    Gelar = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dosens", x => x.NIP);
                    table.ForeignKey(
                        name: "FK_Dosens_Pegawais_NIP",
                        column: x => x.NIP,
                        principalTable: "Pegawais",
                        principalColumn: "NIP",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Tendiks",
                columns: table => new
                {
                    NIP = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    JenisTendik = table.Column<int>(name: "Jenis_Tendik", type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tendiks", x => x.NIP);
                    table.ForeignKey(
                        name: "FK_Tendiks_Pegawais_NIP",
                        column: x => x.NIP,
                        principalTable: "Pegawais",
                        principalColumn: "NIP",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DosenTetaps",
                columns: table => new
                {
                    NIP = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    NIDN = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    JabatanAkademik = table.Column<int>(name: "Jabatan_Akademik", type: "int", nullable: false),
                    Golongan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TMT = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NomorSerdos = table.Column<string>(name: "Nomor_Serdos", type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DosenTetaps", x => x.NIP);
                    table.ForeignKey(
                        name: "FK_DosenTetaps_Dosens_NIP",
                        column: x => x.NIP,
                        principalTable: "Dosens",
                        principalColumn: "NIP",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TendikTetaps",
                columns: table => new
                {
                    NIP = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Jabatan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Golongan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TendikTetaps", x => x.NIP);
                    table.ForeignKey(
                        name: "FK_TendikTetaps_Tendiks_NIP",
                        column: x => x.NIP,
                        principalTable: "Tendiks",
                        principalColumn: "NIP",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DosenTetaps");

            migrationBuilder.DropTable(
                name: "TendikTetaps");

            migrationBuilder.DropTable(
                name: "Dosens");

            migrationBuilder.DropTable(
                name: "Tendiks");

            migrationBuilder.DropTable(
                name: "Pegawais");
        }
    }
}
