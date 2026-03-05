using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DocumentManagement.Data.Dto
{
    public class TahunLulusWithTracerDTO : IValidatableObject
    {
        [Required]
        public int LulusanTahun { get; set; }  // Tahun Lulusan
        public List<StatusLulusanDTO> StatusLulusan { get; set; }
        //public ICollection<StatusLulusanDTO> StatusLulusan { get; set; }
        //public List<StatusLulusanDTO> StatusLulusan { get; set; } = new List<StatusLulusanDTO>();
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (LulusanTahun <= 0)
            {
                yield return new ValidationResult("LulusanTahun harus lebih besar dari 0", new[] { nameof(LulusanTahun) });
            }
            foreach (var status in StatusLulusan)
            {
                if (status.Tahun_Input < LulusanTahun)
                {
                    yield return new ValidationResult(
                        $"Tahun_Input {status.Tahun_Input} harus lebih besar sama dengan dari Tahun Lulusan yaitu {LulusanTahun}.",
                        new[] { nameof(StatusLulusan) });
                }
            }
        }
    }
}