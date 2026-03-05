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
    public class UpdateKegiatanPPMHandler : IRequestHandler<UpdateKegiatanPPMCommand, bool>
    {
        private readonly IKegiatanPPMRepository _repository;

        public UpdateKegiatanPPMHandler(IKegiatanPPMRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(UpdateKegiatanPPMCommand request, CancellationToken cancellationToken)
        {
            return await _repository.UpdateKegiatanPPMAsync(request.KegiatanId, request.UpdateDto);
        }
    }
}