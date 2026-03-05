using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using DocumentManagement.Common.GenericRepository;
using DocumentManagement.Common.UnitOfWork;
using DocumentManagement.Data;
using DocumentManagement.Data.Dto;
using DocumentManagement.Data.Entities;
using DocumentManagement.Domain;
using DocumentManagement.Helper;
using Microsoft.EntityFrameworkCore;
using System.Linq;

using AutoMapper.QueryableExtensions;


namespace DocumentManagement.Repository
{
    public class KegiatanPPMRepository : GenericRepository<KegiatanPPM, DocumentContext>, IKegiatanPPMRepository
    {
        private readonly IMapper _mapper;
        public KegiatanPPMRepository(
            IUnitOfWork<DocumentContext> uow, IMapper mapper
                ) : base(uow)
        {
            _mapper = mapper;
        }
        public async Task<ServiceResponse<CreateKegiatanPPMDTO>> AddKegiatanPPMAsync(CreateKegiatanPPMDTO dto)
        {
            var isDuplicate = await Context.KegiatanPPMs
                .AnyAsync(k => k.JudulPPM == dto.JudulPPM && k.TahunMulai == dto.TahunMulai);

            if (isDuplicate)
            {
                return ServiceResponse<CreateKegiatanPPMDTO>.Return409("Data dengan judul dan tahun mulai yang sama sudah ada.");
            }
            // Mapping dari DTO ke entitas KegiatanPPM
            var kegiatanPPM = _mapper.Map<KegiatanPPM>(dto);

            // Tambahkan Entitas Kegiatan PPM ke Database
            Context.KegiatanPPMs.Add(kegiatanPPM);
            var saveResult = await Context.SaveChangesAsync() > 0;

            if (saveResult)
            {
                // Map kembali ke DTO untuk dikembalikan
                return ServiceResponse<CreateKegiatanPPMDTO>.ReturnResultWith200(dto);
            }
            else
            {
                return ServiceResponse<CreateKegiatanPPMDTO>
                    .Return404();
            }
        }

        public async Task<ServiceResponse<CreatePegawaiKegiatanPPMDTO>> AddPegawaiKegiatanPPMAsync(CreatePegawaiKegiatanPPMDTO dto)
        {
            // Cek apakah Pegawai dan KegiatanPPM ada
            var pegawaiExists = await Context.Pegawais.AnyAsync(p => p.Id == dto.PegawaiID);
            var kegiatanExists = await Context.KegiatanPPMs.AnyAsync(k => k.KegiatanID == dto.KegiatanID);
            if (!pegawaiExists || !kegiatanExists)
            {
                return ServiceResponse<CreatePegawaiKegiatanPPMDTO>.Return409("Data Pegawai Dosen Tidak ditemukan");
            }
            // Cek apakah relasi sudah ada
            var existingRelation = await Context.PegawaiKegiatanPPMs
                .AnyAsync(pk => pk.PegawaiId == dto.PegawaiID && pk.KegiatanID == dto.KegiatanID);
            if (existingRelation)
            {
                return ServiceResponse<CreatePegawaiKegiatanPPMDTO>
                    .Return404();
            }
            // Mapping DTO ke entitas PegawaiKegiatanPPM
            var pegawaiKegiatanPPM = _mapper.Map<PegawaiKegiatanPPM>(dto);
            Context.PegawaiKegiatanPPMs.Add(pegawaiKegiatanPPM);
            var saveResult = await Context.SaveChangesAsync() > 0;
            if (saveResult)
            {
                // Map kembali ke DTO untuk dikembalikan
                return ServiceResponse<CreatePegawaiKegiatanPPMDTO>.ReturnResultWith200(dto);
            }
            else
            {
                return ServiceResponse<CreatePegawaiKegiatanPPMDTO>
                    .Return404();
            }
        }

