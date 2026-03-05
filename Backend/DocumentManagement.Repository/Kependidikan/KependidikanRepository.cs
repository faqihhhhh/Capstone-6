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

namespace DocumentManagement.Repository
{
    public class KependidikanRepository : GenericRepository<TahunAkademik, DocumentContext>, IKependidikanRepository
    {
        private readonly IMapper _mapper;
        public KependidikanRepository(
            IUnitOfWork<DocumentContext> uow, IMapper mapper
                ) : base(uow)
        {
            _mapper = mapper;
        }

        public async Task<ServiceResponse<CreateKependidikanDTO>> AddDataKependidikanAsync(CreateKependidikanDTO kependidikanDto)
        {
            // Logic untuk membatasi TahunMasuk berdasarkan TahunAkademik
            int batasBawahThnMasuk = kependidikanDto.ThnAkademik - 7;

            if (kependidikanDto.ThnMasuk > kependidikanDto.ThnAkademik) 
            {
                return ServiceResponse<CreateKependidikanDTO>
                    .Return409($"TahunMasuk {kependidikanDto.ThnMasuk} tidak boleh lebih besar dari ThnAkademik {kependidikanDto.ThnAkademik}.");
            }
            if (kependidikanDto.ThnMasuk < batasBawahThnMasuk)
            {
                return ServiceResponse<CreateKependidikanDTO>
                    .Return409($"TahunMasuk {kependidikanDto.ThnMasuk} terlalu jauh dari ThnAkademik {kependidikanDto.ThnAkademik}. Batas minimal adalah {batasBawahThnMasuk}.");
            }
            var tahunAkademik = await Context.TahunAkademiks
                .Include(ta => ta.TahunMasuks)
                .ThenInclude(tm => tm.InfoMahasiswa)
                .FirstOrDefaultAsync(ta => ta.ThnAkademik == kependidikanDto.ThnAkademik && ta.Semester == kependidikanDto.Semester);

            if (tahunAkademik == null)
            {
                tahunAkademik = _mapper.Map<TahunAkademik>(kependidikanDto);
                tahunAkademik.TahunMasuks = new List<TahunMasuk>
                {
                    new TahunMasuk
                    {
                        ThnMasuk = kependidikanDto.ThnMasuk,
                        TotalRegistrasiMhs = kependidikanDto.TotalRegistrasiMhs,
                        InfoMahasiswa = _mapper.Map<InfoMahasiswa>(kependidikanDto)
                    }
                };
                Context.TahunAkademiks.AddAsync(tahunAkademik);
                await Context.SaveChangesAsync();
                return ServiceResponse<CreateKependidikanDTO>.ReturnSuccess();
            }
            else 
            {
                var tahunMasuk = tahunAkademik.TahunMasuks.FirstOrDefault(tm => tm.ThnMasuk == kependidikanDto.ThnMasuk);
                if (tahunMasuk == null)
                {
                    // Tambahkan TahunMasuk dan InfoMahasiswa yang baru ke dalam Database
                    tahunMasuk = _mapper.Map<TahunMasuk>(kependidikanDto);
                    tahunMasuk.InfoMahasiswa = _mapper.Map<InfoMahasiswa>(kependidikanDto);
                    tahunAkademik.TahunMasuks.Add(tahunMasuk);
                    await Context.SaveChangesAsync();
                    return ServiceResponse<CreateKependidikanDTO>.ReturnSuccess();
                }
                else if (tahunMasuk.InfoMahasiswa == null)
                {
                    //Tambahkan hanya untuk data InfoMahasiswa saja
                    tahunMasuk.InfoMahasiswa = _mapper.Map<InfoMahasiswa>(kependidikanDto);
                    await Context.SaveChangesAsync();
                    return ServiceResponse<CreateKependidikanDTO>.ReturnSuccess();
                }
                else
                {
                    return ServiceResponse<CreateKependidikanDTO>.Return409("Data sudah ada dan tidak dapat ditambahkan lagi.");
                }
            }
        }

        public async Task<IEnumerable<GetKependidikanDTO>> GetAllDataKependidikanAsync()
        {
            var data = await Context.TahunAkademiks
                .Include(ta => ta.TahunMasuks)
                .ThenInclude(tm => tm.InfoMahasiswa)
                .ToListAsync();

            // Memetakan Data ke ke kependidikanDTO dan InfoMahasiswaDTO
            var result = new List<GetKependidikanDTO>();
            foreach (var tahunAkademik in data)
            {
                foreach (var tahunMasuk in tahunAkademik.TahunMasuks) 
                {
                    var dto = _mapper.Map<GetKependidikanDTO>(tahunAkademik);
                    _mapper.Map(tahunMasuk, dto);

                    // Pemetaan InfoMahasiswa ke InfoMahasiswaDTO dalam GetKependidikanDTO
                    if (tahunMasuk.InfoMahasiswa != null)
                    {
                        dto.InfoMahsiswa = _mapper.Map<InfoMahasiswaDTO>(tahunMasuk.InfoMahasiswa);
                        //_mapper.Map(tahunMasuk.InfoMahasiswa, dto.InfoMahsiswa);
                    }
                    result.Add(dto);
                }
            }
            return result;
        }

