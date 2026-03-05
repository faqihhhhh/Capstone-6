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
    public class GetDataKependidikanByIdQueryHandler : IRequestHandler<GetDataKependidikanByIdQuery, List<GetKependidikanDTO>>
    {
        private readonly IKependidikanRepository _repository;

        public GetDataKependidikanByIdQueryHandler(IKependidikanRepository repository)
        {
            _repository = repository;
        }
        public async Task<List<GetKependidikanDTO>> Handle(GetDataKependidikanByIdQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetDataKependidikanByIdAsync(request.TahunAkademikId);
        }
    }
}
