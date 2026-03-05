using AutoMapper;
using DocumentManagement.Data.Dto;
using DocumentManagement.Data;
using System;
using DocumentManagement.Data.Entities;

namespace DocumentManagement.API.Helpers.Mapping
{
    public class KemahasiswaanProfile: Profile
    {
        public KemahasiswaanProfile()
        {
            //----------------------------- Untuk Tracer Study------------------------------//
            // Mapping dari TahunLulusWithStatusDTO ke TahunLulus entity
            CreateMap<TahunLulusWithTracerDTO, TahunLulus>()
                .ForMember(dest => dest.StatusLulusan, opt => opt.MapFrom(src => src.StatusLulusan));

            // Mapping dari StatusLulusanDTO ke entity StatusLulusan
            CreateMap<StatusLulusanDTO, StatusLulusan>()
                .ForMember(dest => dest.TahunId, opt => opt.Ignore())  // TahunId akan diatur otomatis
                .ForMember(dest => dest.MasaTungguKerja, opt => opt.MapFrom(src => src.MasaTungguKerja))
                .ForMember(dest => dest.JenisTempatKerja, opt => opt.MapFrom(src => src.JenisTempatKerja))
                .ForMember(dest => dest.PosisiJabatan, opt => opt.MapFrom(src => src.PosisiJabatan))
                .ForMember(dest => dest.TingkatTempatKerja, opt => opt.MapFrom(src => src.TingkatTempatKerja));

            CreateMap<StatusLulusanUpdateDTO, StatusLulusan>()
                .ForMember(dest => dest.MasaTungguKerja, opt => opt.MapFrom(src => src.MasaTungguKerja))
                .ForMember(dest => dest.PosisiJabatan, opt => opt.MapFrom(src => src.PosisiJabatan))
                .ForMember(dest => dest.JenisTempatKerja, opt => opt.MapFrom(src => src.JenisTempatKerja))
                .ForMember(dest => dest.TingkatTempatKerja, opt => opt.MapFrom(src => src.TingkatTempatKerja));

            // Mapping dari DTO masing-masing ke entity terkait
            CreateMap<MasaTungguKerjaDTO, MasaTungguKerja>();
            CreateMap<JenisTempatKerjaDTO, JenisTempatKerja>();
            CreateMap<PosisiJabatanDTO, PosisiJabatan>();
            CreateMap<TingkatTempatKerjaDTO, TingkatTempatKerja>();

            // Maping dari Entity ke DTO
            // Mapping dari TahunLulus ke TahunLulusWithStatusDTO
            CreateMap<TahunLulus, GetAllTahunLulusDTO>()
                .ForMember(dest => dest.StatusLulusan, opt => opt.MapFrom(src => src.StatusLulusan));

            // Mapping dari StatusLulusan ke StatusLulusanDTO, termasuk nilai persentase
            CreateMap<StatusLulusan, GetAllStatusLulusanDTO>()
                .ForMember(dest => dest.StatusId, opt => opt.MapFrom(src => src.StatusId))
                .ForMember(dest => dest.Tahun_Input, opt => opt.MapFrom(src => src.Tahun_Input))
                .ForMember(dest => dest.Presentase_Bekerja, opt => opt.MapFrom(src => src.Presentase_Bekerja))
                .ForMember(dest => dest.Presentase_LanjutStudi, opt => opt.MapFrom(src => src.Presentase_LanjutStudi))
                .ForMember(dest => dest.Presentase_Wirausaha, opt => opt.MapFrom(src => src.Presentase_Wirausaha))
                .ForMember(dest => dest.Presentase_Internship, opt => opt.MapFrom(src => src.Presentase_Internship))
                .ForMember(dest => dest.Presentase_BelumKerja, opt => opt.MapFrom(src => src.Presentase_BelumKerja))
                .ForMember(dest => dest.MasaTungguKerja, opt => opt.MapFrom(src => src.MasaTungguKerja))
                .ForMember(dest => dest.PosisiJabatan, opt => opt.MapFrom(src => src.PosisiJabatan))
                .ForMember(dest => dest.JenisTempatKerja, opt => opt.MapFrom(src => src.JenisTempatKerja))
                .ForMember(dest => dest.TingkatTempatKerja, opt => opt.MapFrom(src => src.TingkatTempatKerja));
            
            CreateMap<MasaTungguKerja, GetAllMasaTungguKerjaDTO>()
                .ForMember(dest => dest.Presentase_diatas6Bulan, opt => opt.MapFrom(src => src.Presentase_diatas6Bulan))
                .ForMember(dest => dest.Presentase_dibawah6Bulan, opt => opt.MapFrom(src => src.Presentase_dibawah6Bulan));
            
            CreateMap<PosisiJabatan, GetAllPosisiJabatanDTO>()
                .ForMember(dest => dest.Presentase_Founder, opt => opt.MapFrom(src => src.Presentase_Founder))
                .ForMember(dest => dest.Presentase_CoFounder, opt => opt.MapFrom(src => src.Presentase_CoFounder))
                .ForMember(dest => dest.Presentase_Staf, opt => opt.MapFrom(src => src.Presentase_Staf))
                .ForMember(dest => dest.Presentase_Freelancer, opt => opt.MapFrom(src => src.Presentase_Freelancer));
            
            CreateMap<JenisTempatKerja, GetAllJenisTempatKerjaDTO>()
                .ForMember(dest => dest.Presentase_BUMN, opt => opt.MapFrom(src => src.Presentase_BUMN))
                .ForMember(dest => dest.Presentase_Organisasi_Multilateral, opt => opt.MapFrom(src => src.Presentase_Organisasi_Multilateral))
                .ForMember(dest => dest.Presentase_Instansi_Pemerintah, opt => opt.MapFrom(src => src.Presentase_Instansi_Pemerintah))
                .ForMember(dest => dest.Presentase_Organisasi_NonProfit, opt => opt.MapFrom(src => src.Presentase_Organisasi_NonProfit))
                .ForMember(dest => dest.Presentase_Wirausaha, opt => opt.MapFrom(src => src.Presentase_Wirausaha))
                .ForMember(dest => dest.Presentase_Lainnya, opt => opt.MapFrom(src => src.Presentase_Lainnya));
            
            CreateMap<TingkatTempatKerja, GetAllTingkatTempatKerjaDTO>()
                .ForMember(dest => dest.Presentase_Lokal, opt => opt.MapFrom(src => src.Presentase_Lokal))
                .ForMember(dest => dest.Presentase_Nasional, opt => opt.MapFrom(src => src.Presentase_Nasional))
                .ForMember(dest => dest.Presentase_MultiNasional, opt => opt.MapFrom(src => src.Presentase_MultiNasional));
            //---------------------------------------------------------------------------------------------------------//

            //-------------------------------------- Untuk Prestasi Mahasiswa -------------------------------------------//
            // Mapping dari TahunAkademikDTO ke TahunAkademik entity
            CreateMap<CreatePrestasiDTO, TahunAkademik>()
                .ForMember(dest => dest.PrestasiMhs, opt => opt.MapFrom(src => src.PrestasiMhs))
                .ForMember(dest => dest.ThnAkademikId, opt => opt.Ignore())
                .ForMember(dest => dest.ThnAkademik, opt => opt.MapFrom(src => src.ThnAkademik))
                .ForMember(dest => dest.Semester, opt => opt.MapFrom(src => src.Semester))
                ;
            
            CreateMap<PrestasiMhsDTO, PrestasiMhs>()
                .ForMember(dest => dest.ThnAkademikId, opt => opt.Ignore())
                .ForMember(dest => dest.JmlhPKM, opt => opt.MapFrom(src => src.JmlhPKM))
                .ForMember(dest => dest.JumlhMapres, opt => opt.MapFrom(src => src.JumlhMapres))
                .ForMember(dest => dest.JumlhLombaInter, opt => opt.MapFrom(src => src.JumlhLombaInter))
                .ForMember(dest => dest.JumlhLombaNasional, opt => opt.MapFrom(src => src.JumlhLombaNasional))
                .ForMember(dest => dest.JumlhInbound, opt => opt.MapFrom(src => src.JumlhInbound))
                .ForMember(dest => dest.JumlhOutbound, opt => opt.MapFrom(src => src.JumlhOutbound));

            CreateMap<TahunAkademik, CreatePrestasiDTO>()
                .ForMember(dest => dest.PrestasiMhs, opt => opt.MapFrom(src => src.PrestasiMhs))
                .ForMember(dest => dest.ThnAkademik, opt => opt.MapFrom(src => src.ThnAkademik))
                .ForMember(dest => dest.Semester, opt => opt.MapFrom(src => src.Semester));

            CreateMap<PrestasiMhs, PrestasiMhsDTO>()
                .ForMember(dest => dest.JmlhPKM, opt => opt.MapFrom(src => src.JmlhPKM))
                .ForMember(dest => dest.JumlhMapres, opt => opt.MapFrom(src => src.JumlhMapres))
                .ForMember(dest => dest.JumlhLombaNasional, opt => opt.MapFrom(src => src.JumlhLombaNasional))
                .ForMember(dest => dest.JumlhLombaInter, opt => opt.MapFrom(src => src.JumlhLombaInter))
                .ForMember(dest => dest.JumlhInbound, opt => opt.MapFrom(src => src.JumlhInbound))
                .ForMember(dest => dest.JumlhOutbound, opt => opt.MapFrom(src => src.JumlhOutbound));

            // Mapping dari  Entitas-entitas (TahunAKademik dan Prestasi) ke DTO (GetTahunAkademikDTO dan GetPrestasiMhsDTO)

            // Mapping dari TahunAkademik ke GetTahunAkademikDTO
            CreateMap<TahunAkademik, GetAllPrestasiDTO>()
                .ForMember(dest => dest.GetPrestasiMhs, opt => opt.MapFrom(src => src.PrestasiMhs));

            // Mapping dari PrestasiMhs ke GetPrestasiMhsDTO
            CreateMap<PrestasiMhs, GetPrestasiMhsDTO>();

            // Mapping untuk UPDATE dari DTO ke Entity PrestasiMhs
            CreateMap<UpdatePrestasiMhsDTO, PrestasiMhs>()
                .ForMember(dest => dest.ThnAkademikId, opt => opt.Ignore());

            // Tidak ada mapping khusus untuk DELETE, karena hanya ID yang diperlukan.
        }
    }
}