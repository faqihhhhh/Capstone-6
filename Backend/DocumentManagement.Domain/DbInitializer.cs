using DocumentManagement.Data;
using DocumentManagement.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DocumentManagement.Domain
{
    public static class DbInitializer
    {
        public static void Initialize(DocumentContext context)
        {
            // Seed Tahun Akademik
            if (!context.TahunAkademiks.Any())
            {
                var tahunAkademiks = new TahunAkademik[]
                {
                    new TahunAkademik { ThnAkademik = 2023, Semester = SemesterType.Ganjil },
                    new TahunAkademik { ThnAkademik = 2023, Semester = SemesterType.Genap },
                    new TahunAkademik { ThnAkademik = 2024, Semester = SemesterType.Ganjil }
                };
                context.TahunAkademiks.AddRange(tahunAkademiks);
                context.SaveChanges();
            }

            // Seed Pegawai (Dosen dan Tendik)
            if (!context.Pegawais.Any())
            {
                var pegawais = new List<Pegawai>();

                // Dosen Tetap 1
                var dosen1Id = Guid.NewGuid();
                pegawais.Add(new Pegawai
                {
                    Id = dosen1Id,
                    NIP = "198001012005011001",
                    Nama = "Dr. Budi Santoso, M.Kom.",
                    Tanggal_Lahir = new DateTime(1980, 1, 1),
                    Tempat_Lahir = "Jakarta",
                    Jenis_Kelamin = Gender.Pria,
                    Alamat = "Jl. Sudirman No. 1",
                    Telepon_Darurat = "081234567890",
                    Pendidikan_Terakhir = Studi.S3,
                    Gelar = "Dr.",
                    Status_Pernikahan = Nikah.Menikah,
                    Nomor_BPJS = "000111222333",
                    Nomor_NPWP = "123456789012345",
                    Nomor_Paspor = "-",
                    Jenis_Pegawai = JenisP.Dosen,
                    Dosen = new Dosen
                    {
                        Id = Guid.NewGuid(),
                        PegawaiId = dosen1Id,
                        Jenis_Dosen = Status_Dosen.PNS,
                        DosenTetap = new DosenTetap
                        {
                            Id = Guid.NewGuid(),
                            NIDN = "0001018001",
                            Jabatan_Akademik = Jabatan.LektorKepala,
                            Golongan = "IV/a",
                            Nomor_Serdos = "123456",
                            TMT = new DateTime(2005, 1, 1)
                        }
                    }
                });

                // Dosen Tetap 2
                var dosen2Id = Guid.NewGuid();
                pegawais.Add(new Pegawai
                {
                    Id = dosen2Id,
                    NIP = "198502022010012002",
                    Nama = "Siti Aminah, S.T., M.T.",
                    Tanggal_Lahir = new DateTime(1985, 2, 2),
                    Tempat_Lahir = "Bandung",
                    Jenis_Kelamin = Gender.Wanita,
                    Alamat = "Jl. Asia Afrika No. 10",
                    Telepon_Darurat = "081987654321",
                    Pendidikan_Terakhir = Studi.S2,
                    Gelar = "M.T.",
                    Status_Pernikahan = Nikah.Menikah,
                    Nomor_BPJS = "000111222334",
                    Nomor_NPWP = "123456789012346",
                    Nomor_Paspor = "-",
                    Jenis_Pegawai = JenisP.Dosen,
                    Dosen = new Dosen
                    {
                        Id = Guid.NewGuid(),
                        PegawaiId = dosen2Id,
                        Jenis_Dosen = Status_Dosen.PNS,
                        DosenTetap = new DosenTetap
                        {
                            Id = Guid.NewGuid(),
                            NIDN = "0002028502",
                            Jabatan_Akademik = Jabatan.Lektor,
                            Golongan = "III/c",
                            Nomor_Serdos = "654321",
                            TMT = new DateTime(2010, 1, 1)
                        }
                    }
                });

                // Dosen Kontrak
                var dosen3Id = Guid.NewGuid();
                pegawais.Add(new Pegawai
                {
                    Id = dosen3Id,
                    NIP = "199003032015011003",
                    Nama = "Agus Prasetyo, S.Kom., M.Kom.",
                    Tanggal_Lahir = new DateTime(1990, 3, 3),
                    Tempat_Lahir = "Surabaya",
                    Jenis_Kelamin = Gender.Pria,
                    Alamat = "Jl. Pahlawan No. 5",
                    Telepon_Darurat = "08122334455",
                    Pendidikan_Terakhir = Studi.S2,
                    Gelar = "M.Kom.",
                    Status_Pernikahan = Nikah.Lajang,
                    Nomor_BPJS = "000111222335",
                    Nomor_NPWP = "123456789012347",
                    Nomor_Paspor = "-",
                    Jenis_Pegawai = JenisP.Dosen,
                    Dosen = new Dosen
                    {
                        Id = Guid.NewGuid(),
                        PegawaiId = dosen3Id,
                        Jenis_Dosen = Status_Dosen.Kontrak
                    }
                });

                // Tendik PNS
                var tendik1Id = Guid.NewGuid();
                pegawais.Add(new Pegawai
                {
                    Id = tendik1Id,
                    NIP = "198204042008012004",
                    Nama = "Rina Marlina, S.A.B.",
                    Tanggal_Lahir = new DateTime(1982, 4, 4),
                    Tempat_Lahir = "Yogyakarta",
                    Jenis_Kelamin = Gender.Wanita,
                    Alamat = "Jl. Malioboro No. 20",
                    Telepon_Darurat = "08133445566",
                    Pendidikan_Terakhir = Studi.S1,
                    Gelar = "S.A.B.",
                    Status_Pernikahan = Nikah.Menikah,
                    Nomor_BPJS = "000111222336",
                    Nomor_NPWP = "123456789012348",
                    Nomor_Paspor = "-",
                    Jenis_Pegawai = JenisP.Tendik,
                    Tendik = new Tendik
                    {
                        Id = Guid.NewGuid(),
                        PegawaiId = tendik1Id,
                        Jenis_Tendik = Status_Tendik.PNS,
                        TendikTetap = new TendikTetap
                        {
                            Id = Guid.NewGuid(),
                            Jabatan = "Administrasi Akademik",
                            Golongan = "II/c",
                            TMT = new DateTime(2008, 1, 1)
                        }
                    }
                });

                // Tendik Kontrak
                var tendik2Id = Guid.NewGuid();
                pegawais.Add(new Pegawai
                {
                    Id = tendik2Id,
                    NIP = "199505052020011005",
                    Nama = "Andi Wijaya, A.Md.",
                    Tanggal_Lahir = new DateTime(1995, 5, 5),
                    Tempat_Lahir = "Semarang",
                    Jenis_Kelamin = Gender.Pria,
                    Alamat = "Jl. Pemuda No. 15",
                    Telepon_Darurat = "08144556677",
                    Pendidikan_Terakhir = Studi.D3,
                    Gelar = "A.Md.",
                    Status_Pernikahan = Nikah.Lajang,
                    Nomor_BPJS = "000111222337",
                    Nomor_NPWP = "123456789012349",
                    Nomor_Paspor = "-",
                    Jenis_Pegawai = JenisP.Tendik,
                    Tendik = new Tendik
                    {
                        Id = Guid.NewGuid(),
                        PegawaiId = tendik2Id,
                        Jenis_Tendik = Status_Tendik.Kontrak
                    }
                });

                context.Pegawais.AddRange(pegawais);
                context.SaveChanges();

                // Seed Kegiatan PPM
                if (!context.KegiatanPPMs.Any())
                {
                    var tahunAkademikFirst = context.TahunAkademiks.FirstOrDefault();
                    if (tahunAkademikFirst != null)
                    {
                        var kegiatan1Id = Guid.NewGuid();
                        var kegiatan2Id = Guid.NewGuid();

                        var kegiatanPPMs = new KegiatanPPM[]
                        {
                            new KegiatanPPM
                            {
                                KegiatanID = kegiatan1Id,
                                JudulPPM = "Penerapan AI untuk Pertanian Cerdas di Desa X",
                                TahunMulai = 2023,
                                TahunSelesai = 2024,
                                JenisPPM = Jenis_PPM.Pengabdian,
                                MitraPPM = Mitra_PPM.Pemerintah,
                                NomorKontrak = "001/PPM/2023",
                                DanaPPM = 50000000
                            },
                            new KegiatanPPM
                            {
                                KegiatanID = kegiatan2Id,
                                JudulPPM = "Pengembangan Sistem Informasi Geografis untuk Pemetaan Potensi Wisata",
                                TahunMulai = 2023,
                                TahunSelesai = 2024,
                                JenisPPM = Jenis_PPM.Penelitian,
                                MitraPPM = Mitra_PPM.Swasta,
                                NomorKontrak = "002/PPM/2023",
                                DanaPPM = 25000000
                            }
                        };
                        context.KegiatanPPMs.AddRange(kegiatanPPMs);
                        context.SaveChanges();

                        // Link Dosen to PPM
                        var dsn1 = context.Pegawais.FirstOrDefault(p => p.NIP == "198001012005011001");
                        var dsn2 = context.Pegawais.FirstOrDefault(p => p.NIP == "198502022010012002");

                        if (dsn1 != null && dsn2 != null)
                        {
                            var pegawaiKegiatan = new PegawaiKegiatanPPM[]
                            {
                                new PegawaiKegiatanPPM { Id = Guid.NewGuid(), PegawaiId = dsn1.Id, KegiatanID = kegiatan1Id },
                                new PegawaiKegiatanPPM { Id = Guid.NewGuid(), PegawaiId = dsn2.Id, KegiatanID = kegiatan1Id },
                                new PegawaiKegiatanPPM { Id = Guid.NewGuid(), PegawaiId = dsn1.Id, KegiatanID = kegiatan2Id }
                            };
                            context.PegawaiKegiatanPPMs.AddRange(pegawaiKegiatan);
                            context.SaveChanges();
                        }
                    }
                }

                // Seed Publikasi
                if (!context.Publikasis.Any())
                {
                    var dsn1 = context.Pegawais.FirstOrDefault(p => p.NIP == "198001012005011001");
                    if (dsn1 != null)
                    {
                        var publikasis = new Publikasi[]
                        {
                            new Publikasi
                            {
                                PublikasiId = Guid.NewGuid(),
                                PegawaiId = dsn1.Id,
                                JudulPublikasi = "Optimasi Jaringan Syaraf Tiruan untuk Prediksi Cuaca",
                                KategoriPublikasi = KategoriPublikasi.Scopus,
                                TahunPublikasi = 2023,
                                PenerapanMasyarakat = PenerapanMasyarakat.Ya
                            },
                            new Publikasi
                            {
                                PublikasiId = Guid.NewGuid(),
                                PegawaiId = dsn1.Id,
                                JudulPublikasi = "Analisis Sentimen Menggunakan Metode Naive Bayes",
                                KategoriPublikasi = KategoriPublikasi.Sinta,
                                TahunPublikasi = 2022,
                                PenerapanMasyarakat = PenerapanMasyarakat.Tidak
                            }
                        };
                        context.Publikasis.AddRange(publikasis);
                        context.SaveChanges();
                    }
                }
            }

            // Seed Users
            if (context.Users.Count() <= 3)
            {
                var newUsers = new List<User>
                {
                    new User
                    {
                        Id = Guid.NewGuid(),
                        FirstName = "Rudi",
                        LastName = "Hartono",
                        Email = "rudi@gmail.com",
                        NormalizedEmail = "RUDI@GMAIL.COM",
                        UserName = "rudi@gmail.com",
                        NormalizedUserName = "RUDI@GMAIL.COM",
                        PasswordHash = "AQAAAAEAACcQAAAAEM60FYHL5RMKNeB+CxCOI41EC8Vsr1B3Dyrrr2BOtZrxz6doL8o6Tv/tYGDRk20t1A==", // same as default admin password
                        SecurityStamp = Guid.NewGuid().ToString(),
                        IsDeleted = false
                    },
                    new User
                    {
                        Id = Guid.NewGuid(),
                        FirstName = "Maya",
                        LastName = "Sari",
                        Email = "maya@gmail.com",
                        NormalizedEmail = "MAYA@GMAIL.COM",
                        UserName = "maya@gmail.com",
                        NormalizedUserName = "MAYA@GMAIL.COM",
                        PasswordHash = "AQAAAAEAACcQAAAAEM60FYHL5RMKNeB+CxCOI41EC8Vsr1B3Dyrrr2BOtZrxz6doL8o6Tv/tYGDRk20t1A==",
                        SecurityStamp = Guid.NewGuid().ToString(),
                        IsDeleted = false
                    }
                };
                context.Users.AddRange(newUsers);
                context.SaveChanges();
            }

            // Seed Documents
            if (!context.Documents.Any())
            {
                var categoryId = Guid.Parse("9cc497f5-1736-4bc6-84a8-316fd983b732"); // HR Policies category
                var adminUser = context.Users.FirstOrDefault(u => u.Email == "admin@gmail.com");
                
                var newDocs = new List<Document>
                {
                    new Document
                    {
                        Id = Guid.NewGuid(),
                        Name = "SOP Pelaksanaan Skripsi",
                        Description = "Dokumen standar operasional prosedur untuk pelaksanaan skripsi mahasiswa.",
                        Url = "dummy_sop_skripsi.pdf",
                        CategoryId = categoryId,
                        CreatedDate = DateTime.UtcNow,
                        CreatedBy = adminUser?.Id ?? Guid.Empty,
                        IsDeleted = false
                    },
                    new Document
                    {
                        Id = Guid.NewGuid(),
                        Name = "Panduan Akademik 2024",
                        Description = "Buku panduan akademik untuk mahasiswa angkatan 2024.",
                        Url = "panduan_akademik_2024.pdf",
                        CategoryId = categoryId,
                        CreatedDate = DateTime.UtcNow,
                        CreatedBy = adminUser?.Id ?? Guid.Empty,
                        IsDeleted = false
                    }
                };
                context.Documents.AddRange(newDocs);
                context.SaveChanges();
            }

            // Seed Reminders
            if (!context.Reminders.Any())
            {
                var doc = context.Documents.FirstOrDefault();
                var newReminders = new List<Reminder>
                {
                    new Reminder
                    {
                        Id = Guid.NewGuid(),
                        Subject = "Evaluasi Kurikulum Semester",
                        Message = "Harap melakukan evaluasi terhadap dokumen panduan akademik ini.",
                        StartDate = DateTime.UtcNow.AddDays(1),
                        EndDate = DateTime.UtcNow.AddDays(7),
                        Frequency = Frequency.OneTime,
                        IsRepeated = false,
                        IsEmailNotification = true,
                        DocumentId = doc?.Id,
                        CreatedDate = DateTime.UtcNow,
                        CreatedBy = Guid.Empty,
                        IsDeleted = false
                    }
                };
                context.Reminders.AddRange(newReminders);
                context.SaveChanges();
            }
        }
    }
}
