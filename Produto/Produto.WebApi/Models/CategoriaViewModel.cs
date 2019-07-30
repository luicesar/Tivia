using System;
using System.Collections.Generic;
using AutoMapper;
using Newtonsoft.Json;
using Produto.Domain.Entities;
using Produto.WebApi.Models;

namespace Produto.WebApi.Models {
  public class CategoriaViewModel : Model<CategoriaDomain> {
    public string Nome { get; set; }
    public string Descricao { get; set; }

    [JsonIgnore]
    public ICollection<ProdutoDomain> Produtos { get; set; }
  }
}