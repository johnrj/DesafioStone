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
            return retorno;
        }

        public List<Imobilizado> Obter(bool ativo)
        {
            var retorno = colecao.Find(f => f.Ativo == ativo).ToList();
            return retorno;
        }

        public Imobilizado Inserir(Imobilizado obj)
        {
            ITipoImobilizadoRepository tipoImobilizadoRepo = new TipoImobilizadoRepository();
            var tipoImobilizado = tipoImobilizadoRepo.Obter(obj.TipoImobilizadoId);
            if(tipoImobilizado == null)
            {
                throw new Excecoes.ObjetoNaoEncontradoException(string.Format("O TipoImobilizado {0} não existe.", obj.TipoImobilizadoId));
            }
            colecao.InsertOne(obj);
            return obj;
        }

        public Imobilizado Atualizar(Imobilizado obj)
        {
            var objExistente = Obter(obj._id);
            if(objExistente == null)
            {
                throw new Excecoes.ObjetoNaoEncontradoException();
            }

            colecao.ReplaceOne(u => u._id == obj._id, obj);
            return obj;
        }

        public Imobilizado Apagar(ObjectId id)
        {
            var obj = Obter(id);
            if (obj == null)
            {
                throw new Excecoes.ObjetoNaoEncontradoException();
            }

            colecao.DeleteOne(d => d._id == id);
            return obj;
        }
    }
}