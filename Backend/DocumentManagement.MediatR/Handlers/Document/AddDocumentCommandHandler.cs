using AutoMapper;
using DocumentManagement.Common.UnitOfWork;
using DocumentManagement.Data.Dto;
using DocumentManagement.Data.Entities;
using DocumentManagement.Domain;
using DocumentManagement.Helper;
using DocumentManagement.MediatR.Commands;
using DocumentManagement.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DocumentManagement.MediatR.Handlers
{
    public class AddDocumentCommandHandler : IRequestHandler<AddDocumentCommand, ServiceResponse<DocumentDto>>
    {
        private readonly IDocumentRepository _documentRepository;
        private readonly IUnitOfWork<DocumentContext> _uow;
        private readonly IMapper _mapper;
        private readonly UserInfoToken _userInfo;
        public AddDocumentCommandHandler(
            IDocumentRepository documentRepository,
            IMapper mapper,
            IUnitOfWork<DocumentContext> uow,
            UserInfoToken userInfo
            )
        {
            _documentRepository = documentRepository;
            _mapper = mapper;
            _uow = uow;
            _userInfo = userInfo;
        }

        public async Task<ServiceResponse<DocumentDto>> Handle(AddDocumentCommand request, CancellationToken cancellationToken)
        {
            var entityExist = await _documentRepository.FindBy(c => c.Name == request.Name).FirstOrDefaultAsync();
            if (entityExist != null)
            {
                return ServiceResponse<DocumentDto>.ReturnFailed(409, "Document already exist.");
            }
            request.DocumentMetaDatas = request.DocumentMetaDatas.Where(c => !string.IsNullOrWhiteSpace(c.Metatag)).ToList();
            var entity = _mapper.Map<Document>(request);
            entity.CreatedBy = Guid.Parse(_userInfo.Id);
            entity.CreatedDate = DateTime.UtcNow;
            _documentRepository.Add(entity);
            if (await _uow.SaveAsync() <= 0)
            {
                return ServiceResponse<DocumentDto>.ReturnFailed(500, "Error While Added Document");
            }
            var entityDto = _mapper.Map<DocumentDto>(entity);
            return ServiceResponse<DocumentDto>.ReturnResultWith200(entityDto);
        }
    }
}
