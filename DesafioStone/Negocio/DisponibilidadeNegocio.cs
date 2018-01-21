using DesafioStone.Models;
using DesafioStone.Repository;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DesafioStone.Negocio
{
    public class DisponibilidadeNegocio : IDisponibilidadeNegocio
    {
        IUtilizacaoRepository utilizacaoRepo;
        IImobilizadoRepository imobilizadoRepo;

        public DisponibilidadeNegocio()
        {
            utilizacaoRepo = new UtilizacaoRepository();
            imobilizadoRepo = new ImobilizadoRepository();
        }

        public List<Disponibilidade> ObterTodasDisponibilidadesDoDia()
        {
            var data = DateTime.Today;

            return ObterTodasDisponibilidadesDoDia(data);
        }

        public List<Disponibilidade> ObterTodasDisponibilidadesDoDia(DateTime data)
        {
            var imobilizadosAtivos = imobilizadoRepo.Obter(true);

            var retorno = imobilizadosAtivos.Select(s => new Disponibilidade
            {
                Imobilizado = s,
                HorasDisponiveis = new List<DateTime>(),
                HorasIndisponiveis = new List<DateTime>()
            }).ToList();

            var utilizacoes = utilizacaoRepo.Obter(data);

            ProcessarUtilizacaoHoraria(retorno, utilizacoes, data);

            return retorno;
        }

        public List<Disponibilidade> ObterTodasDisponibilidadesDoDia(string id, DateTime data)
        {
            var imobilizado = imobilizadoRepo.Obter(ObjectId.Parse(id));

            if(imobilizado == null)
            {
                throw new Excecoes.ObjetoNaoEncontradoException();
            }

            if (!imobilizado.Ativo)
            {
                throw new Excecoes.AcaoProibidaException();
            }

            var retorno = new List<Disponibilidade>()
            {
                new Disponibilidade()
                {
                    Imobilizado = imobilizado,
                    HorasDisponiveis = new List<DateTime>(),
                    HorasIndisponiveis = new List<DateTime>()
                }
            };

            var utilizacoes = utilizacaoRepo.Obter(id, data);

            ProcessarUtilizacaoHoraria(retorno, utilizacoes, data);

            return retorno;
        }

        private static void ProcessarUtilizacaoHoraria(List<Disponibilidade> disponibilidade, List<Utilizacao> utilizacao, DateTime data)
        {
            var horaInicial = new DateTime(data.Year, data.Month, data.Day);
            var horaFinal = horaInicial.AddDays(1).AddSeconds(-1);

            foreach(var disp in disponibilidade)
            {
                for(var dt = horaInicial; dt < horaFinal; dt = dt.AddHours(1))
                {
                    var utilizado = utilizacao.Where(w => w.ItemUtilizadoId == disp.Imobilizado._id.ToString() && dt >= w.InicioUso && dt <= w.FimUso);
                    if (utilizado.Any())
                    {
                        disp.HorasIndisponiveis.Add(dt);
                    }
                    else
                    {
                        disp.HorasDisponiveis.Add(dt);
                    }
                }
            }
        }
    }
}