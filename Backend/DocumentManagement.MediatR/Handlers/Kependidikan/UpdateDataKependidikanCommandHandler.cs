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
    public class UpdateDataKependidikanCommandHandler : IRequestHandler<UpdateDataKependidikanCommand, ServiceResponse<UpdateKependidikanDTO>>
    {
        private readonly IKependidikanRepository _repository;
        public UpdateDataKependidikanCommandHandler(IKependidikanRepository repository)
        {
            _repository = repository;
        }
        public async Task<ServiceResponse<UpdateKependidikanDTO>> Handle(UpdateDataKependidikanCommand request, CancellationToken cancellationToken)
        {
            await _repository.UpdateDataKependidikanAsync(request.KependidikanDTO);
            return ServiceResponse<UpdateKependidikanDTO>.ReturnSuccess();
        }
    }
}