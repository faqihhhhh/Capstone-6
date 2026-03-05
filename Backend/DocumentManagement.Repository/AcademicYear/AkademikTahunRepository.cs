using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DocumentManagement.Common.GenericRepository;
using DocumentManagement.Common.UnitOfWork;
using DocumentManagement.Data.Dto;
using DocumentManagement.Data.Entities;
using DocumentManagement.Domain;
using DocumentManagement.Helper;
using Microsoft.EntityFrameworkCore;

namespace DocumentManagement.Repository
{
    public class AkademikTahunRepository : GenericRepository<TahunAkademik, DocumentContext>, IAkademikTahunRepository
    {  
        private readonly IMapper _mapper;
        public AkademikTahunRepository(
            IUnitOfWork<DocumentContext> uow, IMapper mapper) : base(uow)
        {
            _mapper = mapper;
        }
        // Mendapatkan Tahun Akademik terbaru
        public async Task<TahunAkademik> GetLatestTahunAkademikAsync()
        {
            return await Context.TahunAkademiks
                .OrderByDescending(t => t.ThnAkademik)
                .ThenByDescending(t => t.Semester)
                .FirstOrDefaultAsync();
        }

        public async Task<ServiceResponse<CreateTahunAkademikDTO>> AddAkademikTahunAsync(CreateTahunAkademikDTO tahunAkademikCreateDTO)
        {
            bool exists = await Context.TahunAkademiks
                .AnyAsync(t => t.ThnAkademik == tahunAkademikCreateDTO.ThnAkademik && t.Semester == tahunAkademikCreateDTO.Semester);
            if (exists)
            {
                return ServiceResponse<CreateTahunAkademikDTO>.Return409("Tahun Akademik tidak bisa ditambahkan karena sudah ada di database.");
                //throw new ArgumentException("Tahun Akademik sudah ada di Database.");
            }
            var tahunAkademik = _mapper.Map<TahunAkademik>(tahunAkademikCreateDTO);
            await Context.TahunAkademiks.AddAsync(tahunAkademik);
            await Context.SaveChangesAsync();
            return ServiceResponse<CreateTahunAkademikDTO>.ReturnSuccess();
        }

        public async Task<ServiceResponse<string>> DeleteTahunAkademikAsync(int id)
        {
            var tahunAkademikEntity = await Context.TahunAkademiks.FindAsync(id);
            //.Include(t => t.PrestasiMhs) // Sertakan PrestasiMhs jika ada hubungan 1-to-1 yang perlu dihapus
            //.FirstOrDefaultAsync(t => t.ThnAkademikId == id);

            if (tahunAkademikEntity == null)
            {
                return ServiceResponse<string>.ReturnFailed(404, "Data tidak ditemukan."); // Data tidak ditemukan
            }

            Context.TahunAkademiks.Remove(tahunAkademikEntity);
            await Context.SaveChangesAsync();

            return ServiceResponse<string>.ReturnSuccess();
        }

        public async Task<IEnumerable<GetTahunAkademikDTO>> GetAllTahunAkademikAsync()
        {
            var tahunAkademikEntities = await Context.TahunAkademiks.ToListAsync();
            return _mapper.Map<IEnumerable<GetTahunAkademikDTO>>(tahunAkademikEntities); // Mapping ke DTO
        }

        public async Task<GetTahunAkademikDTO> GetTahunAkademikByIdAsync(int id)
        {
            var tahunAkademikEntity = await Context.TahunAkademiks.FindAsync(id);

            if (tahunAkademikEntity == null)
            {
                throw new Exception("Tahun Akademik tidak ditemukan.");
            }

            return _mapper.Map<GetTahunAkademikDTO>(tahunAkademikEntity); // Mapping ke DTO
        }
    }
}