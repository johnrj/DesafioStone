using Xunit;
using DesafioStone.Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using DesafioStone.Repository;
using DesafioStone.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace DesafioStone.Negocio.Tests
{
    public class DisponibilidadeNegocioTests
    {
        IDisponibilidadeNegocio negocio;
        Mock<IUtilizacaoRepository> utilizacaoRepo;
        Mock<IImobilizadoRepository> imobilizadoRepo;
        private const string imobilizadoId = "5a63b3f7a872872ce8b6235f";
        private const string imobilizadoIdInvalido = "5a63b3f7a872872ce8b6235d";
        private const string tipoImobilizadoId = "5a63c2bda872872654b15637";

        public DisponibilidadeNegocioTests()
        {
            utilizacaoRepo = new Mock<IUtilizacaoRepository>();
            imobilizadoRepo = new Mock<IImobilizadoRepository>();
            negocio = new DisponibilidadeNegocio(utilizacaoRepo.Object, imobilizadoRepo.Object);
        }

        [Fact()]
        public void ObterTodasDisponibilidadesDoDiaCorrente()
        {
            var imobilizados = InstanciarListaImobilizado();
            var utilizacoes = InstanciarListaUtilizacoes(DateTime.Today);

            imobilizadoRepo.Setup(s => s.Obter(It.IsAny<bool>())).Returns(imobilizados);
            utilizacaoRepo.Setup(s => s.Obter(DateTime.Today)).Returns(utilizacoes);

            var retorno = negocio.ObterTodasDisponibilidadesDoDia();

            AssercoesGenericas(DateTime.Today, imobilizados.First(), retorno);
        }

        [Fact()]
        public void ObterTodasDisponibilidadesDoDiaEspecifico()
        {
            var data = new DateTime(2018, 1, 1);
            var imobilizados = InstanciarListaImobilizado();
            var utilizacoes = InstanciarListaUtilizacoes(data);

            imobilizadoRepo.Setup(s => s.Obter(true)).Returns(imobilizados);
            utilizacaoRepo.Setup(s => s.Obter(data)).Returns(utilizacoes);

            var retorno = negocio.ObterTodasDisponibilidadesDoDia(data);

            AssercoesGenericas(data, imobilizados.First(), retorno);
        }

        [Fact()]
        public void ObterTodasDisponibilidadesDoDiaEItemEspecifico()
        {
            var data = new DateTime(2018, 1, 1);

            var imobilizado = InstanciarListaImobilizado(false).First();

            var utilizacoes = InstanciarListaUtilizacoes(data);

            imobilizadoRepo.Setup(s => s.Obter(ObjectId.Parse(imobilizadoId))).Returns(imobilizado);
            utilizacaoRepo.Setup(s => s.Obter(imobilizadoId, data)).Returns(utilizacoes);

            Assert.Throws<Excecoes.ObjetoNaoEncontradoException>(() => negocio.ObterTodasDisponibilidadesDoDia(imobilizadoIdInvalido, data));
            Assert.Throws<Excecoes.AcaoProibidaException>(() => negocio.ObterTodasDisponibilidadesDoDia(imobilizadoId, data));

            imobilizado.Ativo = true;
            imobilizadoRepo.Setup(s => s.Obter(ObjectId.Parse(imobilizadoId))).Returns(imobilizado);

            var retorno = negocio.ObterTodasDisponibilidadesDoDia(imobilizadoId, data);
            AssercoesGenericas(data, imobilizado, retorno);
        }

        private static List<Imobilizado> InstanciarListaImobilizado(bool ativo = true)
        {
            return new List<Imobilizado>()
            {
                new Imobilizado()
                {
                    Nome = "Teste",
                    Descricao = "Teste teste",
                    TipoImobilizadoId = tipoImobilizadoId,
                    Ativo = ativo,
                    _id = new ObjectId(imobilizadoId)
                }
            };
        }
        private static List<Utilizacao> InstanciarListaUtilizacoes(DateTime data)
        {
            return new List<Utilizacao>()
            {
                new Utilizacao()
                {
                    Andar = 10,
                    Sala = "teste",
                    Responsavel = "teste",
                    ItemUtilizadoId = imobilizadoId,
                    Devolvido = false,
                    InicioUso = data.AddHours(10),
                    FimUso = data.AddHours(12)
                }
            };
        }

        private static void AssercoesGenericas(DateTime data, Imobilizado imobilizado, List<Disponibilidade> retorno)
        {
            Assert.Equal(imobilizado, retorno.First().Imobilizado);
            Assert.Equal(3, retorno.First().HorasIndisponiveis.Count);
            Assert.Equal(21, retorno.First().HorasDisponiveis.Count);
            Assert.Equal(24, retorno.First().HorasDisponiveis.Count + retorno.First().HorasIndisponiveis.Count);
            Assert.Equal(data, retorno.First().HorasDisponiveis.First().Date);
            Assert.Equal(data, retorno.First().HorasIndisponiveis.First().Date);
        }
    }
}