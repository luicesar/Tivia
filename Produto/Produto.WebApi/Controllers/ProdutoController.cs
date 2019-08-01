using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Produto.Domain.Entities;
using Produto.Repository.Interfaces;
using Produto.Shared.Repository;
using Produto.WebApi.Models;

namespace Produto.WebApi.Controllers {
    [Authorize ("Bearer"), Route ("api/[controller]")]
    public class ProdutoController : ControllerBase<ProdutoDomain, ProdutoViewModel> {
        private readonly IProdutoRepository repository;
        public ProdutoController (IProdutoRepository repository) : base (repository) {

            this.repository = repository;
        }

        [HttpGet, Route ("ListaComCategoria")]
        public IEnumerable<ProdutoSlimViewModel> GetProdutosCategoria () {

            var lista = new List<ProdutoSlimViewModel> ();

            var produtos = new List<ProdutoDomain> ();
            produtos = repository.GetAll (null, i => i.Categoria).ToList ();

            foreach (var item in produtos) {

                var prodView = new ProdutoSlimViewModel {
                    Nome = item.Nome,
                    Descricao = item.Descricao,
                    Preco = item.Preco,
                    CategoriaId = item.CategoriaId,
                    CategoriaNome = item.Categoria.Nome
                };

                lista.Add (prodView);
            }

            return lista;
        }
    }
}