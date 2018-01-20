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

        public TipoImobilizado Obter(string nome)
        {
            var retorno = colecao.Find(f => f.Nome == nome).FirstOrDefault();
            if(retorno == null)
            {
                throw new Excecoes.ObjetoNaoEncontradoException();
            }

            return retorno;
        }

        public TipoImobilizado Obter(ObjectId id)
        {
            var retorno = colecao.Find(f => f._id == id).FirstOrDefault();
            if (retorno == null)
            {
                throw new Excecoes.ObjetoNaoEncontradoException();
            }

            return retorno;
        }

        public TipoImobilizado Inserir(TipoImobilizado obj)
        {
            //O tratamento de existencia do item passado está no metodo Obter
            var objExistente = Obter(obj.Nome);
            colecao.InsertOne(obj);
            return obj;
        }

        public TipoImobilizado Atualizar(TipoImobilizado obj)
        {
            //O tratamento de existencia do item passado está no metodo Obter
            var objExistente = Obter(obj._id);
            colecao.ReplaceOne(u => u._id == obj._id, obj);
            return obj;
        }

        public TipoImobilizado Apagar(ObjectId id)
        {
            //O tratamento de existencia do item passado está no metodo Obter
            var obj = Obter(id);
            colecao.DeleteOne(d => d._id == id);
            return obj;
        }
    }
}