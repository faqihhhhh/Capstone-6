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
    public interface IPublikasiRepository
    {
        Task<ServiceResponse<CreatePublikasiDTO>> AddPublikasiAsync(CreatePublikasiDTO publikasiDTO);
        Task<IEnumerable<GetPublikasiDTO>> GetAllPublikasiAsync();
        Task<IEnumerable<RingkasanPublikasiDTO>> GetPublikasiAggregatedAsync();
        Task<DeletePublikasiDTO> DeletePublikasiAsync(Guid publikasiId);
        Task<UpdatePublikasiDTO> UpdatePublikasiAsync(Guid publikasiId, UpdatePublikasiDTO publikasiDTO);
    }
}
