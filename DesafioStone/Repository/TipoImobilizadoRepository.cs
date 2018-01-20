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

        public TipoImobilizado Atualizar(TipoImobilizado obj)
        {
            var objExistente = Obter(obj.Nome);
            if (objExistente == null)
            {
                throw new Excecoes.ObjetoNaoEncontradoException();
            }
            //TODO: Atualizar os Imobilizados desse TipoImobilizado para o novo valor

            colecao.ReplaceOne(u => u.Nome == obj.Nome, obj);
            return obj;
        }

        public TipoImobilizado Apagar(string nome)
        {
            var obj = Obter(nome);
            if (obj == null)
            {
                throw new Excecoes.ObjetoNaoEncontradoException();
            }
            //TODO: Não permitir apagar um TipoImobilizado que esteja sendo usado por algum Imobilizado

            colecao.DeleteOne(d => d.Nome == nome);
            return obj;
        }
    }
}