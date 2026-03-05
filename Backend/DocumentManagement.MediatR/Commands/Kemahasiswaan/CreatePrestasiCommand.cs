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
    public class CreatePrestasiCommand: IRequest<ServiceResponse<CreatePrestasiDTO>>
    {
        public CreatePrestasiDTO TahunAkademikDto { get; set; }

        public CreatePrestasiCommand(CreatePrestasiDTO tahunAkademikDto)
        {
            TahunAkademikDto = tahunAkademikDto;
        }
    }
}