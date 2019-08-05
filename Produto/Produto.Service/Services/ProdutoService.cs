using System;
using System.Collections.Generic;
using System.Text;
using Produto.Domain.Entities;
using Produto.Repository.Interfaces;
using Produto.Service.Interfaces;
using Produto.Repository.DataContext;

namespace Produto.Service.Services
{
    public class ProdutoService : ServiceBase<ProdutoDomain>,IProdutoService { 
        
        public ProdutoService(ProdutoDataContext dbContext, IProdutoRepository Produto): base(dbContext)
        {
            
        }
    }
}