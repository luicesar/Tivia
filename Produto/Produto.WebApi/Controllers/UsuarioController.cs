using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection.Metadata;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Produto.Domain.Entities;
using Produto.Repository.Interfaces;
using Produto.Repository.Models;
using Produto.Shared.Helpers;
using Produto.Shared.Repository;
using Produto.WebApi.Auth;
using Produto.WebApi.Models;

namespace Produto.WebApi.Controllers {
  [Authorize ("Bearer"), Route ("api/[controller]")]
  public class UsuarioController : ControllerBase {

    public UsuarioController (IConfiguration configuration) {

    }

    [HttpGet, AllowAnonymous, Route ("info")]
    public string InformacoesDoUsuarioLogado () {

      //var usuario = AuthIdenty.UsuarioLogado (User)?.UID;
      var usuario = "";

      return usuario;
    }

    [HttpPost, AllowAnonymous, Route ("auth")]
    public IActionResult Authenticar ([FromServices] TokenConfiguration tokenConfigurations) {

      var user = new UsuarioLogadoModel ();
      user.UID = Guid.NewGuid ().ToString ("N");

      ClaimsIdentity identity = new ClaimsIdentity (
        new GenericIdentity (user.UID, "Login"),
        new [] {
          new Claim (JwtRegisteredClaimNames.Jti, Guid.NewGuid ().ToString ("N")),
            new Claim (JwtRegisteredClaimNames.UniqueName, user.UID),
            new Claim ("userInfo", JsonConvert.SerializeObject (user))
        }
      );

      DateTime dataCriacao = DateTime.Now;
      DateTime dataExpiracao = DateTime.Now.AddDays (365);

      var signinCredentials = new SigningCredentials (
        new SymmetricSecurityKey (Encoding.UTF8.GetBytes (tokenConfigurations.SecretyKey)),
        SecurityAlgorithms.HmacSha256);

      var handler = new JwtSecurityTokenHandler ();
      var securityToken = handler.CreateToken (new SecurityTokenDescriptor {
        Issuer = tokenConfigurations.Issuer,
          Audience = tokenConfigurations.Audience,
          SigningCredentials = signinCredentials,
          Subject = identity,
          NotBefore = dataCriacao,
          Expires = dataExpiracao
      });

      var tokenGenerated = handler.WriteToken (securityToken);

      return Ok (new {
        token = tokenGenerated,
          userInfo = user
      });
    }

  }
}