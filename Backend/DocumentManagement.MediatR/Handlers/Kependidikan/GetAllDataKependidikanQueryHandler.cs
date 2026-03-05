using DocumentManagement.Data.Dto;
using DocumentManagement.MediatR.Queries;
using DocumentManagement.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DocumentManagement.MediatR.Handlers
{
    public class GetAllDataKependidikanQueryHandler : IRequestHandler<GetAllDataKependidikanQuery, IEnumerable<GetKependidikanDTO>>
    {
        private readonly IKependidikanRepository _kependidikanRepo;

        public GetAllDataKependidikanQueryHandler(IKependidikanRepository kependidikanRepository)
        {
            _kependidikanRepo = kependidikanRepository;
        }

        public async Task<IEnumerable<GetKependidikanDTO>> Handle(GetAllDataKependidikanQuery request, CancellationToken cancellationToken)
        {
            return await _kependidikanRepo.GetAllDataKependidikanAsync();
        }
    }
}