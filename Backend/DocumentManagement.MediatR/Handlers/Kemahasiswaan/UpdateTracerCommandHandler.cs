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
using Microsoft.AspNetCore.Http.HttpResults;


namespace DocumentManagement.MediatR.Handlers
{
    public class UpdateTracerCommandHandler : IRequestHandler<UpdateTracerCommand, ServiceResponse<StatusLulusanUpdateDTO>>
    {
        private readonly ITahunLulusRepository _tahunlulusRepository;
        private readonly KemahasiswaanService _kemahasiswaanService;

        public UpdateTracerCommandHandler(ITahunLulusRepository tahunLulusRepository, KemahasiswaanService kemahasiswaanService)
        {
            _tahunlulusRepository = tahunLulusRepository;
            _kemahasiswaanService = kemahasiswaanService;
        }
        public async Task<ServiceResponse<StatusLulusanUpdateDTO>> Handle(UpdateTracerCommand request, CancellationToken cancellationToken)
        {
            var existingStatus = await _tahunlulusRepository.StatusLulusByIdAsync(request.StatusId);
            if (existingStatus == null)
            {
                return ServiceResponse<StatusLulusanUpdateDTO>.ReturnFailed(404, "Status Lulusan tidak ditemukan.");
            }
            await _kemahasiswaanService.UpdateTracerAsync(request.StatusId, request.StatusDto);
            return ServiceResponse<StatusLulusanUpdateDTO>.ReturnSuccess();
        }
    }
}
