using DesafioStone.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Bson;
using MongoDB.Driver;

namespace DesafioStone.Repository
{
    public class ImobilizadoRepository:BaseRepository
    {
        public List<Imobilizado> ObterTodos()
        {
            var retorno = db.GetCollection<Imobilizado>("Imobilizado").Find(f => true).ToList();
            return retorno;
        }

    }
}