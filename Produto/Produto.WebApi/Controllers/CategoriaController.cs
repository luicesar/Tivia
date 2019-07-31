using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Produto.Domain.Entities;
using Produto.Repository.Interfaces;
using Produto.Shared.Repository;
using Produto.WebApi.Models;

namespace Produto.WebApi.Controllers {
    [Route ("api/[controller]")]
    public class CategoriaController : ControllerBase<CategoriaDomain, CategoriaViewModel> {
        public CategoriaController (ICategoriaRepository repository) : base (repository) { }
    }
}