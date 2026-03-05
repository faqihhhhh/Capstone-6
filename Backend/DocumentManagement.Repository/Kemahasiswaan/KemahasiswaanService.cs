using System;
using AutoMapper;
using DocumentManagement.Data;
using DocumentManagement.Data.Dto;
using DocumentManagement.Helper;
using DocumentManagement.Repository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Runtime.ConstrainedExecution;
using DocumentManagement.Domain;

namespace DocumentManagement.Repository
{
    public class KemahasiswaanService
    {
        private readonly ITahunLulusRepository _tahunlulusRepository;
        //private readonly IStatusLulusanRepository _lulusanRepository;
        private readonly IMapper _mapper;
        private readonly DocumentContext Context;
        public KemahasiswaanService(ITahunLulusRepository tahunRepository, DocumentContext context, IMapper mapper)
        {
            _tahunlulusRepository = tahunRepository;
            Context = context;
            _mapper = mapper;
        }


        /// <summary>
        /// Untuk Create Tahun Lulus di Database (method POST)
        /// </summary>
        /// <param name="tahunLulusDTO"></param>
        /// <returns></returns>
        public async Task<int> CreateTahunLulusWithTracerAsync(TahunLulusWithTracerDTO tahunLulusDTO)
        {
            
            var existingTahunLulus = await _tahunlulusRepository.GetTahunLulusByTahunAsync(tahunLulusDTO.LulusanTahun);

            if (existingTahunLulus != null)
            {
                foreach (var statusDTO in tahunLulusDTO.StatusLulusan)
                {
                    var existingStatus = await Context.StatusLulusans
                        .FirstOrDefaultAsync(s => s.Tahun_Input == statusDTO.Tahun_Input && 
                        s.TahunLulus.TahunId == existingTahunLulus.TahunId);
                    if (existingStatus == null) 
                    {
                        var statusLulusan = _mapper.Map<StatusLulusan>(statusDTO);
                        statusLulusan.TahunLulus = existingTahunLulus;
                        Context.StatusLulusans.AddAsync(statusLulusan);
                    }
                    else
                    { continue; }
                }
                await Context.SaveChangesAsync();
                return existingTahunLulus.TahunId;
            }
            else
            {
                // Buat entitas baru jika LulusanTahun belum ada
                var tahunLulus = _mapper.Map<TahunLulus>(tahunLulusDTO);

                //relasi antara TahunLulus dan StatusLulusan, MasaTunggu, Tingkat Tempat, JenisTempat, Posisi, 
                foreach (var status in tahunLulus.StatusLulusan)
                {
                    status.TahunLulus = tahunLulus; // relasi many-to-one
                }
                return await _tahunlulusRepository.AddTahunLulusWithTracerAsync(tahunLulus);
            }
        }

        public async Task<bool> DeleteTracerAsync(int statusId)
        {
            var statusLulusan = await Context.StatusLulusans
                .FirstOrDefaultAsync(s => s.StatusId == statusId);
            if (statusLulusan == null)
            {
                return false;
            }
            Context.StatusLulusans.Remove(statusLulusan);

            // Simpan perubahan
            await Context.SaveChangesAsync();

            return true; // Penghapusan berhasil
        }

        public async Task<ServiceResponse<StatusLulusanUpdateDTO>> UpdateTracerAsync(int statusId, StatusLulusanUpdateDTO statusLulusanDto)
        {
            var updateLulusan = await Context.StatusLulusans
                .Include(s => s.MasaTungguKerja)
                .Include(s => s.PosisiJabatan)
                .Include(s => s.JenisTempatKerja)
                .Include(s => s.TingkatTempatKerja)
                .FirstOrDefaultAsync(s => s.StatusId == statusId);

            if (updateLulusan == null)
            {
                throw new Exception("Data dengan Status lulusan dengan tahun lulus yang diinputkan tidak ditemukan.");
            }

            // Update entitas StatusLulusan dengan data dari DTO
            _mapper.Map(statusLulusanDto, updateLulusan);
            // Simpan perubahan ke database
            await Context.SaveChangesAsync();
            return ServiceResponse<StatusLulusanUpdateDTO>.ReturnSuccess();
        }
    }
}