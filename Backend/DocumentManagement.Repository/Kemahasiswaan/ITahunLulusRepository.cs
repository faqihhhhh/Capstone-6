using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DocumentManagement.Data;
using DocumentManagement.Data.Dto;

namespace DocumentManagement.Repository
{
    public interface ITahunLulusRepository
    {
        Task<TahunLulus> GetTahunLulusByIdAsync(int id);
        Task<TahunLulus> GetTahunLulusByTahunAsync(int Tahun);
        Task<int> AddTahunLulusWithTracerAsync(TahunLulus tahunLulus);
        Task<IEnumerable<GetAllTahunLulusDTO>> GetStatusLulusanByLulusanTahunAsync(int lulusanTahun);
        Task<IEnumerable<GetAllTahunLulusDTO>> GetAllTahunLulusWithStatusAsync();
        Task<StatusLulusan> StatusLulusByIdAsync(int id);
    }
}
