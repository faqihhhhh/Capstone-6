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
    public class DeleteKegiatanPPMHandler : IRequestHandler<DeleteKegiatanPPMCommand, bool>
    {
        private readonly IKegiatanPPMRepository _repository;

        public DeleteKegiatanPPMHandler(IKegiatanPPMRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(DeleteKegiatanPPMCommand request, CancellationToken cancellationToken)
        {
            return await _repository.DeleteKegiatanPPMAsync(request.KegiatanId);
        }
    }
}
