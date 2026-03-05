using AutoMapper;
using DocumentManagement.Data.Dto;
using DocumentManagement.Data;
using System;
using DocumentManagement.Data.Entities;

namespace DocumentManagement.API.Helpers.Mapping
{
    public class TahunAkademikProfile : Profile
    {
        public TahunAkademikProfile()
        {
            CreateMap<CreateTahunAkademikDTO, TahunAkademik>();

            //Dari Entitas Ke DTO (dari DB ke User)
            CreateMap<TahunAkademik, GetTahunAkademikDTO>()
            .ForMember(dest => dest.Deskripsi, opt => opt.MapFrom(src => $"{src.ThnAkademik}/{src.ThnAkademik + 1}-{src.Semester}"));
        }
    }
}
