using WebAPIMongoDBExample.Models;
using WebAPIMongoDBExample.Negocio;
using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;

namespace WebAPIMongoDBExample.Controllers
{
    public class TipoImobilizadoController : ApiController
    {
        ITipoImobilizadoNegocio _negocio;
        public TipoImobilizadoController(ITipoImobilizadoNegocio negocio)
        {
            _negocio = negocio;
        }

        [ResponseType(typeof(List<TipoImobilizado>))]
        public IHttpActionResult GetTipoImobilizado()
        {
            try
            {
                var retorno = _negocio.ObterTodos();
                return Ok(retorno);
            }
            catch(Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [ResponseType(typeof(TipoImobilizado))]
        public IHttpActionResult GetTipoImobilizado(string id)
        {
            try
            {
                var retorno = _negocio.Obter(id);
                return Ok(retorno);
            }
            catch(Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [ResponseType(typeof(TipoImobilizado))]
        public IHttpActionResult PostTipoImobilizado(TipoImobilizado obj)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            try
            {
                var retorno = _negocio.Inserir(obj);
                return Created("DefaultApi", retorno);
            }
            catch (Excecoes.ObjetoDuplicadoException)
            {
                return Conflict();
            }
            catch(Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [ResponseType(typeof(TipoImobilizado))]
        public IHttpActionResult PutTipoImobilizado([FromUri] string id, [FromBody] TipoImobilizado obj)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var retorno = _negocio.Atualizar(id, obj);
                return Ok(retorno);
            }
            catch (Excecoes.ObjetoNaoEncontradoException)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [ResponseType(typeof(TipoImobilizado))]
        public IHttpActionResult DeleteTipoImobilizado(string id)
        {
            try
            {
                var retorno = _negocio.Apagar(id);
                return Ok(retorno);
            }
            catch (Excecoes.ObjetoNaoEncontradoException)
            {
                return NotFound();
            }
            catch (Excecoes.AcaoProibidaException)
            {
                return StatusCode(HttpStatusCode.Forbidden);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
