using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentManagement.Data.Dto
{
    public class GetAllTahunLulusDTO
    {
        
        public int LulusanTahun { get; set; }
        public List<GetAllStatusLulusanDTO> StatusLulusan { get; set; }
    }

    public class GetAllStatusLulusanDTO
    {
        public int StatusId { get; set; }
        public int Tahun_Input { get; set; }
        public double Presentase_Bekerja { get; set; }
        public double Presentase_LanjutStudi { get; set; }
        public double Presentase_Internship { get; set; }
        public double Presentase_Wirausaha { get; set; }
        public double Presentase_BelumKerja { get; set; }

        // Relasi 1-to-1 dengan entitas lain
        public GetAllMasaTungguKerjaDTO MasaTungguKerja { get; set; }
        public GetAllPosisiJabatanDTO PosisiJabatan { get; set; }
        public GetAllJenisTempatKerjaDTO JenisTempatKerja { get; set; }
        public GetAllTingkatTempatKerjaDTO TingkatTempatKerja { get; set; }
    }
    public class GetAllMasaTungguKerjaDTO
    {
        public double Presentase_diatas6Bulan { get; set; }
        public double Presentase_dibawah6Bulan { get; set; }
    }

    public class GetAllJenisTempatKerjaDTO
    {
        public double Presentase_BUMN { get; set; }
        public double Presentase_Organisasi_Multilateral { get; set; }
        public double Presentase_Instansi_Pemerintah {  get; set; }
        public double Presentase_Organisasi_NonProfit { get; set; }
        public double Presentase_Wirausaha {  get; set; }
        public double Presentase_Lainnya { get; set; }
    }

    public class GetAllPosisiJabatanDTO
    {
        public double Presentase_Founder { get; set; }
        public double Presentase_CoFounder { get; set; }
        public double Presentase_Staf { get; set; }
        public double Presentase_Freelancer { get; set; }
    }

    public class GetAllTingkatTempatKerjaDTO
    {
        public double Presentase_Lokal { get; set; }
        public double Presentase_Nasional { get; set; }
        public double Presentase_MultiNasional { get; set; }
    }
}