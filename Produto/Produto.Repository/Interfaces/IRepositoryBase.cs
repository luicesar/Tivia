using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Produto.Shared.Entities;

namespace Produto.Repository.Interfaces {
  public interface IRepositoryBase<T> where T : Entity {
    bool Create (T entity);
    bool Update (int ID, T entity);
    IList<T> GetAll (Expression<Func<T, bool>> predicate);
    IList<T> GetAll ();
    T GetEntityByUID (int Id);
    T GetEntityByUID (int Id, params Expression<Func<T, object>>[] includeProperties);
    T GetEntityByUID (Expression<Func<T, bool>> predicate);
    bool DeleteEntity (int Id);
    bool Delete (int Id);
    IList<T> GetAll (Expression<Func<T, bool>> predicate = null, params Expression<Func<T, object>>[] includeProperties);
  }
}