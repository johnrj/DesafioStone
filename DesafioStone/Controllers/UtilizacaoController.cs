using DesafioStone.Models;
using DesafioStone.Negocio;
using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;

namespace DesafioStone.Controllers
{
    public class UtilizacaoController : ApiController
    {
        IUtilizacaoNegocio negocio;
        public UtilizacaoController()
        {
            negocio = new UtilizacaoNegocio();
        }

        [ResponseType(typeof(List<Utilizacao>))]
        public IHttpActionResult GetUtilizacoes()
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

        [ResponseType(typeof(Utilizacao))]
        public IHttpActionResult PostUtilizacao(Utilizacao obj)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            try
            {
                var retorno = negocio.Inserir(obj);
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

        [ResponseType(typeof(Utilizacao))]
        public IHttpActionResult DeleteUtilizacao(string id)
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
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
