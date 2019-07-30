using System;
using System.Linq;
using Flunt.Notifications;
using Microsoft.EntityFrameworkCore;
using Produto.Domain.Entities;
using Produto.Repository.Mapping;

namespace Produto.Repository.DataContext {

  public class ProdutoContext : DbContext {
    public DbSet<CategoriaDomain> Categoria { get; set; }
    public DbSet<ProdutoDomain> Produto { get; set; }

    public ProdutoContext (DbContextOptions<ProdutoContext> options):
      base (options) {

      }

    protected override void OnModelCreating (ModelBuilder modelBuilder) {
      modelBuilder.Ignore<Notification> ();
      modelBuilder.Ignore<Notifiable> ();
      modelBuilder.ApplyConfiguration (new CategoriaMap ());
      modelBuilder.ApplyConfiguration (new ProdutoMap ());

      base.OnModelCreating (modelBuilder);
    }
  }

}