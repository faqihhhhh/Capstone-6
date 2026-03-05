using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DocumentManagement.Data.Dto;
using DocumentManagement.Helper;
using MediatR;

namespace DocumentManagement.MediatR.Commands
{
    public class DeleteKependidikanCommand : IRequest<ServiceResponse<string>>
    {
        public DeleteKependidikanDTO KependidikanDTO { get; set; }

        public DeleteKependidikanCommand(DeleteKependidikanDTO kependidikanDTO)
        {
            KependidikanDTO = kependidikanDTO;
        }
    }
}