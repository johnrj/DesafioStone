using WebAPIMongoDBExample.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;

namespace WebAPIMongoDBExample.Repository
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
            return retorno;
        }

        public Utilizacao Obter(ObjectId id)
        {
            var retorno = colecao.Find(f => f._id == id).FirstOrDefault();
            return retorno;
        }

        public List<Utilizacao> Obter(DateTime data)
        {
            var horaInicial = new DateTime(data.Year, data.Month, data.Day);
            var horaFinal = horaInicial.AddDays(1).AddSeconds(-1);
            var retorno = new List<Utilizacao>();

            for (var dt = horaInicial; dt < horaFinal; dt = dt.AddHours(1))
            {
                retorno.AddRange(colecao.Find(f => dt >= f.InicioUso && dt <= f.FimUso).ToList());
            }
            return retorno;
        }

        public Utilizacao Inserir(Utilizacao obj)
        {
            colecao.InsertOne(obj);
            return obj;
        }

        public bool ItemEmuso(Utilizacao obj)
        {
            var itensEmUsoNoPeriodo = colecao.Find(f => f.ItemUtilizadoId == obj.ItemUtilizadoId && obj.InicioUso >= f.InicioUso && obj.InicioUso <= f.FimUso).ToList();
            return itensEmUsoNoPeriodo.Any();
        }

        public Utilizacao Atualizar(Utilizacao obj)
        {
            colecao.ReplaceOne(u => u._id == obj._id, obj);
            return obj;
        }

        public void Apagar(ObjectId id)
        {
            colecao.DeleteOne(d => d._id == id);
        }

        public List<Utilizacao> Obter(string id, DateTime data)
        {
            var horaInicial = new DateTime(data.Year, data.Month, data.Day);
            var horaFinal = horaInicial.AddDays(1).AddSeconds(-1);
            var retorno = new List<Utilizacao>();

            for (var dt = horaInicial; dt < horaFinal; dt = dt.AddHours(1))
            {
                retorno.AddRange(colecao.Find(f => f.ItemUtilizadoId == id && dt >= f.InicioUso && dt <= f.FimUso).ToList());
            }
            return retorno;
        }
    }
}