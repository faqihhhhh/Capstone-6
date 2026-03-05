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
    public class UpdatePrestasiCommandHandler: IRequestHandler<UpdatePrestasiCommand, ServiceResponse<UpdatePrestasiMhsDTO>>
    {
        private readonly IPrestasiRepository _prestasiMhsRepository;

        public UpdatePrestasiCommandHandler(IPrestasiRepository prestasiMhsRepository)
        {
            _prestasiMhsRepository = prestasiMhsRepository;
        }

        public async Task<ServiceResponse<UpdatePrestasiMhsDTO>> Handle(UpdatePrestasiCommand request, CancellationToken cancellationToken)
        { 
            await _prestasiMhsRepository.UpdatePrestasiMhsAsync(request.ThnAkademikId, request.PrestasiMhsDto);
            return ServiceResponse<UpdatePrestasiMhsDTO>.ReturnSuccess();
        }
    }
}
