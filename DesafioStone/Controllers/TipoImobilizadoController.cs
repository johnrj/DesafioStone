using DesafioStone.Models;
using DesafioStone.Negocio;
using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;

namespace DesafioStone.Controllers
{
    public class TipoImobilizadoController : ApiController
    {
        private ITipoImobilizadoNegocio negocio;
        public TipoImobilizadoController()
        {
            negocio = new TipoImobilizadoNegocio();
        }

        [ResponseType(typeof(List<TipoImobilizado>))]
        public IHttpActionResult GetTipoImobilizado()
        {
            try
            {
                var retorno = negocio.ObterTodos();
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
                var retorno = negocio.Obter(id);
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
                var retorno = negocio.Inserir(obj);
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
                var retorno = negocio.Atualizar(id, obj);
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
                var retorno = negocio.Apagar(id);
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
