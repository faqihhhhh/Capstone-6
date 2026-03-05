using AutoMapper;
using DocumentManagement.Data;
using DocumentManagement.Data.Dto;
using DocumentManagement.Helper;
using DocumentManagement.MediatR.Commands;
using DocumentManagement.MediatR.Queries;
using DocumentManagement.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace DocumentManagement.MediatR.Handlers
{
    public class UpdatePegawaiCommandHandler : IRequestHandler<UpdatePegawaiCommand, ServiceResponse<PegawaiDTO>>
    {
        private readonly IPegawaiRepository _pegawaiRepository;
        private readonly PegawaiService _pegawaiService;
       

        public UpdatePegawaiCommandHandler(IPegawaiRepository pegawaiRepository,  PegawaiService pegawaiService)
        {
            _pegawaiRepository = pegawaiRepository;
            _pegawaiService = pegawaiService;
        }

        public async Task<ServiceResponse<PegawaiDTO>> Handle(UpdatePegawaiCommand request, CancellationToken cancellationToken)
        {
            var existingPegawai = await _pegawaiRepository.PegawaiByIdAsync(request.Id);
            if (existingPegawai == null)
            {
                return ServiceResponse<PegawaiDTO>.ReturnFailed(404, "Pegawai tidak ditemukan.");
            }
            await _pegawaiService.UpdatePegawaiAsync(request.Id, request.PegawaiDto);
            return ServiceResponse<PegawaiDTO>.ReturnSuccess();
        }
    }
}