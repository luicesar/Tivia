
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq.Expressions;
using Produto.Domain.Entities;
using Produto.Repository;
using Produto.Shared.Entities;
using Produto.Repository.DataContext;
using Produto.Service.Interfaces;

namespace Produto.Service
{
    public class ServiceBase<T> : RepositoryBase<T>, IServiceBase<T> where T : Entity
    {        
        public ServiceBase(ProdutoDataContext dbContext) : base(dbContext)
        {
        }
        
    }
}

