using Application.Response;
using Application.WorkspaceCQ.Commands;
using Application.WorkspaceCQ.ViewModels;
using AutoMapper;
using Domain.Entity;
using Infra.Repository.UnitOfWork;
using MediatR;

namespace Application.WorkspaceCQ.Handlers
{
    public class CreateWorkspaceCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<CreateWorkspaceCommand, ResponseBase<WorkspaceViewModel>>
    {
        private readonly IMapper _mapper = mapper;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        public async Task<ResponseBase<WorkspaceViewModel>> Handle(CreateWorkspaceCommand request, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.UserRepository.Get(x => x.Id == request.UserId);
            if (user == null) 
            {
                return new ResponseBase<WorkspaceViewModel>
                {
                    ResponseInfo = new ResponseInfo
                    {
                        ErrorDescription = "Nenhum usuário encontrado com o id informado",
                        HTTPStatus = 400,
                        Title = "Usuário não encontrado"
                    }
                };
            }

            var workspace = new Workspace()
            {
                User = user, 
                Title = request.Title
            };

            await _unitOfWork.WorkspaceRepository.Create(workspace);  
            _unitOfWork.Commit();

            return new ResponseBase<WorkspaceViewModel>
            {
                ResponseInfo = null, 
                Value = _mapper.Map<WorkspaceViewModel>(workspace)
            };

        }
    }
}
