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
    public class GetMitraPenelitianHandler : IRequestHandler<GetMitraPenelitianQuery, List<MitraPenelitianDTO>>
    {
        private readonly IKegiatanPPMRepository _repository;

        public GetMitraPenelitianHandler(IKegiatanPPMRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<MitraPenelitianDTO>> Handle(GetMitraPenelitianQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetMitraPenelitianAsync();
        }
    }
}
