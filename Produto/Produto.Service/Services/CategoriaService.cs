using System;
using System.Collections.Generic;
using System.Text;
using Produto.Domain.Entities;
using Produto.Repository.Interfaces;
using Produto.Service.Interfaces;
using Produto.Repository.DataContext;

namespace Produto.Service.Services
{
    public class CategoriaService : ServiceBase<CategoriaDomain>, ICategoriaService
    {
        public CategoriaService(ProdutoDataContext dbContext, ICategoriaRepository categoria): base(dbContext)
        {
            
        }
    }
}