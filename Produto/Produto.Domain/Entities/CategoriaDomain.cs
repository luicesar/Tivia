using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Flunt.Validations;
using Produto.Shared.Entities;

namespace Produto.Domain.Entities {
  public class CategoriaDomain : Entity {

    protected CategoriaDomain () {
      Produtos = new List<ProdutoDomain> ();
    }

    public CategoriaDomain (
      string nome) {

      Nome = nome;

      AddNotifications (new Contract ()
        .Requires ()
        .IsNotNullOrEmpty (Nome, "Categoria.Nome", "O campo nome é obrigatório")
      );
    }

    public string Nome { get; set; }
    public ICollection<ProdutoDomain> Produtos { get; private set; }
  }
}