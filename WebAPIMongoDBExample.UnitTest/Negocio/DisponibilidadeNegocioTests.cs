using WebAPIMongoDBExample.Models;
using WebAPIMongoDBExample.Repository;
using MongoDB.Bson;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace WebAPIMongoDBExample.Negocio.Tests
{
    public class DisponibilidadeNegocioTests
    {
        IDisponibilidadeNegocio _negocio;
        Mock<IUtilizacaoRepository> _utilizacaoRepo;
        Mock<IImobilizadoRepository> _imobilizadoRepo;
        private const string _imobilizadoId = "5a63b3f7a872872ce8b6235f";
        private const string _imobilizadoIdInvalido = "5a63b3f7a872872ce8b6235d";
        private const string _tipoImobilizadoId = "5a63c2bda872872654b15637";

        public DisponibilidadeNegocioTests()
        {
            _utilizacaoRepo = new Mock<IUtilizacaoRepository>();
            _imobilizadoRepo = new Mock<IImobilizadoRepository>();
            _negocio = new DisponibilidadeNegocio(_utilizacaoRepo.Object, _imobilizadoRepo.Object);
        }

        [Fact()]
        public void ObterTodasDisponibilidadesDoDiaCorrente()
        {
            var imobilizados = InstanciarListaImobilizado();
            var utilizacoes = InstanciarListaUtilizacoes(DateTime.Today);

            _imobilizadoRepo.Setup(s => s.Obter(It.IsAny<bool>())).Returns(imobilizados);
            _utilizacaoRepo.Setup(s => s.Obter(It.IsAny<DateTime>())).Returns(utilizacoes);

            var retorno = _negocio.ObterTodasDisponibilidadesDoDia();

            AssercoesGenericas(DateTime.Today, imobilizados.First(), retorno);
        }

        [Fact()]
        public void ObterTodasDisponibilidadesDoDiaEspecifico()
        {
            var data = new DateTime(2018, 1, 1);
            var imobilizados = InstanciarListaImobilizado();
            var utilizacoes = InstanciarListaUtilizacoes(data);

            _imobilizadoRepo.Setup(s => s.Obter(It.IsAny<bool>())).Returns(imobilizados);
            _utilizacaoRepo.Setup(s => s.Obter(It.IsAny<DateTime>())).Returns(utilizacoes);

            var retorno = _negocio.ObterTodasDisponibilidadesDoDia(data);

            AssercoesGenericas(data, imobilizados.First(), retorno);
        }

        [Fact()]
        public void ObterTodasDisponibilidadesDoDiaEItemEspecifico()
        {
            var data = new DateTime(2018, 1, 1);
            var imobilizado = InstanciarListaImobilizado(false).First();
            var utilizacoes = InstanciarListaUtilizacoes(data);

            _imobilizadoRepo.Setup(s => s.Obter(It.IsAny<ObjectId>())).Returns((Imobilizado)null);
            _utilizacaoRepo.Setup(s => s.Obter(It.IsAny<string>(), It.IsAny<DateTime>())).Returns(utilizacoes);

            Assert.Throws<Excecoes.ObjetoNaoEncontradoException>(() => _negocio.ObterTodasDisponibilidadesDoDia(_imobilizadoIdInvalido, data));

            _imobilizadoRepo.Setup(s => s.Obter(It.IsAny<ObjectId>())).Returns(imobilizado);
            
            Assert.Throws<Excecoes.AcaoProibidaException>(() => _negocio.ObterTodasDisponibilidadesDoDia(_imobilizadoId, data));

            imobilizado.Ativo = true;
            _imobilizadoRepo.Setup(s => s.Obter(It.IsAny<ObjectId>())).Returns(imobilizado);

            var retorno = _negocio.ObterTodasDisponibilidadesDoDia(_imobilizadoId, data);
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
                    TipoImobilizadoId = _tipoImobilizadoId,
                    Ativo = ativo,
                    _id = new ObjectId(_imobilizadoId)
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
                    ItemUtilizadoId = _imobilizadoId,
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