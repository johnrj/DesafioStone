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
            return retorno;
        }

        private TipoImobilizado Obter(ObjectId id)
        {
            var retorno = colecao.Find(f => f._id == id).FirstOrDefault();
            return retorno;
        }

        public TipoImobilizado Inserir(TipoImobilizado obj)
        {
            var objExistente = Obter(obj.Nome);
            if(objExistente != null)
            {
                throw new Excecoes.ObjetoDuplicadoException();
            }
            colecao.InsertOne(obj);
            return obj;
        }

        public TipoImobilizado Atualizar(string id, TipoImobilizado novoObj)
        {
            var objId = ObjectId.Parse(id);
            novoObj._id = objId;

            var objExistente = Obter(objId);
            if (objExistente == null)
            {
                throw new Excecoes.ObjetoNaoEncontradoException();
            }

            colecao.ReplaceOne(u => u._id == objExistente._id, novoObj);
            return novoObj;
        }

        public TipoImobilizado Apagar(string id)
        {
            var obj = Obter(ObjectId.Parse(id));
            if (obj == null)
            {
                throw new Excecoes.ObjetoNaoEncontradoException();
            }

            IImobilizadoRepository imobilizadoRepo = new ImobilizadoRepository();
            var imobilizados = imobilizadoRepo.ObterTodos().Where(w=>w.TipoImobilizadoId == obj._id.ToString()).ToList();
            if (imobilizados.Any())
            {
                throw new Excecoes.AcaoProibidaException();
            }
            //TODO: Não permitir apagar um TipoImobilizado que esteja sendo usado por algum Imobilizado

            colecao.DeleteOne(d => d._id == obj._id);
            return obj;
        }
    }
}