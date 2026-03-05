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
    public class DeletePegawaiByNIPCommandHandler : IRequestHandler<DeletePegawaiByNIPCommand, ServiceResponse<string>>
    {
        private readonly IPegawaiRepository _pegawaiRepository;
        private readonly PegawaiService _pegawaiService;

        public DeletePegawaiByNIPCommandHandler(IPegawaiRepository pegawaiRepository, PegawaiService pegawaiService)
        {
            _pegawaiRepository = pegawaiRepository;
            _pegawaiService = pegawaiService;   
        }

        public async Task<ServiceResponse<string>> Handle(DeletePegawaiByNIPCommand request, CancellationToken cancellationToken)
        {
            var existingPegawai = await _pegawaiRepository.PegawaiByNIPAsync(request.NIP);
            if (existingPegawai == null)
            {
                return ServiceResponse<string>.ReturnFailed(404, "Pegawai tidak ditemukan.");
            }

            await _pegawaiService.DeletePegawaiByNIPAsync(request.NIP);
            return ServiceResponse<string>.ReturnSuccess();
        }
    }
}