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
    public class DeletePegawaiCommandHandler : IRequestHandler<DeletePegawaiCommand, ServiceResponse<string>>
    {
        private readonly IPegawaiRepository _pegawaiRepository;
        private readonly PegawaiService _pegawaiService;

        public DeletePegawaiCommandHandler (IPegawaiRepository pegawaiRepository, PegawaiService pegawaiService)
        {
            _pegawaiRepository = pegawaiRepository;
            _pegawaiService = pegawaiService;   
        }

        public async Task<ServiceResponse<string>> Handle(DeletePegawaiCommand request, CancellationToken cancellationToken)
        {
            var existingPegawai = await _pegawaiRepository.PegawaiByIdAsync(request.Id);
            if (existingPegawai == null)
            {
                return ServiceResponse<string>.ReturnFailed(404, "Pegawai tidak ditemukan.");
            }

            await _pegawaiService.DeletePegawaiAsync(request.Id);
            return ServiceResponse<string>.ReturnSuccess();
        }
    }
}