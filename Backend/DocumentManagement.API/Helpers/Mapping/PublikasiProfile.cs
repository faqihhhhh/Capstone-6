using AutoMapper;
using DocumentManagement.Data.Dto;
using DocumentManagement.Data;
using System;
using DocumentManagement.Data.Entities;

namespace DocumentManagement.API.Helpers.Mapping
{
    public class PublikasiProfile : Profile
    {
        public PublikasiProfile()
        {
            CreateMap<CreatePublikasiDTO, Publikasi>();
            CreateMap<Publikasi, GetPublikasiDTO>()
            .ForMember(dest => dest.NIP, opt => opt.MapFrom(src => src.Pegawai.NIP))
            .ForMember(dest => dest.Nama, opt => opt.MapFrom(src => src.Pegawai.Nama));
            CreateMap<UpdatePublikasiDTO, Publikasi>()
            .ForMember(dest => dest.PublikasiId, opt => opt.Ignore()); // PublikasiId biasanya tidak diubah
        }
    }
}
