using WebAPIMongoDBExample.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

namespace WebAPIMongoDBExample.Repository
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

        public TipoImobilizado Obter(ObjectId id)
        {
            var retorno = colecao.Find(f => f._id == id).FirstOrDefault();
            return retorno;
        }

        public TipoImobilizado Obter(string nome)
        {
            var retorno = colecao.Find(f => f.Nome == nome).FirstOrDefault();
            return retorno;
        }

        public TipoImobilizado Inserir(TipoImobilizado obj)
        {
            colecao.InsertOne(obj);
            return obj;
        }

        public TipoImobilizado Atualizar(TipoImobilizado obj)
        {
            colecao.ReplaceOne(u => u._id == obj._id, obj);
            return obj;
        }

        public void Apagar(ObjectId id)
        {
            colecao.DeleteOne(d => d._id == id);
        }
    }
}