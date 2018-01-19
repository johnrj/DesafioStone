using DesafioStone.Models;
using DesafioStone.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace DesafioStone.Controllers
{
    public class TipoImobilizadoController : ApiController
    {
        public List<TipoImobilizado> GetTipoImobilizados()
        {
            var repo = new TipoImobilizadoRepository();
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

            var repo = new TipoImobilizadoRepository();

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
    }
}
