using Infra.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Repository.UnitOfWork
{
    public interface IUnitOfWork
    {
         IUserRepository UserRepository { get; }
        void Commit();
    }
}
