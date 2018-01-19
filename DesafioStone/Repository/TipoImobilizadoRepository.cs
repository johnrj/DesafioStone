using DesafioStone.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Driver;
using MongoDB.Bson;

namespace DesafioStone.Repository
{
    public class TipoImobilizadoRepository : BaseRepository, ITipoImobilizadoRepository
    {
        private IMongoCollection<TipoImobilizado> colecao;

        public TipoImobilizadoRepository()
        {
            colecao = db.GetCollection<TipoImobilizado>("TipoImobilizado");
        }

        public List<TipoImobilizado> ObterTodos()
        {
            var retorno = colecao.Find(f => true).ToList();
            return retorno;
        }

        public TipoImobilizado InserirTipoImobilizado(TipoImobilizado obj)
        {
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
            var retorno = colecao.Find(f => f.Nome == nome).FirstOrDefault();
            return retorno;
        }

        public TipoImobilizado Obter(ObjectId id)
        {
            var retorno = colecao.Find(f => f._id == id).FirstOrDefault();
            return retorno;
        }

        public void Atualizar(TipoImobilizado obj)
        {
            if (Obter(obj._id) != null)
            {
                colecao.ReplaceOne(u => u._id == obj._id, obj);
            }
            else
            {
                throw new Excecoes.ObjetoNaoEncontradoException();
            }

        }

        public TipoImobilizado Apagar(ObjectId id)
        {
            var obj = Obter(id);
            if(obj != null)
            {
                colecao.DeleteOne(d => d._id == id);
                return obj;
            }
            else
            {
                throw new Excecoes.ObjetoNaoEncontradoException();
            }
        }
    }
}