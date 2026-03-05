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
    public interface IPrestasiRepository
    {
        Task<ServiceResponse<CreatePrestasiDTO>> AddTahunAkademikAsync(CreatePrestasiDTO tahunAkademikDto);
        Task<IEnumerable<GetAllPrestasiDTO>> GetAllTahunAkademikAsync();
        Task<GetAllPrestasiDTO> GetTahunAkademikByIdAsync(int id);
        Task<ServiceResponse<UpdatePrestasiMhsDTO>> UpdatePrestasiMhsAsync(int thnAkademikId, UpdatePrestasiMhsDTO prestasiMhsDto);
        Task<PrestasiMhs> GetPrestasiMhsByTahunAkademikIdAsync(int thnAkademikId);
        Task<ServiceResponse<string>> DeletePrestasiMhsAsync(int prestasiMhsId);
    }
}
