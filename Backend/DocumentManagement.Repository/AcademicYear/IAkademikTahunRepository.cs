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
    public interface IAkademikTahunRepository
    {
        Task<TahunAkademik> GetLatestTahunAkademikAsync();
        Task<ServiceResponse<CreateTahunAkademikDTO>> AddAkademikTahunAsync(CreateTahunAkademikDTO tahunAkademikCreateDTO);
        Task<ServiceResponse<string>> DeleteTahunAkademikAsync(int id);
        Task<IEnumerable<GetTahunAkademikDTO>> GetAllTahunAkademikAsync();
        Task<GetTahunAkademikDTO> GetTahunAkademikByIdAsync(int id);
    }
}
