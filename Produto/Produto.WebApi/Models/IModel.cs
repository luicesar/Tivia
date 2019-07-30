using Produto.Shared.Entities;

namespace Produto.WebApi.Models {
  public interface IModel<D> where D : Entity {
    D MapForDomain ();
  }
}