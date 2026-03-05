using DocumentManagement.Data;
using DocumentManagement.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Reflection.Emit;

namespace DocumentManagement.Domain
{
    public class DocumentContext : IdentityDbContext<User, Role, Guid, UserClaim, UserRole, UserLogin, RoleClaim, UserToken>
    {
        public DocumentContext(DbContextOptions options) : base(options)
        {
        }
        public override DbSet<User> Users { get; set; }
        public override DbSet<Role> Roles { get; set; }
        public override DbSet<UserClaim> UserClaims { get; set; }
        public override DbSet<UserRole> UserRoles { get; set; }
        public override DbSet<UserLogin> UserLogins { get; set; }
        public override DbSet<RoleClaim> RoleClaims { get; set; }
        public override DbSet<UserToken> UserTokens { get; set; }
        public DbSet<Operation> Operations { get; set; }
        public DbSet<Screen> Screens { get; set; }
        public DbSet<ScreenOperation> ScreenOperations { get; set; }
        public DbSet<NLog> NLog { get; set; }
        public DbSet<Document> Documents { get; set; }
        public DbSet<Category> Categories { get; set; }
        public virtual DbSet<DocumentRolePermission> DocumentRolePermissions { get; set; }
        public virtual DbSet<DocumentUserPermission> DocumentUserPermissions { get; set; }
        public DbSet<DocumentAuditTrail> DocumentAuditTrails { get; set; }
        public DbSet<UserNotification> UserNotifications { get; set; }
        public DbSet<LoginAudit> LoginAudits { get; set; }
        public DbSet<DocumentToken> DocumentTokens { get; set; }
        public DbSet<Reminder> Reminders { get; set; }
        public DbSet<ReminderNotification> ReminderNotifications { get; set; }
        public DbSet<ReminderUser> ReminderUsers { get; set; }
        public DbSet<ReminderScheduler> ReminderSchedulers { get; set; }
        public DbSet<HalfYearlyReminder> HalfYearlyReminders { get; set; }
        public DbSet<QuarterlyReminder> QuarterlyReminders { get; set; }
        public DbSet<DailyReminder> DailyReminders { get; set; }
        public DbSet<EmailSMTPSetting> EmailSMTPSettings { get; set; }
        public DbSet<SendEmail> SendEmails { get; set; }

        public DbSet<DocumentComment> DocumentComments { get; set; }
        public DbSet<DocumentVersion> DocumentVersions { get; set; }
        public DbSet<DocumentMetaData> DocumentMetaDatas { get; set; }

        //Modul Kepegawaian
        public DbSet<Pegawai> Pegawais { get; set; }
        public DbSet<Dosen> Dosens { get; set; }
        public DbSet<DosenTetap> DosenTetaps { get; set; }
        public DbSet<Tendik> Tendiks { get; set; }
        public DbSet<TendikTetap> TendikTetaps { get; set; }

        //Modul Kemahasiswaan
        public DbSet<TahunLulus> TahunLuluss { get; set; }
        public DbSet<StatusLulusan> StatusLulusans { get; set; }
        public DbSet<MasaTungguKerja> MasaTungguKerjas { get; set; }
        public DbSet<JenisTempatKerja> JenisTempatKerjas { get; set; }
        public DbSet<PosisiJabatan> PosisiJabatans { get; set; }
        public DbSet<TingkatTempatKerja> TingkatKerjas { get; set; }
        public DbSet<TahunAkademik> TahunAkademiks { get; set; }
        public DbSet<PrestasiMhs> PrestasiMhss { get; set; }
        public DbSet<TahunMasuk> TahunMasuks { get; set; }

        //Modul Kependidikan
        public DbSet<InfoMahasiswa> InfoMahasiswas { get; set; }

        //Modul Kegiatan PPM
        public DbSet<KegiatanPPM> KegiatanPPMs { get; set; }
        public DbSet<PegawaiKegiatanPPM> PegawaiKegiatanPPMs { get; set; }

