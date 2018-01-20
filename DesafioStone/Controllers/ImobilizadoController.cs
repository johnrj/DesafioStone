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
    public class ImobilizadoController : ApiController
    {
        private IImobilizadoRepository repo;
        public ImobilizadoController()
        {
            repo = new ImobilizadoRepository();
        }

        [ResponseType(typeof(List<Imobilizado>))]
        public IHttpActionResult GetImobilizado()
        {
            try
            {
                var imobilizados = repo.ObterTodos();
                return Ok(imobilizados);
            }
            catch(Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [ResponseType(typeof(Imobilizado))]
        public IHttpActionResult GetImobilizado(string id)
        {
            try
            {
                var retorno = repo.Obter(new ObjectId(id));
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

        [ResponseType(typeof(Imobilizado))]
        public IHttpActionResult PostImobilizado(Imobilizado obj)
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
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [ResponseType(typeof(Imobilizado))]
        public IHttpActionResult PutImobilizado([FromUri] string id, [FromBody] Imobilizado obj)
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

        [ResponseType(typeof(Imobilizado))]
        public IHttpActionResult DeleteImobilizado(string id)
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
