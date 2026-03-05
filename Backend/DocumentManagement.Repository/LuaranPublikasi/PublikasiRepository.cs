using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
using AutoMapper.QueryableExtensions;

namespace DocumentManagement.Repository
{
    public class PublikasiRepository : GenericRepository<Publikasi, DocumentContext>, IPublikasiRepository
    {
        private readonly IMapper _mapper;
        public PublikasiRepository(
            IUnitOfWork<DocumentContext> uow, IMapper mapper
                ) : base(uow)
        {
            _mapper = mapper;
        }

        public async Task<ServiceResponse<CreatePublikasiDTO>> AddPublikasiAsync(CreatePublikasiDTO publikasiDTO)
        {
            // Cek apakah Pegawai dengan ID yang diberikan ada
            bool pegawaiExists = await Context.Pegawais.AnyAsync(p => p.Id == publikasiDTO.PegawaiId);

            if (!pegawaiExists)
            {
                return ServiceResponse<CreatePublikasiDTO>
                    .Return409("Data Pegawai Dosen Tidak ditemukan");
            }

            //Mapping dari DTO ke Entitas Publikasi
            var luaranPublikasi = _mapper.Map<Publikasi>( publikasiDTO );

            //Tambahkan Entitas Publikasi ke Database
            Context.Publikasis.Add( luaranPublikasi );
            var saveResult = await Context.SaveChangesAsync() > 0;

            if (saveResult)
            {
                return ServiceResponse<CreatePublikasiDTO>.ReturnSuccess();
            }
            else 
            {
                return ServiceResponse<CreatePublikasiDTO>
                    .Return404();
            }
        }
        public async Task<IEnumerable<GetPublikasiDTO>> GetAllPublikasiAsync()
        {
            return await Context.Publikasis
                .Include(p => p.Pegawai)
                .ProjectTo<GetPublikasiDTO>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<IEnumerable<RingkasanPublikasiDTO>> GetPublikasiAggregatedAsync()
        {
            var fiveYearsAgo = DateTime.Now.Year - 5;

            var publikasiList = await Context.Publikasis
                .Include(p => p.Pegawai)
                .Where(p => p.TahunPublikasi >= fiveYearsAgo) // Filter 5 tahun terakhir
                .ToListAsync();

            var agregasi = publikasiList
                .GroupBy(p => new { p.Pegawai.NIP, p.Pegawai.Nama })
                .Select(g => new RingkasanPublikasiDTO
                {
                    NIP = g.Key.NIP,
                    Nama = g.Key.Nama,
                    TotalScopus = g.Count(p => p.KategoriPublikasi == KategoriPublikasi.Scopus),
                    TotalNonScopus = g.Count(p => p.KategoriPublikasi == KategoriPublikasi.InternationalNonScopus),
                    TotalSinta = g.Count(p => p.KategoriPublikasi == KategoriPublikasi.Sinta),
                    TotalHKI = g.Count(p => p.KategoriPublikasi == KategoriPublikasi.HKI),
                    TotalPenerapanMasyarakat = g.Count(p => p.PenerapanMasyarakat == PenerapanMasyarakat.Ya)
                })
                .ToList();

            return agregasi;
        }
        public async Task<DeletePublikasiDTO> DeletePublikasiAsync(Guid publikasiId)
        {
            var publikasi = await Context.Publikasis.FindAsync(publikasiId);

            if (publikasi == null)
            {
                return new DeletePublikasiDTO
                {
                    IsDeleted = false,
                    Message = "Publikasi tidak ditemukan"
                };
            }

            Context.Publikasis.Remove(publikasi);
            var result = await Context.SaveChangesAsync() > 0;

            return new DeletePublikasiDTO
            {
                IsDeleted = result,
                Message = result ? "Publikasi berhasil dihapus" : "Gagal menghapus publikasi"
            };
        }
        public async Task<UpdatePublikasiDTO> UpdatePublikasiAsync(Guid publikasiId, UpdatePublikasiDTO publikasiDTO)
        {
            // Cari publikasi berdasarkan ID
            var existingPublikasi = await Context.Publikasis.FindAsync(publikasiId);

            if (existingPublikasi == null)
            {
                return null; // Publikasi tidak ditemukan
            }

            // Mapping dari DTO ke entitas yang ada
            _mapper.Map(publikasiDTO, existingPublikasi);

            // Simpan perubahan
            await Context.SaveChangesAsync();

            return publikasiDTO;
        }
    }
}
