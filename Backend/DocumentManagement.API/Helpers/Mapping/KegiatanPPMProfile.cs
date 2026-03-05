using AutoMapper;
using DocumentManagement.Data.Dto;
using DocumentManagement.Data;
using System;
using DocumentManagement.Data.Entities;

namespace DocumentManagement.API.Helpers.Mapping
{
    public class KegiatanPPMProfile : Profile
    {
        public KegiatanPPMProfile() 
        {
            // Mapping dari DTO (CreateKegiatanPPMDTO) ke entitas KegiatanPPM
            // --> untuk method POST (CREATE)
            CreateMap<CreateKegiatanPPMDTO, KegiatanPPM>();
            CreateMap<CreatePegawaiKegiatanPPMDTO, PegawaiKegiatanPPM>();

            // Mapping dari PegawaiKegiatanPPM ke PegawaiKegiatanPPMResponseDTO
            CreateMap<PegawaiKegiatanPPM, PegawaiKegiatanPPMResponseDTO>()
                .ForMember(dest => dest.NamaPegawai, opt => opt.MapFrom(src => src.Pegawai.Nama))
                .ForMember(dest => dest.NIP, opt => opt.MapFrom(src => src.Pegawai.NIP))
                .ForMember(dest => dest.JenisDosen, opt => opt.MapFrom(src => src.Pegawai.Dosen.Jenis_Dosen))
                .ForMember(dest => dest.JudulPPM, opt => opt.MapFrom(src => src.KegiatanPPM.JudulPPM))
                .ForMember(dest => dest.TahunMulai, opt => opt.MapFrom(src => src.KegiatanPPM.TahunMulai))
                .ForMember(dest => dest.TahunSelesai, opt => opt.MapFrom(src => src.KegiatanPPM.TahunSelesai))
                .ForMember(dest => dest.JenisPPM, opt => opt.MapFrom(src => src.KegiatanPPM.JenisPPM))
                .ForMember(dest => dest.MitraPPM, opt => opt.MapFrom(src => src.KegiatanPPM.MitraPPM))
                .ForMember(dest => dest.NomorKontrak, opt => opt.MapFrom(src => src.KegiatanPPM.NomorKontrak))
                .ForMember(dest => dest.DanaPPM, opt => opt.MapFrom(src => src.KegiatanPPM.DanaPPM));

            CreateMap<KegiatanPPM, KegiatanPPMListDTO>();

            // Get Data Ringkasan
            CreateMap<PegawaiKegiatanPPM, RingkasanPengabdianPenelitianDTO>()
                .ForMember(dest => dest.NamaPegawai, opt => opt.MapFrom(src => src.Pegawai.Nama))
                .ForMember(dest => dest.NIP, opt => opt.MapFrom(src => src.Pegawai.NIP))
                .ForMember(dest => dest.JenisDosen, opt => opt.MapFrom(src => src.Pegawai.Dosen.Jenis_Dosen));

            // Mapping untuk MitraPenelitianDTO dan MitraPengabdianDTO
            CreateMap<KegiatanPPM, MitraPenelitianDTO>()
                .ForMember(dest => dest.Tahun, opt => opt.MapFrom(src => src.TahunMulai))
                .ForMember(dest => dest.TotalDana, opt => opt.MapFrom(src => src.DanaPPM));

            // Mapping untuk MitraPengabdianDTO
            CreateMap<KegiatanPPM, MitraPengabdianDTO>()
                .ForMember(dest => dest.Tahun, opt => opt.MapFrom(src => src.TahunMulai))
                .ForMember(dest => dest.TotalDana, opt => opt.MapFrom(src => src.DanaPPM));

            //Update KegiatanPPM
            // Mapping dari UpdateKegiatanPPMDTO ke KegiatanPPM untuk operasi update
            CreateMap<UpdateKegiatanPPMDTO, KegiatanPPM>()
                .ForMember(dest => dest.KegiatanID, opt => opt.Ignore()); // Ignore KegiatanID saat update
        }
    }
}
