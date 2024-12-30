﻿using Domain.Entity;
using Infra.Repository.UnitOfWork;

namespace Infra.Repository.IRepository
{
    public interface IListsCardsRepository : IBaseRepository<ListCard>
    {
    }
}
