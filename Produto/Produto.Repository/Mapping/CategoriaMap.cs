using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Produto.Domain.Entities;

namespace Produto.Repository.Mapping {
  public class CategoriaMap : IEntityTypeConfiguration<CategoriaDomain> {
    public void Configure (EntityTypeBuilder<CategoriaDomain> builder) {
      builder.Property (t => t.ID).IsRequired ().HasColumnType ("int");
      builder.Property (t => t.Nome).IsRequired ().HasColumnType ("varchar(100)");
      builder.Property (t => t.Descricao).IsRequired ().HasColumnType ("varchar(500)");
      builder.Property (t => t.DataCriacao).HasColumnType ("datetime");

      builder.HasMany (x => x.Produtos).WithOne (x => x.Categoria)
        .HasForeignKey (x => x.CategoriaId)
        .HasPrincipalKey (x => x.ID);

      builder.ToTable ("Categoria");
    }
  }
}