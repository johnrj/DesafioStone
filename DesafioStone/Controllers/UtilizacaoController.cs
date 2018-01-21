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
    }
}
