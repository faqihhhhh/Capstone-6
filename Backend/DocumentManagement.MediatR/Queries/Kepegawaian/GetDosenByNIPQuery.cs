using DocumentManagement.Data.Dto;
using MediatR;
using System;

namespace DocumentManagement.MediatR.Queries
{
    public class GetDosenByNIPQuery : IRequest<DosenDetailDTO>
    {
        public string NIP { get; set; }

        public GetDosenByNIPQuery(string nip)
        {
            NIP = nip;
        }
    }

}

