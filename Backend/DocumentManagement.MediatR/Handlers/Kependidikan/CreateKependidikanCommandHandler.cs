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
    public class CreateKependidikanCommandHandler : IRequestHandler<CreateKependidikanCommand, ServiceResponse<CreateKependidikanDTO>>
    {
        private readonly IKependidikanRepository _kependidikanRepo;

        public CreateKependidikanCommandHandler(IKependidikanRepository kependidikanRepo)
        {
            _kependidikanRepo = kependidikanRepo;
        }

        public async Task<ServiceResponse<CreateKependidikanDTO>> Handle(CreateKependidikanCommand request, CancellationToken cancellationToken)
        {
            return await _kependidikanRepo.AddDataKependidikanAsync(request.KependidikanDto);
        }
    }
}
