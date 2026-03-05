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
using DocumentManagement.Domain;
using Microsoft.EntityFrameworkCore;

namespace DocumentManagement.Repository
{
    public class TahunLulusRepository : GenericRepository<TahunLulus, DocumentContext>, ITahunLulusRepository
    {
        private readonly IMapper _mapper;
        public TahunLulusRepository(
            IUnitOfWork<DocumentContext> uow, IMapper mapper
            ) : base(uow)
        {
            _mapper = mapper; // Simpan instance IMapper yang di-inject
        }

        public async Task<TahunLulus> GetTahunLulusByIdAsync(int id)
        {
            return await Context.TahunLuluss.FirstOrDefaultAsync(t => t.TahunId == id);
        }
        public async Task<TahunLulus> GetTahunLulusByTahunAsync(int Tahun)
        {
            return await Context.TahunLuluss.FirstOrDefaultAsync(t => t.LulusanTahun == Tahun);
        }

        public async Task<int> AddTahunLulusWithTracerAsync(TahunLulus tahunLulus)
        {
            await Context.TahunLuluss.AddAsync(tahunLulus);
            await Context.SaveChangesAsync();
            return tahunLulus.TahunId;
        }

        public async Task<IEnumerable<GetAllTahunLulusDTO>> GetAllTahunLulusWithStatusAsync()
        {
            // Ambil semua data dari database termasuk StatusLulusan terkait
            var tahunLulusList = await Context.TahunLuluss
                .Include(t => t.StatusLulusan)
                    .ThenInclude(s => s.MasaTungguKerja)
                .Include(t => t.StatusLulusan)
                    .ThenInclude(s => s.PosisiJabatan)
                .Include(t => t.StatusLulusan)
                    .ThenInclude(s => s.JenisTempatKerja)
                .Include(t => t.StatusLulusan)
                    .ThenInclude(s => s.TingkatTempatKerja)
                .ToListAsync();

            // Mapping dari entity ke DTO menggunakan AutoMapper
            var result = _mapper.Map<IEnumerable<GetAllTahunLulusDTO>>(tahunLulusList);

            return result;
        }
        public async Task<IEnumerable<GetAllTahunLulusDTO>> GetStatusLulusanByLulusanTahunAsync(int lulusanTahun)
        {
            // Ambil TahunLulus berdasarkan LulusanTahun
            var tahunLulusList = await Context.TahunLuluss
                .Include(t => t.StatusLulusan)
                .Where(t => t.LulusanTahun == lulusanTahun)
                .ToListAsync();

            // Mapping dari entity ke DTO menggunakan AutoMapper
            var result = _mapper.Map<IEnumerable<GetAllTahunLulusDTO>>(tahunLulusList);

            return result;
        }

        public async Task<StatusLulusan> StatusLulusByIdAsync(int id)
        {
            return await Context.Set<StatusLulusan>()
                .FirstOrDefaultAsync(p => p.StatusId == id);//DbSet.FirstOrDefaultAsync(p => p.Id == id);
        }
    }
}
