using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DocumentManagement.Data;
using DocumentManagement.Data.Dto;
using DocumentManagement.Data.Entities;
using DocumentManagement.Helper;

namespace DocumentManagement.Repository
{
    public interface IKegiatanPPMRepository
    {
        Task<ServiceResponse<CreateKegiatanPPMDTO>> AddKegiatanPPMAsync(CreateKegiatanPPMDTO dto);
        Task<ServiceResponse<CreatePegawaiKegiatanPPMDTO>> AddPegawaiKegiatanPPMAsync(CreatePegawaiKegiatanPPMDTO dto);
        Task<IEnumerable<PegawaiKegiatanPPMResponseDTO>> GetAllPegawaiKegiatanPPMAsync();
        Task<List<KegiatanPPMListDTO>> GetAllKegiatanPPMAsync();
        Task<List<RingkasanPengabdianPenelitianDTO>> GetRingkasanPengabdianPenelitianAsync();
        Task<List<MitraPenelitianDTO>> GetMitraPenelitianAsync();
        Task<List<MitraPengabdianDTO>> GetMitraPengabdianAsync();
        Task<bool> DeleteKegiatanPPMAsync(Guid kegiatanId);
        Task<bool> UpdateKegiatanPPMAsync(Guid kegiatanId, UpdateKegiatanPPMDTO updateDto);
    }
}