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
    public class DeleteTahunAkademikCommandHandler: IRequestHandler<DeleteTahunAkademikCommand, ServiceResponse<string>>
    {
        private readonly IAkademikTahunRepository _tahunAkademikRepository;

        public DeleteTahunAkademikCommandHandler(IAkademikTahunRepository tahunAkademikRepository)
        {
            _tahunAkademikRepository = tahunAkademikRepository;
        }

        public async Task<ServiceResponse<string>> Handle(DeleteTahunAkademikCommand request, CancellationToken cancellationToken)
        {
            await _tahunAkademikRepository.DeleteTahunAkademikAsync(request.Id);
            return ServiceResponse<string>.ReturnSuccess();
        }
    }
}
