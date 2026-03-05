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
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace DocumentManagement.Repository
{
    public class PegawaiService
    {
        private readonly IPegawaiRepository _pegawaiRepository;
        private readonly IMapper _mapper;

        public PegawaiService(IPegawaiRepository pegawaiRepository, IMapper mapper)
        {
            _pegawaiRepository = pegawaiRepository;
            _mapper = mapper;
        }

        //======================================================================================================
        //Method untuk Create Pegawai (dari client/user ke system) : POST
        //======================================================================================================
        /// <summary>
        /// Method untuk Create Pegawai (dari client/user ke system) : POST
        /// </summary>
        /// <param name="pegawaiDTO"></param>
        /// <returns></returns>
        public async Task<ServiceResponse<PegawaiDTO>> CreatePegawaiAsync(PegawaiDTO pegawaiDTO)
        {
            if (pegawaiDTO.Id.HasValue)
            {
                var existingPegawai = await _pegawaiRepository.PegawaiByIdAsync(pegawaiDTO.Id.Value);
                if (existingPegawai != null)
                {
                    return ServiceResponse<PegawaiDTO>.ReturnFailed(409, "Pegawai sudah tersimpan di dalam Database.");
                }
            }
            else {
                pegawaiDTO.Id = Guid.NewGuid();
            }
             // Pertama, semua data yang direquest dari client/user melalui 
             // PegawaiDTO akan dimapping ke Entitas Pegawai
            var pegawai = _mapper.Map<Pegawai>(pegawaiDTO);

            // Selanjutnya akan mengecek di Atribut Jenis Pegawai jika
            // Dosen maka akan dimapping ke entitas Dosen
            // Cek apakah pegawai adalah Dosen (logika khusus untuk Dosen)
            if (pegawaiDTO.Jenis_Pegawai == JenisP.Dosen)
                {
                //Pemetaan PegawaiDTO ke Entitas DOsen
                var dosen = _mapper.Map<Dosen>(pegawaiDTO);
                pegawai.Dosen = dosen;
                //pegawai.Dosen = dosen;

                // Logika tambahan untuk Dosen PNS atau Tetap non-PNS
                if (pegawaiDTO.Jenis_Dosen == Status_Dosen.PNS || pegawaiDTO.Jenis_Dosen == Status_Dosen.TetapNonPNS)
                {
                    if (pegawaiDTO.DosenTetap == null)
                    {
                        return ServiceResponse<PegawaiDTO>.ReturnFailed(400, "Data Dosen Tetap harus diisi untuk Dosen yang berstatus PNS atau Tetap non-PNS.");
                    }
                    // Pemetaan DosenTetapDTO ke entitas DosenTetap
                    var dosenTetap = _mapper.Map<DosenTetap>(pegawaiDTO.DosenTetap);
                    pegawai.Dosen.DosenTetap = dosenTetap;
                }
            }
            else if (pegawaiDTO.Jenis_Pegawai == JenisP.Tendik)
                {
                // Pemetaan TendikDTO ke entitas Tendik
                var tendik = _mapper.Map<Tendik>(pegawaiDTO);
                pegawai.Tendik = tendik;
                
                // Logika tambahan untuk Tendik PNS
                if (pegawaiDTO.Jenis_Tendik == Status_Tendik.PNS)
                {
                    if (pegawaiDTO.TendikTetap == null)
                    {
                        return ServiceResponse<PegawaiDTO>.ReturnFailed(400, "Data TendikTetap harus diisi untuk Tendik yang berstatus PNS.");
                    }
                    // Pemetaan TendikTetapDTO ke entitas TendikTetap
                    var tendikTetap = _mapper.Map<TendikTetap>(pegawaiDTO.TendikTetap);
                    pegawai.Tendik.TendikTetap = tendikTetap;

                    //var tendikTetap = new TendikTetap
                    //{
                    //    Jabatan = pegawaiDTO.Tendik.TendikTetap.Jabatan,
                    //    Golongan = pegawaiDTO.Tendik.TendikTetap.Golongan,
                    //};
                }
            }
            // Simpan pegawai ke database
            await _pegawaiRepository.AddPegawaiAsync(pegawai);
            return ServiceResponse<PegawaiDTO>.ReturnSuccess();
        }
        //======================================================================================================
        //Method untuk Update Pegawai (dari client/user ke system) : PUT
        //======================================================================================================
        /// <summary>
        /// Method untuk Update Pegawai (dari client/user ke system) : PUT
        /// </summary>
        /// <param name="id"></param>
        /// <param name="pegawaiDTO"></param>
        /// <returns></returns>
        public async Task<ServiceResponse<PegawaiDTO>> UpdatePegawaiAsync(Guid id, PegawaiDTO pegawaiDTO)
        {
            var existingPegawai = await _pegawaiRepository.PegawaiByIdAsync(id);
            if (existingPegawai == null)
            {
                return ServiceResponse<PegawaiDTO>.ReturnFailed(404, "Pegawai tidak ditemukan.");
            }

            // Pemetaan dari PegawaiDTO ke entitas Pegawai untuk update
            var pegawai = _mapper.Map(pegawaiDTO, existingPegawai);
            
            // Cek apakah Pegawai adalah Dosen
            if (pegawaiDTO.Jenis_Pegawai == JenisP.Dosen)
            {
                var existingDosen = await _pegawaiRepository.DosenByIdAsync(id);
                if (existingDosen != null)
                {
                    var dosen = _mapper.Map(pegawaiDTO, existingDosen);
                    pegawai.Dosen = dosen;
                    
                    // Cek jika Dosen adalah PNS atau tetapNonPNS
                    if (pegawaiDTO.Jenis_Dosen == Status_Dosen.PNS || pegawaiDTO.Jenis_Dosen == Status_Dosen.TetapNonPNS)
                    {
                        var existingDosenTetap = await _pegawaiRepository.DosenTetapByIdAsync(id);
                        if (existingDosenTetap != null)
                        {
                            var dosenTetap = _mapper.Map(pegawaiDTO.DosenTetap, existingDosenTetap);
                            pegawai.Dosen.DosenTetap = dosenTetap;
                            
                        }

                    }
                }
            }
            // Cek apakah Pegawai adalah Tendik
            else if (pegawaiDTO.Jenis_Pegawai == JenisP.Tendik)
            {
                var existingTendik = await _pegawaiRepository.TendikByIdAsync(id);
                if (existingTendik != null)
                {
                    var tendik = _mapper.Map(pegawaiDTO, existingTendik);
                    pegawai.Tendik = tendik;
                    //existingTendik.Jenis_Tendik = pegawaiDTO.Tendik.Jenis_Tendik;
                    //await _pegawaiRepository.UpdateTendikAsync(existingTendik);

                    // Cek jika Tendik adalah PNS
                    if (pegawaiDTO.Jenis_Tendik == Status_Tendik.PNS)
                    {
                        var existingTendikTetap = await _pegawaiRepository.TendikTetapByIdAsync(id);
                        if (existingTendikTetap != null)
                        {
                            var tendikTetap = _mapper.Map(pegawaiDTO.TendikTetap, existingTendikTetap);
                            tendik.TendikTetap = tendikTetap;
                        }
                    }
                }
            }
            await _pegawaiRepository.UpdatedPegawaiAsync(pegawai);
            return ServiceResponse<PegawaiDTO>.ReturnSuccess();
        }
        //======================================================================================================
        // Method untuk Delete Pegawai (dari client/user ke system) : Delete
        //======================================================================================================
        /// <summary>
        /// Method untuk Delete Pegawai (dari client/user ke system) : Delete
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ServiceResponse<string>> DeletePegawaiAsync(Guid id)
        {
            var existingPegawai = await _pegawaiRepository.PegawaiByIdAsync(id);
            if (existingPegawai == null)
            {
                return ServiceResponse<string>.ReturnFailed(404, "Pegawai tidak ditemukan.");
            }
            
            await _pegawaiRepository.DeletedPegawaiByIdAsync(id);
            return ServiceResponse<string>.ReturnSuccess();
        }

        public async Task<ServiceResponse<string>> DeletePegawaiByNIPAsync(string NIP)
        {
            var existingPegawai = await _pegawaiRepository.PegawaiByNIPAsync(NIP);
            if (existingPegawai == null)
            {
                return ServiceResponse<string>.ReturnFailed(404, "Pegawai tidak ditemukan.");
            }

            await _pegawaiRepository.DeletedPegawaiAsync(NIP);
            return ServiceResponse<string>.ReturnSuccess();
        }
    }

}
