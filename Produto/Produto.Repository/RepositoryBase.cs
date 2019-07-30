using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Produto.Repository.DataContext;
using Produto.Shared.Entities;
using Produto.Shared.Repository;

namespace Produto.Repository {
  public abstract class RepositoryBase<T> : IRepository<T> where T : Entity {
    private readonly DbContext context;

    public RepositoryBase (DbContext context) {
      this.context = context;
    }

    public virtual IList<T> GetAll () {
      return GetAll (null);
    }

    public virtual IList<T> GetAll (Expression<Func<T, bool>> predicate) {
      return GetAll (predicate, null);
    }

    public virtual IList<T> GetAll (Expression<Func<T, bool>> predicate = null, params Expression<Func<T, object>>[] includeProperties) {
      IQueryable<T> query = context.Set<T> ();
      if (predicate != null)
        query = query.Where (predicate);
      if (includeProperties != null)
        query = includeProperties.Aggregate (query, (current, include) => current.Include (include));
      return query.AsNoTracking ().ToList ();
    }

    public virtual T GetEntityByUID (int ID) {
      return GetEntityByUID (ID, null);
    }

    public virtual T GetEntityByUID (int ID, params Expression<Func<T, object>>[] includeProperties) {
      IQueryable<T> query = context.Set<T> ();
      if (includeProperties != null)
        query = includeProperties.Aggregate (query, (current, include) => current.Include (include));
      return query.Where (x => x.ID == ID)
        .AsNoTracking ()
        .FirstOrDefault ();
    }

    public T GetEntityByUID (Expression<Func<T, bool>> predicate) {
      IQueryable<T> query = context.Set<T> ();
      query = query.Where (predicate);
      return query.AsNoTracking ().FirstOrDefault ();
    }

    public bool DeleteEntity (int ID) {
      try {
        T entity = GetEntityByUID (ID);
        if (entity != null) {
          Delete (ID);
          return true;
        } else {
          return false;
        }
      } catch (Exception) {
        return false;
      }
    }

    public bool Delete (int ID) {
      try {
        T entity = context.Set<T> ().Where (x => x.ID == ID).FirstOrDefault ();
        context.Entry (entity).State = EntityState.Deleted;
        context.SaveChanges ();
        return true;
      } catch (Exception) {
        return false;
      }
    }

    public virtual bool Update (int ID, T entity) {
      try {
        entity.SetId (ID);
        context.Update (entity);
        context.SaveChanges ();
        return true;
      } catch (Exception ex) {
        var err = ex;
        return false;
      }
    }

    public virtual bool Create (T entity) {
      try {
        context.Add<Entity> (entity);
        context.SaveChanges ();
        return true;
      } catch (Exception ex) {
        var err = ex;
        return false;
      }
    }

    private IQueryable<T> Include (IQueryable<T> query, params Expression<Func<T, object>>[] includeProperties) {
      foreach (var property in includeProperties)
        query = query.Include (property);
      return query;
    }

  }
}