using WebAPIMongoDBExample.Models;
using WebAPIMongoDBExample.Negocio;
using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Description;

namespace WebAPIMongoDBExample.Controllers
{
    public class ImobilizadoController : ApiController
    {
        IImobilizadoNegocio _negocio;
        public ImobilizadoController(IImobilizadoNegocio negocio)
        {
            _negocio = negocio;
        }

        [ResponseType(typeof(List<Imobilizado>))]
        public IHttpActionResult GetImobilizado()
        {
            try
            {
                var imobilizados = _negocio.ObterTodos();
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
                var retorno = _negocio.Obter(id);
                return Ok(retorno);
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
                var retorno = _negocio.Inserir(obj);
                return Created("DefaultApi", retorno);
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
        public IHttpActionResult PutImobilizado([FromUri] string id, [FromBody] Imobilizado obj)
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

        [ResponseType(typeof(Imobilizado))]
        public IHttpActionResult DeleteImobilizado(string id)
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
