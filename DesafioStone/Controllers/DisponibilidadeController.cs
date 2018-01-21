using DesafioStone.Models;
using DesafioStone.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DesafioStone.Controllers
{
    public class DisponibilidadeController : ApiController
    {
        IDisponibilidadeRepository repo;

        public DisponibilidadeController()
        {
            repo = new DisponibilidadeRepository();
        }

        public List<Disponibilidade> GetDisponibilidade()
        {
            var retorno = repo.ObterDisponibilidade();
            return retorno;
        }

        public List<Disponibilidade> GetDisponibilidade(DateTime d)
        {
            var retorno = repo.ObterDisponibilidade(data: d);
            return retorno;
        }

        public List<Disponibilidade> GetDisponibilidade(string id, DateTime d)
        {
            var retorno = repo.ObterDisponibilidade(id, d);
            return retorno;
        }
    }
}
