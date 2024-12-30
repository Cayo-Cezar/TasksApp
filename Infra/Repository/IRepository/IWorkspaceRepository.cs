using Domain.Entity;
using Infra.Repository.UnitOfWork;

namespace Infra.Repository.IRepository
{
    public interface IWorkspaceRepository : IBaseRepository<Workspace>
    {
        Task<Workspace?> GetWorkspaceAndUser(Guid workspaceId);
        Task<List<Workspace>> GetAllWorkspacesAndUser(Guid userId);
    }
}
