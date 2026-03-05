using DocumentManagement.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface IStatusLulusanRepository
{
    public int GenerateIdFromGuid();
    Task<TahunLulus> TahunLulusAsync(int id);
    Task<StatusLulusan> LulusanByIdAsync(int id);
    Task AddTahunLulusAsync(TahunLulus tahunLulus);
    Task AddStatusLulusanAsync(StatusLulusan statusLulusan);
    Task AddMultipleStatusLulusanAsync(IEnumerable<StatusLulusan> statusLulusanList);
}
