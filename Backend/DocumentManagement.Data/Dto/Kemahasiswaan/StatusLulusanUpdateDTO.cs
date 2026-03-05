using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentManagement.Data.Dto
{
    public class StatusLulusanUpdateDTO
    {
        [Required]
        //public int StatusId { get; set; }  // ID StatusLulusan untuk update
        public int Bekerja { get; set; }
        public int LanjutStudi { get; set; }
        public int Internship { get; set; }
        public int Berwirausaha { get; set; }
        public int BelumKerja { get; set; }

        // Relasi 1-to-1 dengan entitas lainnya
        public MasaTungguKerjaDTO MasaTungguKerja { get; set; }
        public PosisiJabatanDTO PosisiJabatan { get; set; }
        public JenisTempatKerjaDTO JenisTempatKerja { get; set; }
        public TingkatTempatKerjaDTO TingkatTempatKerja { get; set; }
    }
}
