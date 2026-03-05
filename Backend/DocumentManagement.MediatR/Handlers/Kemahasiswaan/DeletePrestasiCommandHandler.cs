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
    public class DeletePrestasiCommandHandler : IRequestHandler<DeletePrestasiCommand, ServiceResponse<string>>
    {
        private readonly IPrestasiRepository _prestasiRepository;

        public DeletePrestasiCommandHandler(IPrestasiRepository prestasiRepository)
        {
            _prestasiRepository = prestasiRepository;
        }

        public async Task<ServiceResponse<string>> Handle(DeletePrestasiCommand request, CancellationToken cancellationToken)
        {
            await _prestasiRepository.DeletePrestasiMhsAsync(request.ThnAkademikId);
            return ServiceResponse<string>.ReturnSuccess();
        }
    }
}
