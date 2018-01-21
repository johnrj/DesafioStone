using DesafioStone.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DesafioStone.Repository
{
    public class UtilizacaoRepository : BaseRepository, IUtilizacaoRepository
    {
        private IMongoCollection<Utilizacao> colecao;
        private IMongoCollection<Imobilizado> colecaoImobilizado;
        public UtilizacaoRepository()
        {
            colecao = db.GetCollection<Utilizacao>("Utilizacao");
            colecaoImobilizado = db.GetCollection<Imobilizado>("Imobilizado");
        }

        public List<Utilizacao> ObterTodos()
        {
            var retorno = colecao.Find(f => true).ToList();
            var idsItensUtilizados = retorno.Select(s => ObjectId.Parse(s.ItemUtilizadoId)).Distinct().ToList();

            var filter = Builders<Imobilizado>.Filter.In(i=>i._id, idsItensUtilizados);
            var itensUtilizados = colecaoImobilizado.Find(filter).ToList().ToDictionary(k => k._id, i => i);

            retorno.ForEach(f => f.ItemUtilizado = itensUtilizados[ObjectId.Parse(f.ItemUtilizadoId)]);

            return retorno;
        }

        public Utilizacao Obter(ObjectId id)
        {
            var retorno = colecao.Find(f => f._id == id).FirstOrDefault();
            return retorno;
        }

        public Utilizacao Inserir(Utilizacao obj)
        {
            IImobilizadoRepository imobilizadoRepo = new ImobilizadoRepository();
            var imobilizado = imobilizadoRepo.Obter(ObjectId.Parse(obj.ItemUtilizadoId));
            if(imobilizado == null)
            {
                throw new Excecoes.ObjetoNaoEncontradoException(string.Format("O Imobilizado {0} não existe.", obj.ItemUtilizadoId));
            }
            if (ItemEmuso(obj))
            {
                throw new Excecoes.AcaoProibidaException(string.Format("O Imobilizado {0} está em uso no período solicitado.", obj.ItemUtilizadoId));
            }

            colecao.InsertOne(obj);
            return obj;
        }

        private bool ItemEmuso(Utilizacao obj)
        {
            var itensEmUsoNoPeriodo = colecao.Find(f => f.ItemUtilizadoId == obj.ItemUtilizadoId && obj.InicioUso >= f.InicioUso && obj.InicioUso <= f.FimUso).ToList();
            return itensEmUsoNoPeriodo.Any();
        }

        public Utilizacao Atualizar(Utilizacao obj)
        {
            var objExistente = Obter(obj._id);
            if(objExistente == null)
            {
                throw new Excecoes.ObjetoNaoEncontradoException();
            }

            colecao.ReplaceOne(u => u._id == obj._id, obj);
            return obj;
        }

        public Utilizacao Apagar(ObjectId id)
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