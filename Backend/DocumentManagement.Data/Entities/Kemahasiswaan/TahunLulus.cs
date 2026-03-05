using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentManagement.Data
{
    public class TahunLulus
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TahunId { get; set; }
        public int LulusanTahun {  get; set; }

        // Navigation Property - 1 TahunLulus bisa memiliki banyak StatusLulusan (1-to-many)
        public virtual ICollection<StatusLulusan> StatusLulusan { get; set; }
        
    }
}