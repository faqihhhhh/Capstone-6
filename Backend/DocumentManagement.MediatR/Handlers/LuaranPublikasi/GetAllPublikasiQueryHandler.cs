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
    public class GetAllPublikasiQueryHandler : IRequestHandler<GetAllPublikasiQuery, IEnumerable<GetPublikasiDTO>>
    {
        private readonly IPublikasiRepository _publikasiRepository;

        public GetAllPublikasiQueryHandler(IPublikasiRepository publikasiRepository)
        {
            _publikasiRepository = publikasiRepository;
        }

        public async Task<IEnumerable<GetPublikasiDTO>> Handle(GetAllPublikasiQuery request, CancellationToken cancellationToken)
        {
            return await _publikasiRepository.GetAllPublikasiAsync();
        }
    }
}
