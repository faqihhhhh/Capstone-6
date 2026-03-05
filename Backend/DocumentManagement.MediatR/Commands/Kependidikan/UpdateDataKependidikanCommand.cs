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
    public class UpdateDataKependidikanCommand : IRequest<ServiceResponse<UpdateKependidikanDTO>>
    {
        public UpdateKependidikanDTO KependidikanDTO { get; set; }
        public UpdateDataKependidikanCommand(UpdateKependidikanDTO kependidikanDTO)
        {
            KependidikanDTO = kependidikanDTO;
        }
    }
}
