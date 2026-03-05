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
    public interface IKependidikanRepository
    {
        Task<ServiceResponse<CreateKependidikanDTO>> AddDataKependidikanAsync(CreateKependidikanDTO kependidikanDto);
        Task<IEnumerable<GetKependidikanDTO>> GetAllDataKependidikanAsync();
        Task<List<GetKependidikanDTO>> GetDataKependidikanByIdAsync(int tahunAkademikId);
        Task<ServiceResponse<string>> DeleteDataKependidikanAsync(DeleteKependidikanDTO dto);
        Task<ServiceResponse<UpdateKependidikanDTO>> UpdateDataKependidikanAsync(UpdateKependidikanDTO dto);
    }
}
