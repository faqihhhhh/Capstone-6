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

namespace DocumentManagement.Repository
{
    public class PegawaiRepository : GenericRepository<Pegawai, DocumentContext>, IPegawaiRepository
    {
        private readonly IMapper _mapper;
        public PegawaiRepository(
            IUnitOfWork<DocumentContext> uow, IMapper mapper
            ) : base(uow)
        {
            _mapper = mapper; // Simpan instance IMapper yang di-inject
        }
        // 

        //=================================================|||====================================================//
        /// <summary>
        /// Mengambil semua Dosen dan memetakan ke DosenDetailDTO (Query Get)
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<DosenDetailDTO>> GetAllDosenAsync()
        {
            var dosenList = await Context.Pegawais
                .Include(p => p.Dosen)                  // Include Dosen
                .ThenInclude(d => d.DosenTetap)         // Include DosenTetap jika ada
                .Where(p => p.Jenis_Pegawai == JenisP.Dosen) 
                .ToListAsync();
            // Lakukan pemetaan dari Pegawai ke DosenDetailDTO setelah data diambil
            return _mapper.Map<IEnumerable<DosenDetailDTO>>(dosenList.Select(p => (p, p.Dosen)));
        }
        /// <summary>
        /// Mengambil semua Tendik dan memetakan ke TendikDetailDTO
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<TendikDetailDTO>> GetAllTendikAsync()
        {
            var tendikList = await Context.Pegawais
                .Include(p => p.Tendik)                  // Include Tendik
                .ThenInclude(t => t.TendikTetap)         // Include TendikTetap jika ada
                .Where(p => p.Jenis_Pegawai == JenisP.Tendik)
                .ToListAsync();
            // Pemetaan otomatis menggunakan AutoMapper
            return _mapper.Map<IEnumerable<TendikDetailDTO>>(tendikList.Select(p => (p, p.Tendik)));
        }
        /// <summary>
        /// Mengambil Data detail Dosen By Id dan memetakan ke DosenDetailDTO
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<DosenDetailDTO> GetDosenByIdAsync(Guid id)
        {
            var dosenbyId = await Context.Pegawais
                .Include(p => p.Dosen)                  // Include Dosen
                .ThenInclude(d => d.DosenTetap)         // Include DosenTetap jika ada
                .Where(p => p.Id == id && p.Jenis_Pegawai == JenisP.Dosen) 
                .FirstOrDefaultAsync();
            // Jika tidak ditemukan, lempar exception
            if (dosenbyId == null)
            {
                return null; // Data tidak ditemukan
            }
            return _mapper.Map<DosenDetailDTO>((dosenbyId, dosenbyId.Dosen));
        }

        /// <summary>
        /// Mengambil Data detail Dosen By NIP dan memetakan ke DosenDetailDTO
        /// </summary>
        /// <param name="NIP"></param>
        /// <returns></returns>
        public async Task<DosenDetailDTO> GetDosenByNIPAsync(string NIP)
        {
            var dosenbyNIP = await Context.Pegawais
                .Include(p => p.Dosen)                  // Include Dosen
                .ThenInclude(d => d.DosenTetap)         // Include DosenTetap jika ada
                .Where(p => p.NIP == NIP && p.Jenis_Pegawai == JenisP.Dosen)
                .FirstOrDefaultAsync();
            if (dosenbyNIP == null) 
            { 
                throw new KeyNotFoundException($"Dosen dengan Id {NIP} tidak ditemukan."); 
            }
            return _mapper.Map<DosenDetailDTO>((dosenbyNIP, dosenbyNIP.Dosen));
        }
        /// <summary>
        /// Mengambil Data detail Tendik By Id dan memetakan ke TendikDetailDTO
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<TendikDetailDTO> GetTendikByIdAsync(Guid id)
        {
            var tendikbyId = await Context.Pegawais
                .Include(p => p.Tendik)                  // Include Tendik
                .ThenInclude(t => t.TendikTetap)         // Include TendikTetap jika ada
                .Where(p => p.Id == id && p.Jenis_Pegawai == JenisP.Tendik)
                .FirstOrDefaultAsync();
            // Jika tidak ditemukan, lempar exception
            if (tendikbyId == null)
            {
                return null; // Data tidak ditemukan
            }
            return _mapper.Map<TendikDetailDTO>((tendikbyId, tendikbyId.Tendik));
        }

