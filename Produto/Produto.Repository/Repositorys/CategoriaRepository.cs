using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Produto.Domain.Entities;
using Produto.Repository.DataContext;
using Produto.Repository.Interfaces;

namespace Produto.Repository.Repositorys {
  public class CategoriaRepository : RepositoryBase<CategoriaDomain>, ICategoriaRepository {
    ProdutoContext Dbcontext;
    public CategoriaRepository (ProdutoContext dbcontext) : base (dbcontext) {
      this.Dbcontext = dbcontext;
    }
  }
}