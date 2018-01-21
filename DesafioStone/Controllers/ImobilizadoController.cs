using DesafioStone.Models;
using DesafioStone.Negocio;
using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Description;

namespace DesafioStone.Controllers
{
    public class ImobilizadoController : ApiController
    {
        private IImobilizadoNegocio nego;
        public ImobilizadoController()
        {
            nego = new ImobilizadoNegocio();
        }

        [ResponseType(typeof(List<Imobilizado>))]
        public IHttpActionResult GetImobilizado()
        {
            try
            {
                var imobilizados = nego.ObterTodos();
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
                var retorno = nego.Obter(id);
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
                var retorno = nego.Inserir(obj);
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
                var retorno = nego.Atualizar(id, obj);
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
                var retorno = nego.Apagar(id);
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
