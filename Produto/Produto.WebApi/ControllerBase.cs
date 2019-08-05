using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Produto.Service.Interfaces;
using Produto.WebApi.Models;
using Produto.Shared.Entities;

namespace Produto.WebApi {
  public abstract class ControllerBase<E, M> : Controller where E : Entity where M : IModel<E> {
    private IServiceBase<E> repository;
    public ControllerBase (IServiceBase<E> repository) {
      this.repository = repository;
    }

    [NonAction]
    private M GetByUid (int ID) {
      var result = repository.GetEntityByUID (ID);
      return Mapper.Map<M> (result);
    }

    [NonAction]
    private IEnumerable<M> Get () {
      var result = repository.GetAll ().ToList ();
      foreach (var item in result)
        yield return Mapper.Map<M> (item);
    }

    [NonAction]
    private IActionResult Create ([FromBody] M model) {
      var _mapDomain = model.MapForDomain ();

      if (_mapDomain.Invalid)
        return BadRequest (new { errors = _mapDomain.Notifications });

      var isCreated = repository.Create (_mapDomain);
      if (isCreated)
        return Created (string.Empty, Mapper.Map<M> (_mapDomain));
      else {
        //Validações adicionais usando o repository.
        if (_mapDomain.Valid)
          return StatusCode (500);
        else
          return BadRequest (new { errors = _mapDomain.Notifications });
      }
    }

    [NonAction]
    private IActionResult Update (int ID, [FromBody] M model) {
      var foundEntity = repository.GetEntityByUID (ID);
      if (foundEntity == null)
        return NotFound ();

      var _mapDomain = model.MapForDomain ();

      foundEntity = (E) _mapDomain;

      if (_mapDomain.Invalid)
        return BadRequest (
          new { errors = _mapDomain.Notifications });

      var isUpdated = repository.Update (ID, foundEntity);
      if (isUpdated)
        return Accepted (Mapper.Map<M> (foundEntity));
      else {
        //Validações adicionais usando o repository.
        if (_mapDomain.Valid)
          return StatusCode (500);
        else
          return BadRequest (new { errors = _mapDomain.Notifications });
      }
    }

    [NonAction]
    private IActionResult Delete (int ID) {
      var isDeleted = repository.DeleteEntity (ID);
      if (isDeleted)
        return NoContent ();
      else
        return NotFound ();
    }

    // [Authorize("Bearer")]
    [HttpGet]
    public virtual IEnumerable<M> GetAll () => this.Get ();

    // [Authorize("Bearer")]
    [HttpGet ("{ID}")]
    public virtual M GetEntityByUid (int ID) => this.GetByUid (ID);

    // [Authorize("Bearer")]
    [HttpPost]
    public virtual IActionResult CreateEntity ([FromBody] M model) => this.Create (model);

    // [Authorize("Bearer")]
    [HttpPut ("{ID}")]
    public virtual IActionResult UpdateEntity (int ID, [FromBody] M model) => this.Update (ID, model);

    // [Authorize("Bearer")]
    [HttpDelete ("{ID}")]
    public virtual IActionResult DeleteEntity (int ID) => this.Delete (ID);

  }
}