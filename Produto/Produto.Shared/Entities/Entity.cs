using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Flunt.Notifications;

namespace Produto.Shared.Entities {
  public abstract class Entity : Notifiable {
    [Key]
    public int ID { get; private set; }
    public DateTime DataCriacao { get; private set; }

    public Entity () {
      this.DataCriacao = DateTime.Now;
    }

    public void SetId (int ID) {
      this.ID = ID;
    }

  }
}