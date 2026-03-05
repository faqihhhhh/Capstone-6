using AutoMapper;
using DocumentManagement.Data.Dto;
using DocumentManagement.Data;
using System;

namespace DocumentManagement.API.Helpers.Mapping
{
    public class KepegawaianProfile : Profile
    {
        
        public KepegawaianProfile() 
        {
            // Mapping PegawaiDTO ke Entitas Pegawai
            CreateMap<PegawaiDTO, Pegawai>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id ?? Guid.NewGuid()))  // Generate ID baru jika null
                .ForMember(dest => dest.NIP, opt => opt.MapFrom(src => src.NIP))
                .ForMember(dest => dest.Nama, opt => opt.MapFrom(src => src.Nama))
                .ForMember(dest => dest.Tanggal_Lahir, opt => opt.MapFrom(src => src.Tanggal_Lahir))
                .ForMember(dest => dest.Tempat_Lahir, opt => opt.MapFrom(src => src.Tempat_Lahir))
                .ForMember(dest => dest.Jenis_Kelamin, opt => opt.MapFrom(src => src.Jenis_Kelamin))
                .ForMember(dest => dest.Alamat, opt => opt.MapFrom(src => src.Alamat))
                .ForMember(dest => dest.Telepon_Darurat, opt => opt.MapFrom(src => src.Telepon_Darurat))
                .ForMember(dest => dest.Pendidikan_Terakhir, opt => opt.MapFrom(src => src.Pendidikan_Terakhir))
                .ForMember(dest => dest.Gelar, opt => opt.MapFrom(src => src.Gelar))
                .ForMember(dest => dest.Pendidikan_Terakhir, opt => opt.MapFrom(src => src.Pendidikan_Terakhir)).ForMember(dest => dest.Status_Pernikahan, opt => opt.MapFrom(src => src.Status_Pernikahan))
                .ForMember(dest => dest.Nomor_BPJS, opt => opt.MapFrom(src => src.Nomor_BPJS))
                .ForMember(dest => dest.Nomor_NPWP, opt => opt.MapFrom(src => src.Nomor_NPWP))
                .ForMember(dest => dest.Nomor_Paspor, opt => opt.MapFrom(src => src.Nomor_Paspor))
                .ForMember(dest => dest.Jenis_Pegawai, opt => opt.MapFrom(src => src.Jenis_Pegawai));// Tambahkan mapping Jenis_Pegawai

