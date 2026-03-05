using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DocumentManagement.Data;
using DocumentManagement.Data.Dto;
using DocumentManagement.Helper;
using DocumentManagement.MediatR.Commands;
using DocumentManagement.MediatR.Queries;
using DocumentManagement.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;


namespace DocumentManagement.MediatR.Handlers
{
    public class DeleteTracerCommandHandler : IRequestHandler<DeleteTracerCommand, ServiceResponse<string>>
    {
        private readonly ITahunLulusRepository _tahunlulusRepository;
        private readonly KemahasiswaanService _kemahasiswaanService;

        public DeleteTracerCommandHandler(ITahunLulusRepository tahunlulusRepository, KemahasiswaanService kemahasiswaanService)
        {
            _tahunlulusRepository = tahunlulusRepository;
            _kemahasiswaanService = kemahasiswaanService;
        }

        public async Task<ServiceResponse<string>> Handle(DeleteTracerCommand request,CancellationToken cancellationToken)
        {
            var existingStatus = await _tahunlulusRepository.StatusLulusByIdAsync(request.StatusId);
            if(existingStatus == null)
            {
                return ServiceResponse<string>.ReturnFailed(404, "Status Lulusan tidak ditemukan.");
            }

            await _kemahasiswaanService.DeleteTracerAsync(request.StatusId);
            return ServiceResponse<string>.ReturnSuccess();
        }
    }
}
