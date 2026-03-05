using DocumentManagement.Data.Dto;
using MediatR;
using DocumentManagement.Helper;

namespace DocumentManagement.MediatR.Commands
{
    public class CreateTracerCommand :  IRequest<ServiceResponse<TahunLulusWithTracerDTO>>
    {
        public TahunLulusWithTracerDTO TahunLulusDto { get; set; }

        public CreateTracerCommand(TahunLulusWithTracerDTO tahunLulusDto)
        {
            TahunLulusDto = tahunLulusDto;
        }
    }
}