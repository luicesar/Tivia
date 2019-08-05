using System;
using System.Collections.Generic;
using System.Text;
using System.Linq.Expressions;
using Produto.Domain.Entities;
using Produto.Repository.Interfaces;
using Produto.Shared.Entities;

namespace Produto.Service.Interfaces
{
    public interface IServiceBase<T>: IRepositoryBase<T> where T : Entity
    {
     
    }
}