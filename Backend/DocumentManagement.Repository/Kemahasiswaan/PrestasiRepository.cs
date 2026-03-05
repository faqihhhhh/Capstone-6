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
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DocumentManagement.Repository
{
    public class PrestasiRepository : GenericRepository<TahunAkademik, DocumentContext>, IPrestasiRepository
    {
        private readonly IMapper _mapper;
        public PrestasiRepository(
            IUnitOfWork<DocumentContext> uow, IMapper mapper
            ) : base(uow)
        {
            _mapper = mapper; // Simpan instance IMapper yang di-inject
        }

        public async Task<ServiceResponse<CreatePrestasiDTO>> AddTahunAkademikAsync(CreatePrestasiDTO tahunAkademikDto)
        {
            // Cek apakah TahunAkademik dengan kombinasi ThnAkademik dan Semester sudah ada
            var existingTahun = await Context.TahunAkademiks
                .FirstOrDefaultAsync(t => t.ThnAkademik == tahunAkademikDto.ThnAkademik
                && t.Semester == tahunAkademikDto.Semester);
            if (existingTahun != null)
            {
                bool existingPrestasi = await Context.PrestasiMhss
                    .AnyAsync(p => p.ThnAkademikId == existingTahun.ThnAkademikId);
                if (existingPrestasi)
                {
                    return ServiceResponse<CreatePrestasiDTO>.Return409("Tahun Akademik dan data Prestasi tidak bisa ditambahkan karena sudah ada di database.");
                }
                else
                {
                    var prestasiMhs = _mapper.Map<PrestasiMhs>(tahunAkademikDto.PrestasiMhs);
                    prestasiMhs.ThnAkademikId = existingTahun.ThnAkademikId;
                    await Context.PrestasiMhss.AddAsync(prestasiMhs);
                    await Context.SaveChangesAsync();
                    return ServiceResponse<CreatePrestasiDTO>.ReturnSuccess();
                }
            }
            else
            {
                var tahunAkademik = _mapper.Map<TahunAkademik>(tahunAkademikDto);
                await Context.TahunAkademiks.AddAsync(tahunAkademik);
                await Context.SaveChangesAsync();
                return ServiceResponse<CreatePrestasiDTO>.ReturnSuccess();
                //return _mapper.Map<TahunAkademikDTO>(tahunAkademik);
            }
        }

        public async Task<IEnumerable<GetAllPrestasiDTO>> GetAllTahunAkademikAsync()
        {
            var tahunAkademikEntities = await Context.TahunAkademiks
                .Include(t => t.PrestasiMhs)
                .ToListAsync();

            return _mapper.Map<IEnumerable<GetAllPrestasiDTO>>(tahunAkademikEntities); // Mapping ke DTO
        }

        public async Task<GetAllPrestasiDTO> GetTahunAkademikByIdAsync(int id)
        {
            var tahunAkademikEntity = await Context.TahunAkademiks
                .Include(t => t.PrestasiMhs)
                .FirstOrDefaultAsync(t => t.ThnAkademikId == id);

            if (tahunAkademikEntity == null)
            {
                throw new Exception("Tahun Akademik tidak ditemukan.");
            }

            return _mapper.Map<GetAllPrestasiDTO>(tahunAkademikEntity); // Mapping ke DTO
        }

        public async Task<PrestasiMhs> GetPrestasiMhsByTahunAkademikIdAsync(int thnAkademikId)
        {
            return await Context.PrestasiMhss.FirstOrDefaultAsync(p => p.ThnAkademikId == thnAkademikId);
        }

        public async Task<ServiceResponse<string>> DeletePrestasiMhsAsync(int prestasiMhsId)
        {
            var prestasiMhs = await GetPrestasiMhsByTahunAkademikIdAsync(prestasiMhsId);
            if (prestasiMhs == null) { return ServiceResponse<string>
                    .ReturnFailed(404, "Data PrestasiMhs tidak ditemukan untuk TahunAkademikId tersebut"); }

            Context.PrestasiMhss.Remove(prestasiMhs);
            await Context.SaveChangesAsync();
            return ServiceResponse<string>.ReturnSuccess();
        }

        public async Task<ServiceResponse<UpdatePrestasiMhsDTO>> UpdatePrestasiMhsAsync(int thnAkademikId, UpdatePrestasiMhsDTO prestasiMhsDto)
        {
            // Cari data PrestasiMhs berdasarkan ThnAkademikId
            var prestasiMhsEntity = await Context.PrestasiMhss
                .FirstOrDefaultAsync(p => p.ThnAkademikId == thnAkademikId);

            if (prestasiMhsEntity == null)
            {
                return ServiceResponse<UpdatePrestasiMhsDTO>.ReturnFailed(404, "Data Prestasi Mahasiswa untuk Tahun Akademik yang dicari tidak ditemukan.");
                
            }

            // Update Entity menggunakan Automapper
            _mapper.Map(prestasiMhsDto, prestasiMhsEntity);
            
            Context.PrestasiMhss.Update(prestasiMhsEntity);
            await Context.SaveChangesAsync();
            return ServiceResponse<UpdatePrestasiMhsDTO>.ReturnSuccess();
        }
    }
}
