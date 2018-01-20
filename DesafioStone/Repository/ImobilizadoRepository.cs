using DesafioStone.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Bson;
using MongoDB.Driver;

namespace DesafioStone.Repository
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
            if(retorno == null)
            {
                throw new Excecoes.ObjetoNaoEncontradoException();
            }

            return retorno;
        }

        public Imobilizado Inserir(Imobilizado obj)
        {
            var tipoImobilizado = new TipoImobilizadoRepository().Obter(ObjectId.Parse(obj.TipoImobilizadoId));
            obj.TipoImobilizado = tipoImobilizado;
            colecao.InsertOne(obj);
            return obj;
        }

        public Imobilizado Atualizar(Imobilizado obj)
        {
            //O tratamento de existencia do item passado está no metodo Obter
            var objExistente = Obter(obj._id);
            colecao.ReplaceOne(u => u._id == obj._id, obj);
            return obj;
        }

        public Imobilizado Apagar(ObjectId id)
        {
            //O tratamento de existencia do item passado está no metodo Obter
            var obj = Obter(id);
            colecao.DeleteOne(d => d._id == id);
            return obj;
        }
    }
}