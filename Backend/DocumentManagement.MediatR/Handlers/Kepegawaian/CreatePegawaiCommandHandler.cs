using AutoMapper;
using DocumentManagement.Data;
using DocumentManagement.Data.Dto;
using DocumentManagement.Helper;
using DocumentManagement.MediatR.Commands;
using DocumentManagement.MediatR.Queries;
using DocumentManagement.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace DocumentManagement.MediatR.Handlers
{
    public class CreatePegawaiCommandHandler : IRequestHandler<CreatePegawaiCommand, ServiceResponse<PegawaiDTO>>
    {
        private readonly PegawaiService _pegawaiService;

        public CreatePegawaiCommandHandler(PegawaiService pegawaiService)
        {
            _pegawaiService = pegawaiService; // Assign PegawaiService
        }

        public async Task<ServiceResponse<PegawaiDTO>> Handle(CreatePegawaiCommand request, CancellationToken cancellationToken)
        {
            var pegawaiDto = request.PegawaiDto;
            if (String.IsNullOrEmpty(pegawaiDto.NIP) || pegawaiDto.Id == Guid.Empty)
            {
                return ServiceResponse<PegawaiDTO>.ReturnFailed(400, "NIP (Id) tidak boleh kosong!.");
            }
            await _pegawaiService.CreatePegawaiAsync(request.PegawaiDto);

            return ServiceResponse<PegawaiDTO>.ReturnSuccess();
        }
    }
}