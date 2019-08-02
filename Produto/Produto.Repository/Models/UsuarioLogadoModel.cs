using System;
using System.Collections.Generic;
using Produto.Domain.Entities;

namespace Produto.Repository.Models {
  public class UsuarioLogadoModel {
    public string UID { get; set; }
    public DateTime DataExpiracao { get; set; }
    public UsuarioLogadoModel () { }

  }
}