        //Modul Luaran PPM
        public DbSet<Publikasi> Publikasis { get; set; }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.AddInterceptors(new TaggedQueryCommandInterceptor());
        //}

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Category>(entity =>
            {
                entity.HasOne(x => x.Parent)
                    .WithMany(x => x.Children)
                    .HasForeignKey(x => x.ParentId)
                    .IsRequired(false)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            builder.Entity<User>(b =>
            {
                // Each User can have many UserClaims
                b.HasMany(e => e.UserClaims)
                    .WithOne(e => e.User)
                    .HasForeignKey(uc => uc.UserId)
                    .IsRequired();

                // Each User can have many UserLogins
                b.HasMany(e => e.UserLogins)
                    .WithOne(e => e.User)
                    .HasForeignKey(ul => ul.UserId)
                    .IsRequired();

                // Each User can have many UserTokens
                b.HasMany(e => e.UserTokens)
                    .WithOne(e => e.User)
                    .HasForeignKey(ut => ut.UserId)
                    .IsRequired();

                // Each User can have many entries in the UserRole join table
                b.HasMany(e => e.UserRoles)
                    .WithOne(e => e.User)
                    .HasForeignKey(ur => ur.UserId)
                    .IsRequired();
            });

            builder.Entity<Role>(b =>
            {
                // Each Role can have many entries in the UserRole join table
                b.HasMany(e => e.UserRoles)
                    .WithOne(e => e.Role)
                    .HasForeignKey(ur => ur.RoleId)
                    .IsRequired();

                // Each Role can have many associated RoleClaims
                b.HasMany(e => e.RoleClaims)
                    .WithOne(e => e.Role)
                    .HasForeignKey(rc => rc.RoleId)
                    .IsRequired();
            });

            builder.Entity<DocumentRolePermission>(entity =>
            {
                entity.HasOne(d => d.Document)
                    .WithMany(p => p.DocumentRolePermissions)
                    .HasForeignKey(d => d.DocumentId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.DocumentRolePermissions)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            builder.Entity<DocumentUserPermission>(entity =>
            {

                entity.HasOne(d => d.Document)
                    .WithMany(p => p.DocumentUserPermissions)
                    .HasForeignKey(d => d.DocumentId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.DocumentUserPermissions)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            builder.Entity<DocumentAuditTrail>(entity =>
            {
                entity.HasOne(d => d.Document)
                  .WithMany(p => p.DocumentAuditTrails)
                  .HasForeignKey(d => d.DocumentId)
                  .OnDelete(DeleteBehavior.ClientSetNull);
            });
            builder.Entity<UserNotification>(entity =>
            {
                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserNotifications)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.NoAction);

                entity.HasOne(d => d.Document)
                    .WithMany(p => p.UserNotifications)
                    .HasForeignKey(d => d.DocumentId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            builder.Entity<ReminderUser>(b =>
            {
                b.HasKey(e => new { e.ReminderId, e.UserId });
                b.HasOne(e => e.User)
                  .WithMany()
                  .HasForeignKey(ur => ur.UserId)
                  .OnDelete(DeleteBehavior.NoAction);
            });

            builder.Entity<Document>()
             .HasQueryFilter(p => !p.IsDeleted)
             .HasIndex(b => b.Url);

            builder.Entity<DocumentComment>(b =>
            {
                b.HasOne(e => e.CreatedByUser)
                    .WithMany()
                    .HasForeignKey(ur => ur.CreatedBy)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            builder.Entity<DocumentVersion>(b =>
            {
                b.HasOne(e => e.CreatedByUser)
                    .WithMany()
                    .HasForeignKey(ur => ur.CreatedBy)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            //---------------------------- Relasi Kepegawaian --------------------------//

            //Relasi 1-to-1 antara Pegawai dan Dosen

            builder.Entity<Pegawai>()
                .HasOne(p => p.Dosen)
                .WithOne(d => d.Pegawai)
                .HasForeignKey<Dosen>(d => d.PegawaiId)
                .OnDelete(DeleteBehavior.Cascade); // Jika Pegawai dihapus, Dosen juga dihapus

            /// Relasi 1-to-1 antara Pegawai dan Tendik
            builder.Entity<Pegawai>()
                .HasOne(p => p.Tendik)
                .WithOne(t => t.Pegawai)
                .HasForeignKey<Tendik>(t => t.PegawaiId)
                .OnDelete(DeleteBehavior.Cascade); // Jika Pegawai dihapus, Tendik juga dihapus

            // Relasi 1-to-1 antara Dosen dan DosenTetap (Nullable)
            builder.Entity<Dosen>()
                .HasOne(d => d.DosenTetap)
                .WithOne(dt => dt.Dosen)
                .HasForeignKey<DosenTetap>(dt => dt.DosenId)
                .OnDelete(DeleteBehavior.Cascade); // Jika Dosen dihapus, DosenTetap juga dihapus (jika ada)

            // Relasi 1-to-1 antara Tendik dan TendikTetap (Nullable)
            builder.Entity<Tendik>()
                .HasOne(t => t.TendikTetap)
                .WithOne(tt => tt.Tendik)
                .HasForeignKey<TendikTetap>(tt => tt.TendikId)
                .OnDelete(DeleteBehavior.Cascade); // Jika Tendik dihapus, TendikTetap juga dihapus (jika ada)

            //---------------------------- Relasi Kemahasiswaan --------------------------//
            //Relasi 1-to-many antara TahunLulus dan StatusLulusan
            //builder.Entity<TahunLulus>()
            //   .HasMany(t => t.StatusLulusan)
            //    .WithOne(s => s.TahunLulus)
            //    .HasForeignKey(s => s.TahunId)
            //    .OnDelete(DeleteBehavior.Cascade);

            //Relasi 1-to-1 antara StatusLulusan dan Entitas Lainnya
            builder.Entity<StatusLulusan>()
                .HasOne(s => s.MasaTungguKerja)
                .WithOne(m => m.StatusLulusan)
                .HasForeignKey<MasaTungguKerja>(m => m.StatusId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<StatusLulusan>()
                .HasOne(s => s.JenisTempatKerja)
                .WithOne(j => j.StatusLulusan)
                .HasForeignKey<JenisTempatKerja>(j => j.StatusId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<StatusLulusan>()
                .HasOne(s => s.PosisiJabatan)
                .WithOne(p => p.StatusLulusan)
                .HasForeignKey<PosisiJabatan>(p => p.StatusId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<StatusLulusan>()
                .HasOne(s => s.TingkatTempatKerja)
                .WithOne(t => t.StatusLulusan)
                .HasForeignKey<TingkatTempatKerja>(t => t.StatusId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<TahunAkademik>()
                .HasOne(t => t.PrestasiMhs)
                .WithOne(s => s.TahunAkademik)
                .HasForeignKey<PrestasiMhs>(s => s.ThnAkademikId)
                .OnDelete(DeleteBehavior.Cascade);

            //---------------------------- Relasi Kependidikan --------------------------//
            builder.Entity<TahunAkademik>()
                .HasMany(t => t.TahunMasuks)
                .WithOne(t => t.TahunAkademik)
                .HasForeignKey(t => t.ThnAkademikId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<TahunMasuk>()
                .HasOne(t => t.InfoMahasiswa)
                .WithOne(i => i.TahunMasuk)
                .HasForeignKey<InfoMahasiswa>(i => i.ThnMasukId)
                .OnDelete(DeleteBehavior.Cascade);

            //---------------------------- Relasi KegiatanPPM ---------------------------//
            // Konfigurasi untuk tabel PegawaiKegiatanPPM
            builder.Entity<PegawaiKegiatanPPM>()
                .HasOne(pk => pk.Pegawai)
                .WithMany(p => p.PegawaiKegiatanPPMs)
                .HasForeignKey(pk => pk.PegawaiId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<PegawaiKegiatanPPM>()
                .HasOne(pk => pk.KegiatanPPM)
                .WithMany(k => k.PegawaiKegiatanPPMs)
                .HasForeignKey(pk => pk.KegiatanID)
                .OnDelete(DeleteBehavior.Cascade);
            
            builder.Entity<PegawaiKegiatanPPM>()
                .HasIndex(pk => new { pk.PegawaiId, pk.KegiatanID })
                .IsUnique(); // Membuat kombinasi PegawaiID dan KegiatanID unik

            //---------------------------- Relasi LuaranPPM ---------------------------//
            builder.Entity<Publikasi>()
                .HasOne(p => p.Pegawai)
                .WithMany(p => p.Publikasis)
                .HasForeignKey(p => p.PegawaiId);

            // Enum to String Conversion
            builder.Entity<Dosen>().Property(d => d.Jenis_Dosen).HasConversion<string>();
            builder.Entity<Pegawai>().Property(p => p.Jenis_Pegawai).HasConversion<string>();
            builder.Entity<Pegawai>().Property(p => p.Jenis_Kelamin).HasConversion<string>();
            builder.Entity<Pegawai>().Property(p => p.Pendidikan_Terakhir).HasConversion<string>();
            builder.Entity<Pegawai>().Property(p => p.Status_Pernikahan).HasConversion<string>();
            builder.Entity<Tendik>().Property(t => t.Jenis_Tendik).HasConversion<string>();
            builder.Entity<DosenTetap>().Property(dt => dt.Jabatan_Akademik).HasConversion<string>();
            builder.Entity<TahunAkademik>().Property(t => t.Semester).HasConversion<string>();
            builder.Entity<KegiatanPPM>().Property(k => k.JenisPPM).HasConversion<string>();
            builder.Entity<KegiatanPPM>().Property(k => k.MitraPPM).HasConversion<string>();
            builder.Entity<Publikasi>().Property(p => p.KategoriPublikasi).HasConversion<string>();
            builder.Entity<Publikasi>().Property(p => p.PenerapanMasyarakat).HasConversion<string>();
            
            //-------------------------------------------------------------------------------
            builder.Entity<DocumentUserPermission>().HasQueryFilter(p => !p.IsDeleted);
            builder.Entity<DocumentRolePermission>().HasQueryFilter(p => !p.IsDeleted);
            builder.Entity<User>().ToTable("Users");
            builder.Entity<Role>().ToTable("Roles");
            builder.Entity<RoleClaim>().ToTable("RoleClaims");
            builder.Entity<UserClaim>().ToTable("UserClaims");
            builder.Entity<UserLogin>().ToTable("UserLogins");
            builder.Entity<UserRole>().ToTable("UserRoles");
            builder.Entity<UserToken>().ToTable("UserTokens");
            builder.Entity<DocumentUserPermission>().ToTable("DocumentUserPermissions");
            builder.Entity<DocumentRolePermission>().ToTable("DocumentRolePermissions");
            builder.Entity<UserNotification>().ToTable("UserNotifications");
            builder.DefalutMappingValue();
            builder.DefalutDeleteValueFilter();
            //TPT (Table per Type):memetakan setiap entitas ke tabel yang berbeda
            builder.Entity<Pegawai>().ToTable("Pegawais");
            builder.Entity<Dosen>().ToTable("Dosens");
            builder.Entity<DosenTetap>().ToTable("DosenTetaps");
            builder.Entity<Tendik>().ToTable("Tendiks");
            builder.Entity<TendikTetap>().ToTable("TendikTetaps");
            //--------------------------------------------------------
            builder.Entity<TahunLulus>().ToTable("TahunLuluss");
            builder.Entity<StatusLulusan>().ToTable("StatusLulusans");
            builder.Entity<MasaTungguKerja>().ToTable("MasaTungguKerjas");
            builder.Entity<TingkatTempatKerja>().ToTable("TingkatTempatKerjas");
            builder.Entity<PosisiJabatan>().ToTable("PosisiJabatans");
            builder.Entity<JenisTempatKerja>().ToTable("JenisTempatKerjas");
            builder.Entity<TahunAkademik>().ToTable("TahunAkademiks");
            builder.Entity<PrestasiMhs>().ToTable("PrestasiMhss");
            //-----------------------------------------------------------
            builder.Entity<TahunMasuk>().ToTable("TahunMasuks");
            builder.Entity<InfoMahasiswa>().ToTable("InfoMahasiswas");
            //------------------------------------------------------------
            builder.Entity<KegiatanPPM>().ToTable("KegiatanPPMS");
            builder.Entity<PegawaiKegiatanPPM>().ToTable("PegawaiKegiatanPPMS");

            // Konfigurasi validasi ketika Pegawai disimpan
        }
        public override int SaveChanges()
        {
            foreach (var entry in ChangeTracker.Entries<Pegawai>())
            {
                if (entry.State == EntityState.Added || entry.State == EntityState.Modified)
                {
                    if (entry.Entity.Jenis_Pegawai == JenisP.Dosen && !(entry.Entity is Dosen))
                    {
                        throw new InvalidOperationException("Pegawai dengan Jenis_Pegawai 'Dosen' harus memiliki relasi dengan entitas Dosen.");
                    }

                    if (entry.Entity.Jenis_Pegawai == JenisP.Tendik && !(entry.Entity is Tendik))
                    {
                        throw new InvalidOperationException("Pegawai dengan Jenis_Pegawai 'Tendik' harus memiliki relasi dengan entitas Tendik.");
                    }
                }
            }
            return base.SaveChanges();
        }
    }
}