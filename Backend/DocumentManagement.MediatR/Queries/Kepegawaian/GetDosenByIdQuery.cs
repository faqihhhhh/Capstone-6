using DocumentManagement.Data.Dto;
using MediatR;
using System;

namespace DocumentManagement.MediatR.Queries
{
    public class GetDosenByIdQuery : IRequest<DosenDetailDTO>
    {
        public Guid Id { get; set; }

        public GetDosenByIdQuery(Guid id)
        {
            Id = id;
        }
    }

}