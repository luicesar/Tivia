using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Produto.Domain.Entities;
using Produto.Repository.DataContext;
using Produto.Repository.Interfaces;

namespace Produto.Repository.Repositorys {
  public class ProdutoRepository : RepositoryBase<ProdutoDomain>, IProdutoRepository {
    ProdutoDataContext Dbcontext;
    public ProdutoRepository (ProdutoDataContext dbcontext) : base (dbcontext) {
      this.Dbcontext = dbcontext;
    }
  }
}