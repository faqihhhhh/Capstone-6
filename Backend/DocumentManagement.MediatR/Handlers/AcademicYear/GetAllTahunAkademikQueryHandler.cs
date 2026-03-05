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
    public class GetAllTahunAkademikQueryHandler : IRequestHandler<GetAllTahunAkademikQuery, IEnumerable<GetTahunAkademikDTO>>
    {
        private readonly IAkademikTahunRepository _tahunAkademikRepo;

        public GetAllTahunAkademikQueryHandler(IAkademikTahunRepository tahunAkademikRepo)
        {
            _tahunAkademikRepo = tahunAkademikRepo;
        }
        public async Task<IEnumerable<GetTahunAkademikDTO>> Handle(GetAllTahunAkademikQuery request, CancellationToken cancellationToken)
        {
            return await _tahunAkademikRepo.GetAllTahunAkademikAsync();
        }
    }
}
