using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DocumentManagement.Domain.Migrations
{
    /// <inheritdoc />
    public partial class AddAcademicTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "KegiatanPPMS",
                columns: table => new
                {
                    KegiatanID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    JudulPPM = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TahunMulai = table.Column<int>(type: "int", nullable: false),
                    TahunSelesai = table.Column<int>(type: "int", nullable: false),
                    JenisPPM = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MitraPPM = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NomorKontrak = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DanaPPM = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KegiatanPPMS", x => x.KegiatanID);
                });

            migrationBuilder.CreateTable(
                name: "Publikasis",
                columns: table => new
                {
                    PublikasiId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TahunPublikasi = table.Column<int>(type: "int", nullable: false),
                    JudulPublikasi = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    KategoriPublikasi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PenerapanMasyarakat = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PegawaiId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Publikasis", x => x.PublikasiId);
                    table.ForeignKey(
                        name: "FK_Publikasis_Pegawais_PegawaiId",
                        column: x => x.PegawaiId,
                        principalTable: "Pegawais",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TahunAkademiks",
                columns: table => new
                {
                    ThnAkademikId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ThnAkademik = table.Column<int>(type: "int", nullable: false),
                    Semester = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TahunAkademiks", x => x.ThnAkademikId);
                });

            migrationBuilder.CreateTable(
                name: "TahunLuluss",
                columns: table => new
                {
                    TahunId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LulusanTahun = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TahunLuluss", x => x.TahunId);
                });

            migrationBuilder.CreateTable(
                name: "PegawaiKegiatanPPMS",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PegawaiId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    KegiatanID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PegawaiKegiatanPPMS", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PegawaiKegiatanPPMS_KegiatanPPMS_KegiatanID",
                        column: x => x.KegiatanID,
                        principalTable: "KegiatanPPMS",
                        principalColumn: "KegiatanID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PegawaiKegiatanPPMS_Pegawais_PegawaiId",
                        column: x => x.PegawaiId,
                        principalTable: "Pegawais",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PrestasiMhss",
                columns: table => new
                {
                    ThnAkademikId = table.Column<int>(type: "int", nullable: false),
                    JmlhPKM = table.Column<int>(type: "int", nullable: false),
                    JumlhMapres = table.Column<int>(type: "int", nullable: false),
                    JumlhLombaNasional = table.Column<int>(type: "int", nullable: false),
                    JumlhLombaInter = table.Column<int>(type: "int", nullable: false),
                    JumlhInbound = table.Column<int>(type: "int", nullable: false),
                    JumlhOutbound = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrestasiMhss", x => x.ThnAkademikId);
                    table.ForeignKey(
                        name: "FK_PrestasiMhss_TahunAkademiks_ThnAkademikId",
                        column: x => x.ThnAkademikId,
                        principalTable: "TahunAkademiks",
                        principalColumn: "ThnAkademikId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TahunMasuks",
                columns: table => new
                {
                    ThnMasukId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ThnMasuk = table.Column<int>(type: "int", nullable: false),
                    TotalRegistrasiMhs = table.Column<int>(type: "int", nullable: false),
                    ThnAkademikId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TahunMasuks", x => x.ThnMasukId);
                    table.ForeignKey(
                        name: "FK_TahunMasuks_TahunAkademiks_ThnAkademikId",
                        column: x => x.ThnAkademikId,
                        principalTable: "TahunAkademiks",
                        principalColumn: "ThnAkademikId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StatusLulusans",
                columns: table => new
                {
                    StatusId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TahunId = table.Column<int>(type: "int", nullable: false),
                    Tahun_Input = table.Column<int>(type: "int", nullable: false),
                    Bekerja = table.Column<int>(type: "int", nullable: false),
                    LanjutStudi = table.Column<int>(type: "int", nullable: false),
                    Internship = table.Column<int>(type: "int", nullable: false),
                    Berwirausaha = table.Column<int>(type: "int", nullable: false),
                    BelumKerja = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatusLulusans", x => x.StatusId);
                    table.ForeignKey(
                        name: "FK_StatusLulusans_TahunLuluss_TahunId",
                        column: x => x.TahunId,
                        principalTable: "TahunLuluss",
                        principalColumn: "TahunId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InfoMahasiswas",
                columns: table => new
                {
                    InfoMahasiswaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    JmlhLulus = table.Column<int>(type: "int", nullable: false),
                    JmlhNonAktif = table.Column<int>(type: "int", nullable: false),
                    JmlhDO = table.Column<int>(type: "int", nullable: false),
                    JmlhUndurDiri = table.Column<int>(type: "int", nullable: false),
                    RataanIPKTotal = table.Column<float>(type: "real", nullable: false),
                    MasaStudiDibawah8 = table.Column<int>(type: "int", nullable: false),
                    MasaStudi8Sampai10 = table.Column<int>(type: "int", nullable: false),
                    MasaStudiDiatas10 = table.Column<int>(type: "int", nullable: false),
                    JumlahIPKDibawah2 = table.Column<int>(type: "int", nullable: false),
                    JumlhMBKM = table.Column<int>(type: "int", nullable: false),
                    ThnMasukId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InfoMahasiswas", x => x.InfoMahasiswaId);
                    table.ForeignKey(
                        name: "FK_InfoMahasiswas_TahunMasuks_ThnMasukId",
                        column: x => x.ThnMasukId,
                        principalTable: "TahunMasuks",
                        principalColumn: "ThnMasukId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "JenisTempatKerjas",
                columns: table => new
                {
                    StatusId = table.Column<int>(type: "int", nullable: false),
                    BUMN = table.Column<int>(type: "int", nullable: false),
                    Organisasi_Multilateral = table.Column<int>(type: "int", nullable: false),
                    Instansi_Pemerintah = table.Column<int>(type: "int", nullable: false),
                    Organisasi_NonProfit = table.Column<int>(type: "int", nullable: false),
                    Wirausaha = table.Column<int>(type: "int", nullable: false),
                    Lainnya = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JenisTempatKerjas", x => x.StatusId);
                    table.ForeignKey(
                        name: "FK_JenisTempatKerjas_StatusLulusans_StatusId",
                        column: x => x.StatusId,
                        principalTable: "StatusLulusans",
                        principalColumn: "StatusId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MasaTungguKerjas",
                columns: table => new
                {
                    StatusId = table.Column<int>(type: "int", nullable: false),
                    Diatas6Bulan = table.Column<int>(type: "int", nullable: false),
                    Dibawah6Bulan = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MasaTungguKerjas", x => x.StatusId);
                    table.ForeignKey(
                        name: "FK_MasaTungguKerjas_StatusLulusans_StatusId",
                        column: x => x.StatusId,
                        principalTable: "StatusLulusans",
                        principalColumn: "StatusId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PosisiJabatans",
                columns: table => new
                {
                    StatusId = table.Column<int>(type: "int", nullable: false),
                    Founder = table.Column<int>(type: "int", nullable: false),
                    CoFounder = table.Column<int>(type: "int", nullable: false),
                    Staf = table.Column<int>(type: "int", nullable: false),
                    Freelancer = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PosisiJabatans", x => x.StatusId);
                    table.ForeignKey(
                        name: "FK_PosisiJabatans_StatusLulusans_StatusId",
                        column: x => x.StatusId,
                        principalTable: "StatusLulusans",
                        principalColumn: "StatusId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TingkatTempatKerjas",
                columns: table => new
                {
                    StatusId = table.Column<int>(type: "int", nullable: false),
                    Lokal = table.Column<int>(type: "int", nullable: false),
                    Nasional = table.Column<int>(type: "int", nullable: false),
                    MultiNasional = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TingkatTempatKerjas", x => x.StatusId);
                    table.ForeignKey(
                        name: "FK_TingkatTempatKerjas_StatusLulusans_StatusId",
                        column: x => x.StatusId,
                        principalTable: "StatusLulusans",
                        principalColumn: "StatusId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InfoMahasiswas_ThnMasukId",
                table: "InfoMahasiswas",
                column: "ThnMasukId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PegawaiKegiatanPPMS_KegiatanID",
                table: "PegawaiKegiatanPPMS",
                column: "KegiatanID");

            migrationBuilder.CreateIndex(
                name: "IX_PegawaiKegiatanPPMS_PegawaiId_KegiatanID",
                table: "PegawaiKegiatanPPMS",
                columns: new[] { "PegawaiId", "KegiatanID" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Publikasis_PegawaiId",
                table: "Publikasis",
                column: "PegawaiId");

            migrationBuilder.CreateIndex(
                name: "IX_StatusLulusans_TahunId",
                table: "StatusLulusans",
                column: "TahunId");

            migrationBuilder.CreateIndex(
                name: "IX_TahunMasuks_ThnAkademikId",
                table: "TahunMasuks",
                column: "ThnAkademikId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InfoMahasiswas");

            migrationBuilder.DropTable(
                name: "JenisTempatKerjas");

            migrationBuilder.DropTable(
                name: "MasaTungguKerjas");

            migrationBuilder.DropTable(
                name: "PegawaiKegiatanPPMS");

            migrationBuilder.DropTable(
                name: "PosisiJabatans");

            migrationBuilder.DropTable(
                name: "PrestasiMhss");

            migrationBuilder.DropTable(
                name: "Publikasis");

            migrationBuilder.DropTable(
                name: "TingkatTempatKerjas");

            migrationBuilder.DropTable(
                name: "TahunMasuks");

            migrationBuilder.DropTable(
                name: "KegiatanPPMS");

            migrationBuilder.DropTable(
                name: "StatusLulusans");

            migrationBuilder.DropTable(
                name: "TahunAkademiks");

            migrationBuilder.DropTable(
                name: "TahunLuluss");
        }
    }
}
