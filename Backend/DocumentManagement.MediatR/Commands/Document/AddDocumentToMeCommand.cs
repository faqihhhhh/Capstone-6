using DocumentManagement.Data.Dto;
using DocumentManagement.Helper;
using MediatR;
using System;
using System.Collections.Generic;

namespace DocumentManagement.MediatR.Commands
{
   public class AddDocumentToMeCommand : IRequest<ServiceResponse<DocumentDto>>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
        public Guid CategoryId { get; set; }
        public Guid Id { get; set; } = Guid.NewGuid();
        public List<DocumentMetaDataDto> DocumentMetaDatas { get; set; } = new List<DocumentMetaDataDto>();
    }
}