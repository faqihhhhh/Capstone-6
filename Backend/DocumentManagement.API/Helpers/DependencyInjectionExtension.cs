using DocumentManagement.API.Helpers;
using DocumentManagement.API.Helpers.Mapping;
using DocumentManagement.Common.UnitOfWork;
using DocumentManagement.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace DocumentManagement.Api.Helpers
{
    public static class DependencyInjectionExtension
    {
        public static void AddDependencyInjection(this IServiceCollection services)
        {
            services.AddScoped(typeof(IUnitOfWork<>), typeof(UnitOfWork<>));
            services.AddScoped<IPropertyMappingService, PropertyMappingService>();
            services.AddScoped<IScreenRepository, ScreenRepository>();
            services.AddScoped<IOperationRepository, OperationRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserRoleRepository, UserRoleRepository>();
            services.AddScoped<IUserClaimRepository, UserClaimRepository>();
            services.AddScoped<IRoleClaimRepository, RoleClaimRepository>();
            services.AddScoped<IScreenOperationRepository, ScreenOperationRepository>();
            services.AddScoped<IDocumentRepository, DocumentRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IDocumentRolePermissionRepository, DocumentRolePermissionRepository>();
            services.AddScoped<IDocumentUserPermissionRepository, DocumentUserPermissionRepository>();
            services.AddScoped<IDocumentAuditTrailRepository, DocumentAuditTrailRepository>();
            services.AddScoped<IUserNotificationRepository, UserNotificationRepository>();
            services.AddScoped<ILoginAuditRepository, LoginAuditRepository>();

            // Reminder
            services.AddScoped<IReminderNotificationRepository, ReminderNotificationRepository>();
            services.AddScoped<IReminderRepository, ReminderRepository>();
            services.AddScoped<IReminderUserRepository, ReminderUserRepository>();
            services.AddScoped<IReminderSchedulerRepository, ReminderSchedulerRepository>();
            services.AddScoped<IDailyReminderRepository, DailyReminderRepository>();
            services.AddScoped<IQuarterlyReminderRepository, QuarterlyReminderRepository>();
            services.AddScoped<IHalfYearlyReminderRepository, HalfYearlyReminderRepository>();
            services.AddScoped<IDocumentTokenRepository, DocumentTokenRepository>();
            services.AddScoped<IEmailSMTPSettingRepository, EmailSMTPSettingRepository>();
            services.AddScoped<ISendEmailRepository, SendEmailRepository>();
            services.AddSingleton<IConnectionMappingRepository, ConnectionMappingRepository>();

            services.AddScoped<IDocumentCommentRepository, DocumentCommentRepository>();

            services.AddScoped<IDocumentVersionRepository, DocumentVersionRepository>();
            services.AddScoped<IDocumentMetaDataRepository, DocumentMetaDataRepository>();

            //Tahun AKademik
            services.AddScoped<IAkademikTahunRepository, AkademikTahunRepository>();
            
            //Kepegawaian
            services.AddScoped<IPegawaiRepository, PegawaiRepository>();
            services.AddScoped<PegawaiService>();  // Register PegawaiService

            //Luaran Publikasi
            services.AddScoped<IPublikasiRepository, PublikasiRepository>();

            //KegiatanPPM
            services.AddScoped<IKegiatanPPMRepository, KegiatanPPMRepository>();

            //Kependidikan
            services.AddScoped<IKependidikanRepository, KependidikanRepository>();
            
            //Kemahasiswaan
            services.AddScoped<IStatusLulusanRepository, StatusLulusanRepository>();
            services.AddScoped<ITahunLulusRepository, TahunLulusRepository>();
            services.AddScoped<IPrestasiRepository, PrestasiRepository>();
            services.AddScoped<IAkademikTahunRepository, AkademikTahunRepository>();
            services.AddScoped<KemahasiswaanService>();

        }
    }
}
