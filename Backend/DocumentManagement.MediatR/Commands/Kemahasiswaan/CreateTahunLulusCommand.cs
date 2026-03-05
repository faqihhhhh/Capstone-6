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
    public class CreateTahunLulusCommand : IRequest<int>
    {
        public TahunLulusWithTracerDTO TahunLulusDto { get; set; }
        public CreateTahunLulusCommand(TahunLulusWithTracerDTO dto)
        {
            TahunLulusDto = dto;
        }
    }
}
