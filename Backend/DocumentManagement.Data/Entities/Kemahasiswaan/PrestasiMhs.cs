using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DocumentManagement.Data.Entities;

namespace DocumentManagement.Data
{
    public class PrestasiMhs
    {
        //------------------------ Key dan Relasi -------------------------------//
        [Key, ForeignKey("TahunAkademik")]
        public int ThnAkademikId { get; set; } // Primary Key dan Foreign Key
        public TahunAkademik TahunAkademik { get; set; }
        // ---------------------------------------------------------------------//

        // ----------------------- Atribut Entitas -----------------------------//
        public int JmlhPKM { get; set; }
        public int JumlhMapres { get; set; }
        public int JumlhLombaNasional { get; set; }
        public int JumlhLombaInter {  get; set; }
        public int JumlhInbound {  get; set; }
        public int JumlhOutbound { get; set; }
        // ---------------------------------------------------------------------//
    }
}
