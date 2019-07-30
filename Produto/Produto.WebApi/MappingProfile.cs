using AutoMapper;
using Produto.Domain.Entities;
using Produto.WebApi.Models;

namespace Produto.WebApi {
  public class MappingProfile : Profile {
    public MappingProfile () {

      CreateMap<CategoriaDomain, CategoriaViewModel> ().ReverseMap ();
      CreateMap<ProdutoDomain, ProdutoViewModel> ().ReverseMap ();

    }
  }
}