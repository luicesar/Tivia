using System;
using Flunt.Validations;
using Produto.Shared.Entities;

namespace Produto.Domain.Entities {
  public class ProdutoDomain : Entity {

    protected ProdutoDomain () {

    }

    public ProdutoDomain (
      string nome, string descricao, decimal preco, int categoriaId) {

      Nome = nome;
      Descricao = descricao;
      Preco = preco;
      CategoriaId = categoriaId;

      AddNotifications (new Contract ()
        .Requires ()
        .IsNotNullOrEmpty (Nome, "Produto.Nome", "O campo nome é obrigatório")
        .IsNotNullOrEmpty (Descricao, "Produto.Descricao", "O campo descricao é obrigatório")
        .IsGreaterThan (CategoriaId, 0, "Produto.CategoriaId", "Informe uma categoria.")
        .IsGreaterThan (Preco, 0, "Produto.Preco", "O campo preco deve ser informado")
      );
    }

    public string Nome { get; set; }
    public string Descricao { get; set; }
    public decimal Preco { get; set; }
    public int CategoriaId { get; set; }
    public CategoriaDomain Categoria { get; set; }
  }
}