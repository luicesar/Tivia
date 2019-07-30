using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Produto.Domain.Entities;

namespace Produto.Repository.Mapping {
  public class ProdutoMap : IEntityTypeConfiguration<ProdutoDomain> {
    public void Configure (EntityTypeBuilder<ProdutoDomain> builder) {
      builder.Property (t => t.ID).IsRequired ().HasColumnType ("int");
      builder.Property (t => t.Nome).IsRequired ().HasColumnType ("varchar(100)");
      builder.Property (t => t.Descricao).IsRequired ().HasColumnType ("varchar(500)");
      builder.Property (t => t.Preco).IsRequired ().HasColumnType ("decimal(15,2)");
      builder.Property (t => t.DataCriacao).HasColumnType ("datetime");

      builder.ToTable ("Produto");
    }
  }
}