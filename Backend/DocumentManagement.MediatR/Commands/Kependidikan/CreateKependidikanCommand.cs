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
    public class CreateKependidikanCommand : IRequest<ServiceResponse<CreateKependidikanDTO>>
    {
        public CreateKependidikanDTO KependidikanDto {  get; set; }

        public CreateKependidikanCommand(CreateKependidikanDTO kependidikanDto)
        {
            KependidikanDto = kependidikanDto;
        }
    }
}