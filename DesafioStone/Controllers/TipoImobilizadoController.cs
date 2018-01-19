using DesafioStone.Models;
using DesafioStone.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using MongoDB.Bson;

namespace DesafioStone.Controllers
{
    public class TipoImobilizadoController : ApiController
    {
        private ITipoImobilizadoRepository repo;
        public TipoImobilizadoController()
        {
            repo = new TipoImobilizadoRepository();
        }

        public List<TipoImobilizado> GetTipoImobilizados()
        {
            var retorno = repo.ObterTodos();

            return retorno;
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
                var retorno = repo.InserirTipoImobilizado(obj);
                return Created("DefaultApi", retorno);
            }
            catch (Excecoes.ObjetoDuplicadoException)
            {
                return Conflict();
            }
            catch
            {
                throw;
            }
        }

        [ResponseType(typeof(void))]
        public IHttpActionResult PutTipoImobilizado([FromUri] string id, [FromBody] TipoImobilizado obj)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                obj._id = new ObjectId(id);
                repo.Atualizar(obj);
                return StatusCode(HttpStatusCode.NoContent);
            }
            catch (Excecoes.ObjetoNaoEncontradoException)
            {
                return NotFound();
            }
            catch
            {
                throw;
            }
        }

        [ResponseType(typeof(TipoImobilizado))]
        public IHttpActionResult DeleteTipoImobilizado(string id)
        {
            try
            {
                var retorno = repo.Apagar(new ObjectId(id));
                return Ok(retorno);
            }
            catch (Excecoes.ObjetoNaoEncontradoException)
            {
                return NotFound();
            }
            catch
            {
                throw;
            }
        }
    }
}
