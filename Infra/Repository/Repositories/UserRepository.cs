using Domain.Entity;
using Infra.Persistence;
using Infra.Repository.IRepository;
using Infra.Repository.UnitOfWork;

namespace Infra.Repository.Repositories
{
    public class UserRepository(TasksDbContext context) : BaseRepository<User>(context), IUserRepository 
    {
    }
}
