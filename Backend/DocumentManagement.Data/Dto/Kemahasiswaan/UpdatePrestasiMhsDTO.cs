using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentManagement.Data.Dto
{
    public class UpdatePrestasiMhsDTO
    {
        //public int ThnAkademikId { get; set; } // Foreign key yang menghubungkan dengan TahunAkademik
        public int JmlhPKM { get; set; }
        public int JumlhMapres { get; set; }
        public int JumlhLombaNasional { get; set; }
        public int JumlhLombaInter { get; set; }
        public int JumlhInbound { get; set; }
        public int JumlhOutbound { get; set; }
    }
}