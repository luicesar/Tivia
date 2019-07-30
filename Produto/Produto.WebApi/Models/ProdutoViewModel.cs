using System;
using System.Collections.Generic;
using AutoMapper;
using Newtonsoft.Json;
using Produto.Domain.Entities;
using Produto.WebApi.Models;

namespace Produto.WebApi.Models {
  public class ProdutoViewModel : Model<ProdutoDomain> {
    public string Nome { get; set; }
    public string Descricao { get; set; }
    public decimal Preco { get; set; }
    public int CategoriaId { get; set; }
  }
}