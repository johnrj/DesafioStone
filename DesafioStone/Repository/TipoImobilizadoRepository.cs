using DesafioStone.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Driver;

namespace DesafioStone.Repository
{
    public class TipoImobilizadoRepository:BaseRepository
    {
        public List<TipoImobilizado> ObterTodos()
        {
            var retorno = db.GetCollection<TipoImobilizado>("TipoImobilizado").Find(f => true).ToList();
            return retorno;
        }

        public TipoImobilizado InserirTipoImobilizado(TipoImobilizado obj)
        {
            var colecao = db.GetCollection<TipoImobilizado>("TipoImobilizado");

            if (Obter(obj.Nome) == null)
            {
                colecao.InsertOne(obj);
                return obj;
            }
            else
            {
                throw new Excecoes.ObjetoDuplicadoException();
            }
        }

        public TipoImobilizado Obter(string nome)
        {
            var retorno = db.GetCollection<TipoImobilizado>("TipoImobilizado").Find(f => f.Nome == nome).FirstOrDefault();
            return retorno;
        }
    }
}