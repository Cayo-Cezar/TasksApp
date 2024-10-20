using System.Linq.Expressions;

namespace Infra.Repository.UnitOfWork
{
    public interface IBaseRepository<T> where T : class
    {
        //Get, Update, Delete, GetAll, Create

        Task <T?> Get(Expression<Func<T, bool>> expression); // x =>  x.id == var/param
        IEnumerable<T> GetAll();
        Task<T> Create(T command);
        Task<T> Update(T commandUpdate);
        Task Delete(Guid id);

    }
}
