using AutoMapper;
using DocumentManagement.Common.UnitOfWork;
using DocumentManagement.Data;
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
    public class AddDocumentToMeCommandHandler : IRequestHandler<AddDocumentToMeCommand, ServiceResponse<DocumentDto>>
    {
        private readonly IDocumentRepository _documentRepository;
        private readonly IUnitOfWork<DocumentContext> _uow;
        private readonly IMapper _mapper;
        private readonly UserInfoToken _userInfo;
        private readonly IDocumentUserPermissionRepository _documentUserPermissionRepository;
        private readonly IDocumentAuditTrailRepository _documentAuditTrailRepository;
        public AddDocumentToMeCommandHandler(
            IDocumentRepository documentRepository,
            IMapper mapper,
            IUnitOfWork<DocumentContext> uow,
            UserInfoToken userInfo,
             IDocumentUserPermissionRepository documentUserPermissionRepository,
              IDocumentAuditTrailRepository documentAuditTrailRepository
            )
        {
            _documentRepository = documentRepository;
            _mapper = mapper;
            _uow = uow;
            _userInfo = userInfo;
            _documentUserPermissionRepository = documentUserPermissionRepository;
            _documentAuditTrailRepository = documentAuditTrailRepository;
        }

        public async Task<ServiceResponse<DocumentDto>> Handle(AddDocumentToMeCommand request, CancellationToken cancellationToken)
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

            var documentUserPermission = new DocumentUserPermission
            {
                Id = Guid.NewGuid(),
                DocumentId = entity.Id,
                UserId = Guid.Parse(_userInfo.Id),
                IsTimeBound = false,
                IsAllowDownload = true,
                CreatedBy = Guid.Parse(_userInfo.Id),
                CreatedDate = DateTime.UtcNow

            };
            _documentUserPermissionRepository.Add(documentUserPermission);

            var documentAudit = new DocumentAuditTrail()
            {
                DocumentId = entity.Id,
                CreatedBy = Guid.Parse(_userInfo.Id),
                CreatedDate = DateTime.UtcNow,
                OperationName = DocumentOperation.Add_Permission,
                AssignToUserId = Guid.Parse(_userInfo.Id)
            };
            _documentAuditTrailRepository.Add(documentAudit);

            if (await _uow.SaveAsync() <= 0)
            {
                return ServiceResponse<DocumentDto>.ReturnFailed(500, "Error While Added Document");
            }
            var entityDto = _mapper.Map<DocumentDto>(entity);
            return ServiceResponse<DocumentDto>.ReturnResultWith200(entityDto);
        }
    }
}