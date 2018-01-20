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

        [ResponseType(typeof(List<TipoImobilizado>))]
        public IHttpActionResult GetTipoImobilizado()
        {
            try
            {
                var retorno = repo.ObterTodos();
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
                var retorno = repo.Obter(id);
                return Ok(retorno);
            }
            catch (Excecoes.ObjetoNaoEncontradoException)
            {
                return NotFound();
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
                var retorno = repo.Inserir(obj);
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
                obj._id = new ObjectId(id);
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
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
