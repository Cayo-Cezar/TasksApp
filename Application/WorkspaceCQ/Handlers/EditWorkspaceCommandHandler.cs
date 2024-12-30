using Application.Response;
using Application.WorkspaceCQ.Commands;
using Application.WorkspaceCQ.ViewModels;
using AutoMapper;
using Domain.Entity;
using Infra.Repository.UnitOfWork;
using MediatR;

namespace Application.WorkspaceCQ.Handlers
{
    public class EditWorkspaceCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<EditWorkspaceCommand, ResponseBase<WorkspaceViewModel>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;
        public async Task<ResponseBase<WorkspaceViewModel>> Handle(EditWorkspaceCommand request, CancellationToken cancellationToken)
        {
            var workspace = await _unitOfWork.WorkspaceRepository.GetWorkspaceAndUser(request.Id);

            if (workspace == null)
            {
                return new ResponseBase<WorkspaceViewModel>
                {
                    ResponseInfo = new()
                    {
                        Title = "Workspace não encontrado",
                        ErrorDescription = "Nenhum workspace encontrado com o 'Id' informado",
                        HTTPStatus = 404
                    },
                    Value = null
                };

            }

            if (request.Title != null)
            {
                workspace.Title = request.Title;
            }
            if (request.Status != null)
            {
                workspace.Status = request.Status;
            }

            _unitOfWork.Commit();

            return new ResponseBase<WorkspaceViewModel>
            {
                ResponseInfo = null,
                Value = _mapper.Map<WorkspaceViewModel>(workspace)
            };
        }
    }
}
