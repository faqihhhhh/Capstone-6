using DocumentManagement.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentManagement.Data.Dto
{
    public class MitraPengabdianDTO
    {
        public int Tahun { get; set; }
        public int JumlahMitraPemerintah { get; set; }
        public int JumlahMitraSwasta { get; set; }
        public int JumlahMitraLuarNegeri { get; set; }
        public decimal TotalDana { get; set; }
    }
}