        /// <summary>
        /// Mengambil Data detail Tendik By NIP dan memetakan ke TendikDetailDTO
        /// </summary>
        /// <param name="NIP"></param>
        /// <returns></returns>
        public async Task<TendikDetailDTO> GetTendikByNIPAsync(string NIP)
        {
            var tendikbyNIP = await Context.Pegawais
                .Include(p => p.Tendik)                  // Include Tendik
                .ThenInclude(t => t.TendikTetap)         // Include TendikTetap jika ada
                .Where(p => p.NIP == NIP && p.Jenis_Pegawai == JenisP.Tendik).FirstOrDefaultAsync();
            if (tendikbyNIP == null)
            {
                throw new KeyNotFoundException($"Tenaga Kependidikan(Tendik) dengan Id {NIP} tidak ditemukan.");
            }
            return _mapper.Map<TendikDetailDTO>((tendikbyNIP, tendikbyNIP.Tendik));
        }
        //=================================================|||====================================================//
        // Class ini untuk Query Post, Put dan Delete
        // Add Pegawai, Dosen/Tendik, DosenTetap/TendikTetap
        // Method untuk Create Pegawai (dari client/user ke system)
        
        /// <summary>
        /// Menyimpan Data dari Entitas ke Database untuk method Create/Add
        /// </summary>
        /// <param name="pegawai"></param>
        /// <returns></returns>
        public async Task AddPegawaiAsync(Pegawai pegawai)
        {
            await Context.Pegawais.AddAsync(pegawai);

            if (pegawai.Jenis_Pegawai == JenisP.Dosen && pegawai.Dosen != null)
            {
                
                await Context.Dosens.AddAsync(pegawai.Dosen);

                if ((pegawai.Dosen.Jenis_Dosen == Status_Dosen.PNS || pegawai.Dosen.Jenis_Dosen == Status_Dosen.TetapNonPNS) && pegawai.Dosen.DosenTetap != null)
                {
                    await Context.DosenTetaps.AddAsync(pegawai.Dosen.DosenTetap);
                }
            }
            else if (pegawai.Jenis_Pegawai == JenisP.Tendik && pegawai.Tendik != null)
            {
                await Context.Tendiks.AddAsync(pegawai.Tendik);

                if (pegawai.Tendik.Jenis_Tendik == Status_Tendik.PNS && pegawai.Tendik.TendikTetap != null)
                {
                    await Context.TendikTetaps.AddAsync(pegawai.Tendik.TendikTetap);
                }
            }
            await Context.SaveChangesAsync();
        }
        
        /// <summary>
        /// Menyimpan data dari entitas ke Database untuk method Update
        /// </summary>
        /// <param name="pegawai"></param>
        /// <returns></returns>
        public async Task UpdatedPegawaiAsync(Pegawai pegawai)
        {
            Context.Pegawais.Update(pegawai);

            if (pegawai.Jenis_Pegawai == JenisP.Dosen && pegawai.Dosen != null)
            {
                Context.Dosens.Update(pegawai.Dosen);
                if ((pegawai.Dosen.Jenis_Dosen == Status_Dosen.PNS || pegawai.Dosen.Jenis_Dosen == Status_Dosen.TetapNonPNS) && pegawai.Dosen.DosenTetap != null)
                {
                    Context.DosenTetaps.Update(pegawai.Dosen.DosenTetap);
                }
            }
            else if (pegawai.Jenis_Pegawai == JenisP.Tendik && pegawai.Tendik != null)
            {
                Context.Tendiks.Update(pegawai.Tendik);
                if (pegawai.Tendik.Jenis_Tendik == Status_Tendik.PNS && pegawai.Tendik.TendikTetap != null)
                {
                    Context.TendikTetaps.Update(pegawai.Tendik.TendikTetap);
                }
            }
            await Context.SaveChangesAsync();
        }
        

