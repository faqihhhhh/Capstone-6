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
    public class DeleteKependidikanCommandHandler : IRequestHandler<DeleteKependidikanCommand, ServiceResponse<string>>
    {
        private readonly IKependidikanRepository _repository;

        public DeleteKependidikanCommandHandler(IKependidikanRepository repository)
        {
            _repository = repository;
        }

        public async Task<ServiceResponse<string>> Handle(DeleteKependidikanCommand request, CancellationToken cancellationToken)
        {
            await _repository.DeleteDataKependidikanAsync(request.KependidikanDTO);
            return ServiceResponse<string>.ReturnSuccess();
        }
    }
}
