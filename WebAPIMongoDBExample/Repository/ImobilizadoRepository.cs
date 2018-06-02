using WebAPIMongoDBExample.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

namespace WebAPIMongoDBExample.Repository
{
    public class ImobilizadoRepository : BaseRepository, IImobilizadoRepository
    {
        private IMongoCollection<Imobilizado> colecao;
        public ImobilizadoRepository()
        {
            colecao = db.GetCollection<Imobilizado>("Imobilizado");
        }

        public List<Imobilizado> ObterTodos()
        {
            var retorno = colecao.Find(f => true).ToList();
            return retorno;
        }

        public Imobilizado Obter(ObjectId id)
        {
            var retorno = colecao.Find(f => f._id == id).FirstOrDefault();
            return retorno;
        }

        public List<Imobilizado> Obter(bool ativo)
        {
            var retorno = colecao.Find(f => f.Ativo == ativo).ToList();
            return retorno;
        }

        public List<Imobilizado> ObterPeloTipo(string id)
        {
            var retorno = colecao.Find(f => f.TipoImobilizadoId == id).ToList();
            return retorno;
        }

        public List<Imobilizado> Obter(List<ObjectId> ids)
        {
            var filter = Builders<Imobilizado>.Filter.In(i => i._id, ids);
            var retorno = colecao.Find(filter).ToList();
            return retorno;
        }

        public Imobilizado Inserir(Imobilizado obj)
        {
            colecao.InsertOne(obj);
            return obj;
        }

        public Imobilizado Atualizar(Imobilizado obj)
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