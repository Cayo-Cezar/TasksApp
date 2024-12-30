using Application.Response;
using Application.Utils;
using Application.WorkspaceCQ.Queries;
using Application.WorkspaceCQ.ViewModels;
using AutoMapper;
using Domain.Entity;
using Infra.Repository.UnitOfWork;
using MediatR;
using System.Collections.Generic;

namespace Application.WorkspaceCQ.Handlers
{
    public class GetAllWorkspacesQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<GetAllWorkspacesQuery, ResponseBase<PaginatedList<WorkspaceViewModel>>>
    {
        private readonly IMapper _mapper = mapper;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        public async Task<ResponseBase<PaginatedList<WorkspaceViewModel>>> Handle(GetAllWorkspacesQuery request, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.UserRepository.Get(x => x.Id == request.UserId);

            if (user == null)
            {
                return new ResponseBase<PaginatedList<WorkspaceViewModel>>
                {
                    ResponseInfo = new ResponseInfo
                    {
                        Title = "Nenhum usuário encontrado", 
                        ErrorDescription = "Nenhum usuário encontrado com o 'id' informado",
                        HTTPStatus = 404
                    },
                    Value = null
                };
            }

            var workspaces = await _unitOfWork.WorkspaceRepository.GetAllWorkspacesAndUser(user.Id);

            return new ResponseBase<PaginatedList<WorkspaceViewModel>>
            {
                ResponseInfo = null, 
                Value = new PaginatedList<WorkspaceViewModel>(_mapper.Map<List<WorkspaceViewModel>>(workspaces), request.pageIndex, request.pageSize)

            };
        }
    }
}
