using DesafioStone.Models;
using DesafioStone.Negocio;
using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;

namespace DesafioStone.Controllers
{
    public class DisponibilidadeController : ApiController
    {
        IDisponibilidadeNegocio _negocio;

        public DisponibilidadeController(IDisponibilidadeNegocio negocio)
        {
            _negocio = negocio;
        }

        [ResponseType(typeof(List<Disponibilidade>))]
        public IHttpActionResult GetDisponibilidade()
        {
            try
            {
                var retorno = _negocio.ObterTodasDisponibilidadesDoDia();
                return Ok(retorno);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [ResponseType(typeof(List<Disponibilidade>))]
        public IHttpActionResult GetDisponibilidade(DateTime d)
        {
            try
            {
                var retorno = _negocio.ObterTodasDisponibilidadesDoDia(d);
                return Ok(retorno);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [ResponseType(typeof(List<Disponibilidade>))]
        public IHttpActionResult GetDisponibilidade(string id, DateTime d)
        {
            try
            {
                var retorno = _negocio.ObterTodasDisponibilidadesDoDia(id, d);
                return Ok(retorno);
            }
            catch (Excecoes.AcaoProibidaException)
            {
                return StatusCode(HttpStatusCode.Forbidden);
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
