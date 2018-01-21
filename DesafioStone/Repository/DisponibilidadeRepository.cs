using DesafioStone.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DesafioStone.Repository
{
    public class DisponibilidadeRepository : BaseRepository, IDisponibilidadeRepository
    {
        private IMongoCollection<Imobilizado> colecaoImobilizado;
        private IMongoCollection<Utilizacao> colecaoUtilizacao;

        public DisponibilidadeRepository()
        {
            colecaoImobilizado = db.GetCollection<Imobilizado>("Imobilizado");
            colecaoUtilizacao = db.GetCollection<Utilizacao>("Utilizacao");
        }

        public List<Disponibilidade> ObterDisponibilidade(string id = null, DateTime? data = null)
        {
            if (!data.HasValue)
            {
                data = DateTime.Today;
            }

            var retorno = new List<Disponibilidade>();
            var utilizacoesDia = new List<Utilizacao>();

            if (id == null)
            {
                retorno.AddRange(colecaoImobilizado.Find(f => f.Ativo).ToList().Select(s => new Disponibilidade
                {
                    Imobilizado = s,
                    HorasDisponiveis = new List<DateTime>(),
                    HorasIndisponiveis = new List<DateTime>()
                }).ToList());

                utilizacoesDia.AddRange(colecaoUtilizacao.Find(f => data >= f.InicioUso || data <= f.FimUso).ToList());
            }
            else
            {
                var objId = ObjectId.Parse(id);
                retorno.AddRange(colecaoImobilizado.Find(f => f.Ativo && f._id == objId).ToList().Select(s => new Disponibilidade
                {
                    Imobilizado = s,
                    HorasDisponiveis = new List<DateTime>(),
                    HorasIndisponiveis = new List<DateTime>()
                }).ToList());

                utilizacoesDia.AddRange(colecaoUtilizacao.Find(f => (data >= f.InicioUso || data <= f.FimUso) && f.ItemUtilizadoId == id).ToList());
            }

            var horaInicial = new DateTime(data.Value.Year, data.Value.Month, data.Value.Day);
            var horaFinal = horaInicial.AddDays(1).AddSeconds(-1);

            foreach (var r in retorno)
            {
                for (var d = horaInicial; d < horaFinal; d = d.AddHours(1))
                {
                    var utilizado = utilizacoesDia.Where(w => w.ItemUtilizadoId == r.Imobilizado._id.ToString() && d >= w.InicioUso && d <= w.FimUso);
                    if (utilizado.Any())
                    {
                        r.HorasIndisponiveis.Add(d);
                    }
                    else
                    {
                        r.HorasDisponiveis.Add(d);
                    }
                }
            }

            return retorno;
        }

    }
}