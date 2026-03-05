using DocumentManagement.Common.GenericRepository;
using DocumentManagement.Data;
using DocumentManagement.Data.Dto;
using DocumentManagement.Helper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DocumentManagement.Repository
{
    public interface IPegawaiRepository : IGenericRepository<Pegawai>
    {
        // Query Operation
        Task<IEnumerable<DosenDetailDTO>> GetAllDosenAsync();
        Task<IEnumerable<TendikDetailDTO>> GetAllTendikAsync();
        Task<DosenDetailDTO> GetDosenByIdAsync(Guid id);
        Task<DosenDetailDTO> GetDosenByNIPAsync(string NIP);
        Task<TendikDetailDTO> GetTendikByIdAsync(Guid id);
        Task<TendikDetailDTO> GetTendikByNIPAsync(string NIP);

        //Comand Operation
        Task AddPegawaiAsync(Pegawai pegawai);
        Task UpdatedPegawaiAsync(Pegawai pegawai);
        Task DeletedPegawaiAsync(string NIP);
        Task DeletedPegawaiByIdAsync(Guid id);

        // Get Id dan NIP Pegawai, Dosen, Tendik
        Task<Pegawai> PegawaiByIdAsync(Guid id);
        Task<Pegawai> PegawaiByNIPAsync(string NIP);
         
        Task<Dosen> DosenByIdAsync(Guid id);
        Task<Dosen> DosenByNIPAsync(string NIP);

        Task<DosenTetap> DosenTetapByIdAsync(Guid id);
        Task<DosenTetap> DosenTetapByNIPAsync(string NIP);

        Task<Tendik> TendikByIdAsync(Guid id);
        Task<Tendik> TendikByNIPAsync(string NIP);

        Task<TendikTetap> TendikTetapByIdAsync(Guid Id);
        Task<TendikTetap> TendikTetapByNIPAsync(string NIP);
        
        
    }
}
