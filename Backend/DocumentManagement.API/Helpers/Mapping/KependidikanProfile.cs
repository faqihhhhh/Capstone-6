using AutoMapper;
using DocumentManagement.Data.Dto;
using DocumentManagement.Data;
using System;
using DocumentManagement.Data.Entities;

namespace DocumentManagement.API.Helpers.Mapping
{
    public class KependidikanProfile : Profile
    {
        public KependidikanProfile() 
        {
            // Maping ddari DTO ke Entity --> untuk method POST
            
            CreateMap<CreateKependidikanDTO, TahunAkademik>()
                .ForMember(dest => dest.ThnAkademik, opt => opt.MapFrom(src => src.ThnAkademik))
                .ForMember(dest => dest.Semester, opt => opt.MapFrom(src => src.Semester));

            CreateMap<CreateKependidikanDTO, TahunMasuk>()
                .ForMember(dest => dest.ThnMasuk, opt => opt.MapFrom(src => src.ThnMasuk))
                .ForMember(dest => dest.TotalRegistrasiMhs, opt => opt.MapFrom(src => src.TotalRegistrasiMhs));

            CreateMap<CreateKependidikanDTO, InfoMahasiswa>()
                .ForMember(dest => dest.JmlhLulus, opt => opt.MapFrom(src => src.JmlhLulus))
                .ForMember(dest => dest.JmlhNonAktif, opt => opt.MapFrom(src => src.JmlhNonAktif))
                .ForMember(dest => dest.JmlhDO, opt => opt.MapFrom(src => src.JmlhDO))
                .ForMember(dest => dest.JmlhUndurDiri, opt => opt.MapFrom(src => src.JmlhUndurDiri))
                .ForMember(dest => dest.RataanIPKTotal, opt => opt.MapFrom(src => src.RataanIPKTotal))
                .ForMember(dest => dest.JumlahIPKDibawah2, opt => opt.MapFrom(src => src.JumlahIPKDibawah2))
                .ForMember(dest => dest.MasaStudiDibawah8, opt => opt.MapFrom(src => src.MasaStudiDibawah8))
                .ForMember(dest => dest.MasaStudi8Sampai10, opt => opt.MapFrom(src => src.MasaStudi8Sampai10))
                .ForMember(dest => dest.MasaStudiDiatas10, opt => opt.MapFrom(src => src.MasaStudiDiatas10))
                .ForMember(dest => dest.JumlhMBKM, opt => opt.MapFrom(src => src.JumlhMBKM));

            // Maping dari Entity ke DTO --> untuk Method GET
            CreateMap<TahunAkademik, GetKependidikanDTO>()
                .ForMember(dest => dest.ThnAkademik, opt => opt.MapFrom(src => src.ThnAkademik))
                .ForMember(dest => dest.Semester, opt => opt.MapFrom(src => src.Semester))
                .ForMember(dest => dest.Deskripsi, opt => opt.MapFrom(src => $"{src.ThnAkademik}/{src.ThnAkademik + 1}-{src.Semester}"));

            CreateMap<TahunMasuk, GetKependidikanDTO>()
                .ForMember(dest => dest.ThnMasuk, opt => opt.MapFrom(src => src.ThnMasuk))
                .ForMember(dest => dest.TotalRegistrasiMhs, opt => opt.MapFrom(src => src.TotalRegistrasiMhs));

            CreateMap<InfoMahasiswa, InfoMahasiswaDTO>()
                .ForMember(dest => dest.JmlhAktif, opt => opt.MapFrom(src => src.JmlhAktif))
                .ForMember(dest => dest.JmlhLulus, opt => opt.MapFrom(src => src.JmlhLulus))
                .ForMember(dest => dest.JmlhNonAktif, opt => opt.MapFrom(src => src.JmlhNonAktif))
                .ForMember(dest => dest.JmlhDO, opt => opt.MapFrom(src => src.JmlhDO))
                .ForMember(dest => dest.JmlhUndurDiri, opt => opt.MapFrom(src => src.JmlhUndurDiri))
                .ForMember(dest => dest.RataanIPKTotal, opt => opt.MapFrom(src => src.RataanIPKTotal))
                .ForMember(dest => dest.JumlahIPKDibawah2, opt => opt.MapFrom(src => src.JumlahIPKDibawah2))
                .ForMember(dest => dest.MasaStudiDibawah8, opt => opt.MapFrom(src => src.MasaStudiDibawah8))
                .ForMember(dest => dest.MasaStudi8Sampai10, opt => opt.MapFrom(src => src.MasaStudi8Sampai10))
                .ForMember(dest => dest.MasaStudiDiatas10, opt => opt.MapFrom(src => src.MasaStudiDiatas10));

            // Mapping untuk UpdateKependidikanDTO ke InfoMahasiswa
            CreateMap<UpdateKependidikanDTO, InfoMahasiswa>()
                .ForMember(dest => dest.JmlhAktif, opt => opt.MapFrom(src => src.JmlhAktif))
                .ForMember(dest => dest.JmlhLulus, opt => opt.MapFrom(src => src.JmlhLulus))
                .ForMember(dest => dest.JmlhNonAktif, opt => opt.MapFrom(src => src.JmlhNonAktif))
                .ForMember(dest => dest.JmlhDO, opt => opt.MapFrom(src => src.JmlhDO))
                .ForMember(dest => dest.JmlhUndurDiri, opt => opt.MapFrom(src => src.JmlhUndurDiri))
                .ForMember(dest => dest.RataanIPKTotal, opt => opt.MapFrom(src => src.RataanIPKTotal))
                .ForMember(dest => dest.JumlahIPKDibawah2, opt => opt.MapFrom(src => src.JumlahIPKDibawah2))
                .ForMember(dest => dest.MasaStudiDibawah8, opt => opt.MapFrom(src => src.MasaStudiDibawah8))
                .ForMember(dest => dest.MasaStudi8Sampai10, opt => opt.MapFrom(src => src.MasaStudi8Sampai10))
                .ForMember(dest => dest.MasaStudiDiatas10, opt => opt.MapFrom(src => src.MasaStudiDiatas10));
        }
    }
}