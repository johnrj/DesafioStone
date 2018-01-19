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
    public class ImobilizadoController : ApiController
    {
        public List<Imobilizado> GetImobilizados()
        {
            var repo = new ImobilizadoRepository();
            var imobilizados = repo.ObterTodos();

            return imobilizados;
        }
    }
}