            // Pemetaan PegawaiDTO ke Dosen
            CreateMap<PegawaiDTO, Dosen>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))  // ID Dosen sama dengan PegawaiId
                .ForMember(dest => dest.PegawaiId, opt => opt.MapFrom(src => src.Id))  // PegawaiId sebagai FK dari Pegawai
                .ForMember(dest => dest.Jenis_Dosen, opt => opt.MapFrom(src => src.Jenis_Dosen));

            // Pemetaan PegawaiDTO ke Tendik
            CreateMap<PegawaiDTO, Tendik>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))  // ID Tendik sama dengan PegawaiId
                .ForMember(dest => dest.PegawaiId, opt => opt.MapFrom(src => src.Id))  // PegawaiId sebagai FK dari Pegawai
                .ForMember(dest => dest.Jenis_Tendik, opt => opt.MapFrom(src => src.Jenis_Tendik));

            // Pemetaan DosenTetapDTO ke DosenTetap
            CreateMap<DosenTetapDTO, DosenTetap>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.DosenId))  // ID DosenTetap sama dengan DosenId (FK)
                .ForMember(dest => dest.DosenId, opt => opt.MapFrom(src => src.DosenId))  // DosenId sebagai FK dari Dosen
                .ForMember(dest => dest.NIDN, opt => opt.MapFrom(src => src.NIDN))
                .ForMember(dest => dest.Jabatan_Akademik, opt => opt.MapFrom(src => src.Jabatan_Akademik))
                .ForMember(dest => dest.Golongan, opt => opt.MapFrom(src => src.Golongan))
                .ForMember(dest => dest.TMT, opt => opt.MapFrom(src => src.TMT))
                .ForMember(dest => dest.Nomor_Serdos, opt => opt.MapFrom(src => src.Nomor_Serdos));

            // Pemetaan TendikTetapDTO ke TendikTetap
            CreateMap<TendikTetapDTO, TendikTetap>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.TendikId))  // ID TendikTetap sama dengan TendikId (FK)
                .ForMember(dest => dest.TendikId, opt => opt.MapFrom(src => src.TendikId))  // TendikId sebagai FK dari Tendik
                .ForMember(dest => dest.Jabatan, opt => opt.MapFrom(src => src.Jabatan))
                .ForMember(dest => dest.TMT, opt => opt.MapFrom(src => src.TMT))
                .ForMember(dest => dest.Golongan, opt => opt.MapFrom(src => src.Golongan));

            // Maping between Pegawai, dan Dosen to DosenDetailDTO (1 arah)
            CreateMap<(Pegawai, Dosen), DosenDetailDTO>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Item1.Id))
            .ForMember(dest => dest.NIP, opt => opt.MapFrom(src => src.Item1.NIP))
            .ForMember(dest => dest.Nama, opt => opt.MapFrom(src => src.Item1.Nama))
            .ForMember(dest => dest.Tanggal_Lahir, opt => opt.MapFrom(src => src.Item1.Tanggal_Lahir))
            .ForMember(dest => dest.Tempat_Lahir, opt => opt.MapFrom(src => src.Item1.Tempat_Lahir))
            .ForMember(dest => dest.Jenis_Kelamin, opt => opt.MapFrom(src => src.Item1.Jenis_Kelamin.ToString())) // Enum to string
            .ForMember(dest => dest.Alamat, opt => opt.MapFrom(src => src.Item1.Alamat))
            .ForMember(dest => dest.Telepon_Darurat, opt => opt.MapFrom(src => src.Item1.Telepon_Darurat))
            .ForMember(dest => dest.Pendidikan_Terakhir, opt => opt.MapFrom(src => src.Item1.Pendidikan_Terakhir.ToString())) // Enum to string
            .ForMember(dest => dest.Gelar, opt => opt.MapFrom(src => src.Item1.Gelar))
            .ForMember(dest => dest.Status_Pernikahan, opt => opt.MapFrom(src => src.Item1.Status_Pernikahan.ToString())) // Enum to string
            .ForMember(dest => dest.Nomor_BPJS, opt => opt.MapFrom(src => src.Item1.Nomor_BPJS))
            .ForMember(dest => dest.Nomor_NPWP, opt => opt.MapFrom(src => src.Item1.Nomor_NPWP))
            .ForMember(dest => dest.Nomor_Paspor, opt => opt.MapFrom(src => src.Item1.Nomor_Paspor))
            // Dari Dosen
            .ForMember(dest => dest.Jenis_Dosen, opt => opt.MapFrom(src => src.Item2.Jenis_Dosen.ToString())) // Enum to string dari Dosen
            .ForMember(dest => dest.DosenTetap, opt =>
             {
                 opt.MapFrom(src => src.Item2.DosenTetap);
                 opt.Condition(src => src.Item2.Jenis_Dosen == Status_Dosen.PNS || src.Item2.Jenis_Dosen == Status_Dosen.TetapNonPNS);
             });

            // Mapping antara DosenTetap ke DosenTetapDetailDTO
            CreateMap<DosenTetap, DosenTetapDetailDTO>()
                .ForMember(dest => dest.NIDN, opt => opt.MapFrom(src => src.NIDN))
                .ForMember(dest => dest.Jabatan_Akademik, opt => opt.MapFrom(src => src.Jabatan_Akademik.ToString())) // Enum to string
                .ForMember(dest => dest.Golongan, opt => opt.MapFrom(src => src.Golongan))
                .ForMember(dest => dest.Tanggal_Pensiun, opt => opt.MapFrom(src => src.Tanggal_Pensiun))
                .ForMember(dest => dest.Proyeksi, opt => opt.MapFrom(src => src.Proyeksi))
                .ForMember(dest => dest.TMT, opt => opt.MapFrom(src => src.TMT))
                .ForMember(dest => dest.Nomor_Serdos, opt => opt.MapFrom(src => src.Nomor_Serdos));

            // Maping between Pegawai, dan Tendik to TendikDetailDTO
            CreateMap<(Pegawai, Tendik), TendikDetailDTO>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Item1.Id))
            .ForMember(dest => dest.NIP, opt => opt.MapFrom(src => src.Item1.NIP)) // Dari Pegawai
            .ForMember(dest => dest.Nama, opt => opt.MapFrom(src => src.Item1.Nama)) // Dari Pegawai
            .ForMember(dest => dest.Tanggal_Lahir, opt => opt.MapFrom(src => src.Item1.Tanggal_Lahir)) // Dari Pegawai
            .ForMember(dest => dest.Tempat_Lahir, opt => opt.MapFrom(src => src.Item1.Tempat_Lahir)) // Dari Pegawai
            .ForMember(dest => dest.Jenis_Kelamin, opt => opt.MapFrom(src => src.Item1.Jenis_Kelamin.ToString())) // Enum ke string dari Pegawai
            .ForMember(dest => dest.Alamat, opt => opt.MapFrom(src => src.Item1.Alamat)) // Dari Pegawai
            .ForMember(dest => dest.Telepon_Darurat, opt => opt.MapFrom(src => src.Item1.Telepon_Darurat)) // Dari Pegawai
            .ForMember(dest => dest.Pendidikan_Terakhir, opt => opt.MapFrom(src => src.Item1.Pendidikan_Terakhir.ToString())) // Enum ke string dari Pegawai
            .ForMember(dest => dest.Gelar, opt => opt.MapFrom(src => src.Item1.Gelar))
            .ForMember(dest => dest.Status_Pernikahan, opt => opt.MapFrom(src => src.Item1.Status_Pernikahan.ToString())) // Enum ke string dari Pegawai
            .ForMember(dest => dest.Nomor_BPJS, opt => opt.MapFrom(src => src.Item1.Nomor_BPJS)) // Dari Pegawai
            .ForMember(dest => dest.Nomor_NPWP, opt => opt.MapFrom(src => src.Item1.Nomor_NPWP)) // Dari Pegawai
            .ForMember(dest => dest.Nomor_Paspor, opt => opt.MapFrom(src => src.Item1.Nomor_Paspor)) // Dari Pegawai
            // Dari Tendik
            .ForMember(dest => dest.Jenis_Tendik, opt => opt.MapFrom(src => src.Item2.Jenis_Tendik.ToString())) // Enum ke string dari Tendik
            //.ForMember(dest => dest.TendikTetap, opt => opt.MapFrom(src => src.Item2.TendikTetap ?? src.Item2.TendikTetap)); // Jika TendikTetap ada, petakan
            .ForMember(dest => dest.TendikTetap, opt =>
             {
                 opt.MapFrom(src => src.Item2.TendikTetap);
                 opt.Condition(src => src.Item2.Jenis_Tendik == Status_Tendik.PNS);
             });

            CreateMap<TendikTetap, TendikTetapDetailDTO>()
                .ForMember(dest => dest.Jabatan, opt => opt.MapFrom(src => src.Jabatan))           // Map Jabatan
                .ForMember(dest => dest.Golongan, opt => opt.MapFrom(src => src.Golongan))         // Map Golongan
                .ForMember(dest => dest.TMT, opt => opt.MapFrom(src => src.TMT))                   // Map TMT (Tanggal Mulai Tugas)
                .ForMember(dest => dest.Tanggal_Pensiun, opt => opt.MapFrom(src => src.Tanggal_Pensiun))  // Map Tanggal Pensiun
                .ForMember(dest => dest.Proyeksi, opt => opt.MapFrom(src => src.Proyeksi));        // Map Proyeksi
        }
    }
}