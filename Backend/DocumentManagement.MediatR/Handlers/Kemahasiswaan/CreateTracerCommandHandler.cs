using DocumentManagement.Data.Dto;
using DocumentManagement.Helper;
using DocumentManagement.MediatR.Commands;
using DocumentManagement.Repository;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace DocumentManagement.MediatR.Handlers
{
    public class CreateTracerCommandHandler : IRequestHandler<CreateTracerCommand, ServiceResponse<TahunLulusWithTracerDTO>>
    {
        private readonly KemahasiswaanService _kemahasiswaanService;

        public CreateTracerCommandHandler(KemahasiswaanService kemahasiswaanService)
        {
            _kemahasiswaanService = kemahasiswaanService; // Assign PegawaiService
        }
        public async Task<ServiceResponse<TahunLulusWithTracerDTO>> Handle(CreateTracerCommand request, CancellationToken cancellationToken)
        {
            //var statusLulusanDto = request.TahunLulusDto.StatusLulusan;
            //await _kemahasiswaanService.CreateTracerAsync(request.TahunLulusDTO);
            //Console.WriteLine(statusLulusanDto);
            return ServiceResponse<TahunLulusWithTracerDTO>.ReturnSuccess();
        }
    }
}