        /// <summary>
        /// Menghapus Pegawai di Entitas By NIP
        /// </summary>
        /// <param name="NIP"></param>
        /// <returns></returns>
        public async Task DeletedPegawaiAsync(string NIP)
        {
            var pegawai = await Context.Pegawais
                .Include(p => p.Dosen)
                .ThenInclude(d => d.DosenTetap)
                .Include(p => p.Tendik)
                .ThenInclude(t => t.TendikTetap)
                .FirstOrDefaultAsync(p => p.NIP == NIP);

            if (pegawai != null)
            {
                // If the Pegawai is a Dosen, delete related Dosen and DosenTetap
                if (pegawai.Dosen != null)
                {
                    if (pegawai.Dosen.DosenTetap != null)
                    {
                        Context.DosenTetaps.Remove(pegawai.Dosen.DosenTetap);
                    }
                    Context.Dosens.Remove(pegawai.Dosen);
                }

                // If the Pegawai is a Tendik, delete related Tendik and TendikTetap
                if (pegawai.Tendik != null)
                {
                    if (pegawai.Tendik.TendikTetap != null)
                    {
                        Context.TendikTetaps.Remove(pegawai.Tendik.TendikTetap);
                    }
                    Context.Tendiks.Remove(pegawai.Tendik);
                }

                // Finally, delete the Pegawai
                Context.Pegawais.Remove(pegawai);
                await Context.SaveChangesAsync();
            }
        }


        /// <summary>
        /// Menghapus Pegawai By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task DeletedPegawaiByIdAsync(Guid id)
        {
            var pegawai = await Context.Pegawais
                .Include(p => p.Dosen)
                .ThenInclude(d => d.DosenTetap)
                .Include(p => p.Tendik)
                .ThenInclude(t => t.TendikTetap)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (pegawai != null)
            {
                // If the Pegawai is a Dosen, delete related Dosen and DosenTetap
                if (pegawai.Dosen != null)
                {
                    if (pegawai.Dosen.DosenTetap != null)
                    {
                        Context.DosenTetaps.Remove(pegawai.Dosen.DosenTetap);
                    }
                    Context.Dosens.Remove(pegawai.Dosen);
                }

                // If the Pegawai is a Tendik, delete related Tendik and TendikTetap
                if (pegawai.Tendik != null)
                {
                    if (pegawai.Tendik.TendikTetap != null)
                    {
                        Context.TendikTetaps.Remove(pegawai.Tendik.TendikTetap);
                    }
                    Context.Tendiks.Remove(pegawai.Tendik);
                }

                // Finally, delete the Pegawai
                Context.Pegawais.Remove(pegawai);
                await Context.SaveChangesAsync();
            }
        }
        
        //=================================================|||====================================================//
        // Mengambil pegawai berdasarkan Id dan NIP
        public async Task<Pegawai> PegawaiByIdAsync(Guid id)
        {
            return await Context.Set<Pegawai>()
                .FirstOrDefaultAsync(p => p.Id == id);//DbSet.FirstOrDefaultAsync(p => p.Id == id);
        }
        public async Task<Pegawai> PegawaiByNIPAsync(string NIP)
        {
            return await Context.Set<Pegawai>()
                .FirstOrDefaultAsync(p => p.NIP == NIP);//DbSet.FirstOrDefaultAsync(p => p.NIP == NIP);
        }

        // Get Dosen by Id dan NIP
        public async Task<Dosen> DosenByNIPAsync(string NIP)
        {
            return await Context.Dosens.FirstOrDefaultAsync(d => d.Pegawai.NIP == NIP);
        }
        public async Task<Dosen> DosenByIdAsync(Guid id)
        {
            return await Context.Dosens.FirstOrDefaultAsync(d => d.Id == id);
        }

        // Get DosenTetap by Id dan NIP
        public async Task<DosenTetap> DosenTetapByNIPAsync(string NIP)
        {
            return await Context.DosenTetaps.FirstOrDefaultAsync(dt => dt.Dosen.Pegawai.NIP == NIP);
        }
        public async Task<DosenTetap> DosenTetapByIdAsync(Guid id)
        {
            return await Context.DosenTetaps.FirstOrDefaultAsync(dt => dt.Id == id);
        }

        // Get Tendik by Id dan NIP
        public async Task<Tendik> TendikByNIPAsync(string NIP)
        {
            return await Context.Tendiks.FirstOrDefaultAsync(t => t.Pegawai.NIP == NIP);
        }
        public async Task<Tendik> TendikByIdAsync(Guid id)
        {
            return await Context.Tendiks.FirstOrDefaultAsync(t => t.Id == id);
        }

        // Get TendikTetap by NIP
        public async Task<TendikTetap> TendikTetapByNIPAsync(string NIP)
        {
            return await Context.TendikTetaps.FirstOrDefaultAsync(tt => tt.Tendik.Pegawai.NIP == NIP);
        }
        public async Task<TendikTetap> TendikTetapByIdAsync(Guid Id)
        {
            return await Context.TendikTetaps.FirstOrDefaultAsync(tt => tt.Id == Id);
        }
    }
}