using WebAPIMongoDBExample.Models;
using WebAPIMongoDBExample.Negocio;
using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;

namespace WebAPIMongoDBExample.Controllers
{
    public class UtilizacaoController : ApiController
    {
        IUtilizacaoNegocio _negocio;
        public UtilizacaoController(IUtilizacaoNegocio negocio)
        {
            _negocio = negocio;
        }

        [ResponseType(typeof(List<Utilizacao>))]
        public IHttpActionResult GetUtilizacoes()
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

        [ResponseType(typeof(Utilizacao))]
        public IHttpActionResult PostUtilizacao(Utilizacao obj)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            try
            {
                var retorno = _negocio.Inserir(obj);
                return Created("DefaultAPI", retorno);
            }
            catch (Excecoes.ObjetoNaoEncontradoException)
            {
                return NotFound();
            }
            catch (Excecoes.AcaoProibidaException)
            {
                return StatusCode(HttpStatusCode.Forbidden);
            }
            catch(Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [ResponseType(typeof(Utilizacao))]
        public IHttpActionResult PutUtilizacao([FromUri] string id, [FromBody] Utilizacao obj)
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

        [ResponseType(typeof(Utilizacao))]
        public IHttpActionResult DeleteUtilizacao(string id)
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
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
