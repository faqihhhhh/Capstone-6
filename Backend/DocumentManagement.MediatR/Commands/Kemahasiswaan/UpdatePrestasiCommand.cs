using DocumentManagement.Data.Dto;
using DocumentManagement.Helper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentManagement.MediatR.Commands
{
    public class UpdatePrestasiCommand : IRequest<ServiceResponse<UpdatePrestasiMhsDTO>>
    {
        public int ThnAkademikId { get; set; } // ID dari URL
        public UpdatePrestasiMhsDTO PrestasiMhsDto { get; set; } // Data dari body
        public UpdatePrestasiCommand(int thnAkademikId, UpdatePrestasiMhsDTO prestasiMhsDto)
        {
            ThnAkademikId = thnAkademikId;
            PrestasiMhsDto = prestasiMhsDto;
        }
    }
}