using DesafioStone.Models;
using DesafioStone.Repository;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace DesafioStone.Controllers
{
    public class UtilizacaoController : ApiController
    {
        IUtilizacaoRepository repo;
        public UtilizacaoController()
        {
            repo = new UtilizacaoRepository();
        }

        public List<Utilizacao> GetUtilizacoes()
        {
            var retorno = repo.ObterTodos();
            return retorno;
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
                var retorno = repo.Inserir(obj);
                return Created("DefaultAPI", retorno);
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
                obj._id = ObjectId.Parse(id);
                var retorno = repo.Atualizar(obj);
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
                var retorno = repo.Apagar(ObjectId.Parse(id));
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
