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
    public class GetAllPrestasiQueryHandler : IRequestHandler<GetAllPrestasiQuery, IEnumerable<GetAllPrestasiDTO>>
    {
        private readonly IPrestasiRepository _tahunAkademikRepo;

        public GetAllPrestasiQueryHandler(IPrestasiRepository tahunAkademikRepo)
        {
            _tahunAkademikRepo = tahunAkademikRepo;
        }
        public async Task<IEnumerable<GetAllPrestasiDTO>> Handle(GetAllPrestasiQuery request, CancellationToken cancellationToken)
        {
            return await _tahunAkademikRepo.GetAllTahunAkademikAsync();
        }
    }
}