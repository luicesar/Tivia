using System;
using AutoMapper;
using Produto.Shared.Entities;

namespace Produto.WebApi.Models {
  public abstract class Model<T> : IModel<T> where T : Entity {
    public int ID { get; set; }
    public DateTime DataCriacao { get; set; }

    public virtual T MapForDomain () {
      return Mapper.Map<T> (this);
    }
  }
}