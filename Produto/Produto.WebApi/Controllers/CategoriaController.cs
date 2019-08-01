using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Produto.Domain.Entities;
using Produto.Repository.Interfaces;
using Produto.Shared.Repository;
using Produto.WebApi.Auth;
using Produto.WebApi.Models;

namespace Produto.WebApi.Controllers {
    [Authorize ("Bearer"), Route ("api/[controller]")]
    public class CategoriaController : ControllerBase<CategoriaDomain, CategoriaViewModel> {
        private readonly ICategoriaRepository _repository;
        public CategoriaController (ICategoriaRepository repository) : base (repository) {
            _repository = repository;
        }
    }
}