        public async Task<IEnumerable<PegawaiKegiatanPPMResponseDTO>> GetAllPegawaiKegiatanPPMAsync()
        {
            return await Context.PegawaiKegiatanPPMs
                .Include(pk => pk.Pegawai)
                .Include(pk => pk.KegiatanPPM)
                .ProjectTo<PegawaiKegiatanPPMResponseDTO>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }
        public async Task<List<KegiatanPPMListDTO>> GetAllKegiatanPPMAsync()
        {
            return await Context.KegiatanPPMs
                .AsNoTracking()
                .ProjectTo<KegiatanPPMListDTO>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }
        //Hanya untuk data selama 5 tahun
        public async Task<List<RingkasanPengabdianPenelitianDTO>> GetRingkasanPengabdianPenelitianAsync()
        {
            var fiveYearsAgo = DateTime.Now.Year - 5;

            return await Context.PegawaiKegiatanPPMs
                .Include(pk => pk.Pegawai)
                .Include(pk => pk.KegiatanPPM)
                .Where(pk => pk.KegiatanPPM.TahunMulai >= fiveYearsAgo) // Filter 5 tahun terakhir
                .GroupBy(pk => new { pk.Pegawai.Id, pk.Pegawai.Nama, pk.Pegawai.NIP, pk.Pegawai.Dosen.Jenis_Dosen })
                .Select(g => new RingkasanPengabdianPenelitianDTO
                {
                    NamaPegawai = g.Key.Nama,
                    NIP = g.Key.NIP,
                    JenisDosen = g.Key.Jenis_Dosen.ToString(),
                    JumlahPenelitian = g.Count(pk => (int)pk.KegiatanPPM.JenisPPM == (int)Jenis_PPM.Penelitian),
                    JumlahPengabdian = g.Count(pk => (int)pk.KegiatanPPM.JenisPPM == (int)Jenis_PPM.Pengabdian)
                })
                .ToListAsync();
        }
        //Hanya untuk data selama 5 tahun
        public async Task<List<MitraPenelitianDTO>> GetMitraPenelitianAsync()
        {
            var fiveYearsAgo = DateTime.Now.Year - 5;

            return await Context.KegiatanPPMs
                .Where(k => (int)k.JenisPPM == (int)Jenis_PPM.Penelitian &&
                            (int)k.MitraPPM != (int)Mitra_PPM.NonMitra &&
                            k.TahunMulai >= fiveYearsAgo) // Filter 5 tahun terakhir
                .GroupBy(k => k.TahunMulai)
                .Select(g => new MitraPenelitianDTO
                {
                    Tahun = g.Key,
                    JumlahMitraPemerintah = g.Count(k => (int)k.MitraPPM == (int)Mitra_PPM.Pemerintah),
                    JumlahMitraSwasta = g.Count(k => (int)k.MitraPPM == (int)Mitra_PPM.Swasta),
                    JumlahMitraLuarNegeri = g.Count(k => (int)k.MitraPPM == (int)Mitra_PPM.LuarNegeri),
                    TotalDana = g.Sum(k => k.DanaPPM)
                })
                .Where(dto => dto.JumlahMitraPemerintah > 0 || dto.JumlahMitraSwasta > 0 || dto.JumlahMitraLuarNegeri > 0)
                .ToListAsync();
        }
        //Hanya untuk data selama 5 tahun
        public async Task<List<MitraPengabdianDTO>> GetMitraPengabdianAsync()
        {
            var fiveYearsAgo = DateTime.Now.Year - 5;

            return await Context.KegiatanPPMs
                .Where(k => (int)k.JenisPPM == (int)Jenis_PPM.Pengabdian &&
                            (int)k.MitraPPM != (int)Mitra_PPM.NonMitra &&
                            k.TahunMulai >= fiveYearsAgo) // Filter 5 tahun terakhir
                .GroupBy(k => k.TahunMulai)
                .Select(g => new MitraPengabdianDTO
                {
                    Tahun = g.Key,
                    JumlahMitraPemerintah = g.Count(k => (int)k.MitraPPM == (int)Mitra_PPM.Pemerintah),
                    JumlahMitraSwasta = g.Count(k => (int)k.MitraPPM == (int)Mitra_PPM.Swasta),
                    JumlahMitraLuarNegeri = g.Count(k => (int)k.MitraPPM == (int)Mitra_PPM.LuarNegeri),
                    TotalDana = g.Sum(k => k.DanaPPM)
                })
                .Where(dto => dto.JumlahMitraPemerintah > 0 || dto.JumlahMitraSwasta > 0 || dto.JumlahMitraLuarNegeri > 0)
                .ToListAsync();
        }
        public async Task<bool> DeleteKegiatanPPMAsync(Guid kegiatanId)
        {
            var kegiatanPPM = await Context.KegiatanPPMs.FindAsync(kegiatanId);
            if (kegiatanPPM == null)
            {
                return false; // Data tidak ditemukan
            }

            Context.KegiatanPPMs.Remove(kegiatanPPM);
            await Context.SaveChangesAsync();
            return true; // Data berhasil dihapus
        }

        public async Task<bool> UpdateKegiatanPPMAsync(Guid kegiatanId, UpdateKegiatanPPMDTO updateDto)
        {
            var kegiatanPPM = await Context.KegiatanPPMs.FindAsync(kegiatanId);
            if (kegiatanPPM == null)
            {
                return false; // Data tidak ditemukan
            }

            // Map perubahan dari DTO ke entity
            _mapper.Map(updateDto, kegiatanPPM);

            Context.KegiatanPPMs.Update(kegiatanPPM);
            await Context.SaveChangesAsync();
            return true; // Data berhasil diperbarui
        }
    }
}
