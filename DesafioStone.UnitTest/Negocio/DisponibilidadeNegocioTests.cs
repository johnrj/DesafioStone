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
        Mock<BaseRepository> baseRepo;
        Mock<IUtilizacaoRepository> utilizacaoRepo;
        Mock<IImobilizadoRepository> imobilizadoRepo;
        private const string imobilizado = "5a63b3f7a872872ce8b6235f";
        private const string tipoImobilizado = "5a63c2bda872872654b15637";

        public DisponibilidadeNegocioTests()
        {
            utilizacaoRepo = new Mock<IUtilizacaoRepository>();
            imobilizadoRepo = new Mock<IImobilizadoRepository>();
            negocio = new DisponibilidadeNegocio(utilizacaoRepo.Object, imobilizadoRepo.Object);
        }

        [Fact()]
        public void ObterTodasDisponibilidadesDoDiaCorrente()
        {
            var imobilizados = new List<Imobilizado>()
            {
                new Imobilizado()
                {
                    Nome = "Teste",
                    Descricao = "Teste teste",
                    TipoImobilizadoId = tipoImobilizado,
                    Ativo = true,
                    _id = new ObjectId(imobilizado)
                }
            };

            var utilizacoes = new List<Utilizacao>()
            {
                new Utilizacao()
                {
                    Andar = 10,
                    Sala = "teste",
                    Responsavel = "teste",
                    ItemUtilizadoId = imobilizado,
                    Devolvido = false,
                    InicioUso = DateTime.Today.AddHours(10),
                    FimUso = DateTime.Today.AddHours(12)
                }
            };

            imobilizadoRepo.Setup(s => s.Obter(It.IsAny<bool>())).Returns(imobilizados);
            utilizacaoRepo.Setup(s => s.Obter(DateTime.Today)).Returns(utilizacoes);

            var retorno = negocio.ObterTodasDisponibilidadesDoDia();

            Assert.Equal(imobilizados.First(), retorno.First().Imobilizado);
            Assert.Equal(3, retorno.First().HorasIndisponiveis.Count);
            Assert.Equal(21, retorno.First().HorasDisponiveis.Count);
            Assert.Equal(24, retorno.First().HorasDisponiveis.Count + retorno.First().HorasIndisponiveis.Count);
            Assert.Equal(DateTime.Today, retorno.First().HorasDisponiveis.First().Date);
            Assert.Equal(DateTime.Today, retorno.First().HorasIndisponiveis.First().Date);
        }

        [Fact()]
        public void ObterTodasDisponibilidadesDoDiaTest2()
        {
            Assert.True(false, "This test needs an implementation");
        }

        [Fact()]
        public void ObterTodasDisponibilidadesDoDiaTest3()
        {
            Assert.True(false, "This test needs an implementation");
        }
    }
}