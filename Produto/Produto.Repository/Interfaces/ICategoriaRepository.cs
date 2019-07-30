using System;
using System.Collections.Generic;
using System.Text;
using Produto.Domain.Entities;
using Produto.Shared.Repository;

namespace Produto.Repository.Interfaces {
  public interface ICategoriaRepository : IRepository<CategoriaDomain> { }
}