using Application.Response;
using Application.WorkspaceCQ.Commands;
using Infra.Repository.UnitOfWork;
using MediatR;

namespace Application.WorkspaceCQ.Handlers
{
    public class DeleteWorkspaceCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<DeleteWorkspaceCommand, ResponseBase<DeleteWorkspaceCommand>>
    {
        public IUnitOfWork _unitOfWork = unitOfWork;
        public async Task<ResponseBase<DeleteWorkspaceCommand>> Handle(DeleteWorkspaceCommand request, CancellationToken cancellationToken)
        {
            var workspace = await _unitOfWork.WorkspaceRepository.Get(x => x.Id == request.Id);   

            if (workspace == null)
            {
                return new ResponseBase<DeleteWorkspaceCommand>
                {
                    ResponseInfo = new ResponseInfo
                    {
                        Title = "Workspace não encontrado", 
                        HTTPStatus = 404,
                        ErrorDescription = "Nenhum workspace encontrado com o 'Id' informado"
                    },
                    Value = null
                };
            }

             var listCards = _unitOfWork.ListsCardsRepository.GetAll().Where(x => x.Workspace == workspace).ToList();

             await _unitOfWork.ListsCardsRepository.DeleteRange(listCards);

             await _unitOfWork.WorkspaceRepository.Delete(workspace);

            _unitOfWork.Commit();

            return new ResponseBase<DeleteWorkspaceCommand>
            {
                ResponseInfo = null,
                Value = request
            };
        }
    }
}
