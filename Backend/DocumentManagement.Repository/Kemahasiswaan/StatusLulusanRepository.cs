using DocumentManagement.Common.GenericRepository;
using DocumentManagement.Common.UnitOfWork;
using DocumentManagement.Data;
using DocumentManagement.Data.Dto;
using DocumentManagement.Domain;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System;
using AutoMapper;
using DocumentManagement.Helper;

public class StatusLulusanRepository : GenericRepository<StatusLulusan, DocumentContext>,IStatusLulusanRepository
{
    private readonly IMapper _mapper;

    public int GenerateIdFromGuid()
    {
        var guid = Guid.NewGuid();
        var bytes = guid.ToByteArray();
        int generatedId = BitConverter.ToInt32(bytes, 0); // Konversi ke integer
        return Math.Abs(generatedId); // Pastikan nilainya positif
    }

    public StatusLulusanRepository(
            IUnitOfWork<DocumentContext> uow, IMapper mapper
            ) : base(uow)
    {
        _mapper = mapper; // Simpan instance IMapper yang di-inject
    }

    public async Task<TahunLulus> TahunLulusAsync(int id)
    {
        return await Context.Set<TahunLulus>()
            .FirstOrDefaultAsync(p => p.LulusanTahun == id);//DbSet.FirstOrDefaultAsync(p => p.Id == id);
    }
    public async Task<StatusLulusan> LulusanByIdAsync(int id)
    {
        return await Context.Set<StatusLulusan>()
            .FirstOrDefaultAsync(p => p.StatusId == id);//DbSet.FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task AddTahunLulusAsync(TahunLulus tahunLulus)
    {
        Context.TahunLuluss.Add(tahunLulus);   
        await Context.SaveChangesAsync();
    }

    // Menambahkan satu entitas StatusLulusan ke database
    public async Task AddStatusLulusanAsync(StatusLulusan statusLulusan)
    {
        Context.StatusLulusans.Add(statusLulusan);  // Tambahkan ke DbSet
        await Context.SaveChangesAsync();  // Simpan perubahan ke database
    }

    // Menambahkan beberapa entitas StatusLulusan ke database
    public async Task AddMultipleStatusLulusanAsync(IEnumerable<StatusLulusan> statusLulusanList)
    {
        Context.StatusLulusans.AddRange(statusLulusanList);  // Tambahkan beberapa entitas sekaligus
        await Context.SaveChangesAsync();  // Simpan perubahan ke database
    }
}