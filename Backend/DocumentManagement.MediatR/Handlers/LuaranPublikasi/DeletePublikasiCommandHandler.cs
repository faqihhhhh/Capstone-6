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
    public class DeletePublikasiCommandHandler : IRequestHandler<DeletePublikasiCommand, DeletePublikasiDTO>
    {
        private readonly IPublikasiRepository _publikasiRepository;

        public DeletePublikasiCommandHandler(IPublikasiRepository publikasiRepository)
        {
            _publikasiRepository = publikasiRepository;
        }

        public async Task<DeletePublikasiDTO> Handle(DeletePublikasiCommand request, CancellationToken cancellationToken)
        {
            return await _publikasiRepository.DeletePublikasiAsync(request.PublikasiId);
        }
    }
}