        public async Task<List<GetKependidikanDTO>> GetDataKependidikanByIdAsync(int tahunAkademikId)
        {
            // Cari tahun berdasarkan ID Tahun Akademik
            var tahunAkademik = await Context.TahunAkademiks
                .Include(ta => ta.TahunMasuks)
                    .ThenInclude(tm => tm.InfoMahasiswa)
                .FirstOrDefaultAsync(ta => ta.ThnAkademikId == tahunAkademikId);

            if (tahunAkademik == null)
            {
                return null; // Mengembalikan null jika TahunAkademik tidak ditemukan
            }

            // Memetakan data ke GetKependidikanDTO
            var result = new List<GetKependidikanDTO>();
            foreach (var tahunMasuk in tahunAkademik.TahunMasuks)
            {
                // Peta TahunAkademik dan TahunMasuk ke GetKependidikanDTO
                var dto = _mapper.Map<GetKependidikanDTO>(tahunAkademik);
                _mapper.Map(tahunMasuk, dto);
                
                
                if (tahunMasuk.InfoMahasiswa != null)
                {
                    dto.InfoMahsiswa = _mapper.Map<InfoMahasiswaDTO>(tahunMasuk.InfoMahasiswa);
                }
                result.Add(dto);
            }
            return result;
        }
        public async Task<ServiceResponse<string>> DeleteDataKependidikanAsync(DeleteKependidikanDTO dto)
        {
            // Mencari TahunAkademik yang sesuai
            var tahunAkademik = await Context.TahunAkademiks
                .Include(ta => ta.TahunMasuks)
                    .ThenInclude(tm => tm.InfoMahasiswa)
                .FirstOrDefaultAsync(ta => ta.ThnAkademik == dto.ThnAkademik 
                && ta.Semester == dto.Semester);
            if (tahunAkademik == null)
            {
                return ServiceResponse<string>
                    .ReturnFailed(404, "Data tidak ditemukan untuk Tahun Akademik tersebut");
            }

            // Mencari TahunMasuk yang sesuai
            var tahunMasuk = tahunAkademik.TahunMasuks.FirstOrDefault(tm => tm.ThnMasuk == dto.ThnMasuk);
            if (tahunMasuk == null)
            {
                return ServiceResponse<string>
                    .ReturnFailed(404, "Data tidak ditemukan untuk Tahun Masuk tersebut");
            }
            // Memeriksa jika InfoMahasiswa sudah ada dan menghapusnya
            if (tahunMasuk.InfoMahasiswa == null)
            {
                return ServiceResponse<string>
                    .ReturnFailed(404, "Tidak ditemukan catatan Mahsiswa pada Tahun Akademik dan Tahun Masuk tersebut");
            }
            Context.InfoMahasiswas.Remove(tahunMasuk.InfoMahasiswa);
            await Context.SaveChangesAsync();
            return ServiceResponse<string>.ReturnSuccess();
        }

        public async Task<ServiceResponse<UpdateKependidikanDTO>> UpdateDataKependidikanAsync(UpdateKependidikanDTO dto)
        {
            // Cari TahunAkademik berdasarkan ThnAkademik dan Semester
            var tahunAkademik = await Context.TahunAkademiks
                .Include(ta => ta.TahunMasuks)
                    .ThenInclude(tm => tm.InfoMahasiswa)
                .FirstOrDefaultAsync(ta => ta.ThnAkademik == dto.ThnAkademik 
                && ta.Semester == dto.Semester);

            if (tahunAkademik == null)
            {
                return ServiceResponse<UpdateKependidikanDTO>
                    .ReturnFailed(404, "Tahun Akademik tidak ditemukan.");
            }
            // Cari TahunMasuk yang sesuai
            var tahunMasuk = tahunAkademik.TahunMasuks
                .FirstOrDefault(tm => tm.ThnMasuk == dto.ThnMasuk);
            if (tahunMasuk == null)
            {
                return ServiceResponse<UpdateKependidikanDTO>
                    .ReturnFailed(404, "Tahun Masuk tidak ditemukan.");
            }
            // Cek jika InfoMahasiswa ada, lalu update data
            if (tahunMasuk.InfoMahasiswa == null)
            {
                return ServiceResponse<UpdateKependidikanDTO>
                    .ReturnFailed(404, "Data Informasi Kependidikan tidak ditemukan untuk Tahun Masuk tersebut.");
            }
            // Map data dari DTO ke entity InfoMahasiswa
            _mapper.Map(dto, tahunMasuk.InfoMahasiswa);
            await Context.SaveChangesAsync();
            return ServiceResponse<UpdateKependidikanDTO>.ReturnSuccess();
        }
    }
}