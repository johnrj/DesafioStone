using Xunit;
using WebAPIMongoDBExample.Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using WebAPIMongoDBExample.Repository;
using WebAPIMongoDBExample.Models;
using MongoDB.Bson;

namespace WebAPIMongoDBExample.Negocio.Tests
{
    public class UtilizacaoNegocioTests
    {
        Mock<IUtilizacaoRepository> _utilizacaoRepo;
        Mock<IImobilizadoRepository> _imobilizadoRepo;
        IUtilizacaoNegocio _negocio;
        private const string _imobilizadoId = "5a63b3f7a872872ce8b6235f";
        private const string _tipoImobilizadoId = "5a63c2bda872872654b15637";

        public UtilizacaoNegocioTests()
        {
            _utilizacaoRepo = new Mock<IUtilizacaoRepository>();
            _imobilizadoRepo = new Mock<IImobilizadoRepository>();
            _negocio = new UtilizacaoNegocio(_utilizacaoRepo.Object, _imobilizadoRepo.Object);
        }

        [Fact()]
        public void ObterTodosTest_IdInvalido()
        {
            _utilizacaoRepo.Setup(s => s.ObterTodos()).Returns(InstanciarListaUtilizacaoIdInvalido());
            Assert.Throws<FormatException>(() => _negocio.ObterTodos());
        }

        [Fact()]
        public void InserirTest_IdInvalido()
        {
            Assert.Throws<FormatException>(() => _negocio.Inserir(InstanciarListaUtilizacaoIdInvalido().First()));
        }

        [Fact()]
        public void InserirTest_NaoEncontrado()
        {
            _imobilizadoRepo.Setup(s => s.Obter(It.IsAny<ObjectId>())).Returns((Imobilizado)null);
            Assert.Throws<Excecoes.ObjetoNaoEncontradoException>(() => _negocio.Inserir(InstanciarListaUtilizacaoIdValido().First()));
        }

        [Fact()]
        public void InserirTest_AcaoProibida()
        {
            _imobilizadoRepo.Setup(s => s.Obter(It.IsAny<ObjectId>())).Returns(InstanciarListaImobilizado().First());
            _utilizacaoRepo.Setup(s => s.ItemEmuso(It.IsAny<Utilizacao>())).Returns(true);
            Assert.Throws<Excecoes.AcaoProibidaException>(() => _negocio.Inserir(InstanciarListaUtilizacaoIdValido().First()));
        }

        [Fact()]
        public void InserirTest_Ok()
        {
            var utilizacao = InstanciarListaUtilizacaoIdValido().First();
            _imobilizadoRepo.Setup(s => s.Obter(It.IsAny<ObjectId>())).Returns(InstanciarListaImobilizado().First());
            _utilizacaoRepo.Setup(s => s.ItemEmuso(It.IsAny<Utilizacao>())).Returns(false);
            _utilizacaoRepo.Setup(s => s.Inserir(It.IsAny<Utilizacao>())).Returns(utilizacao);

            var retorno = _negocio.Inserir(utilizacao);
            Assert.Equal(utilizacao, retorno);
        }

        [Fact()]
        public void AtualizarTest_IdInvalido()
        {
            Assert.Throws<FormatException>(() => _negocio.Atualizar("123", new Utilizacao()));
        }

        [Fact()]
        public void AtualizarTest_NaoEncontrado()
        {
            _utilizacaoRepo.Setup(s => s.Obter(It.IsAny<ObjectId>())).Returns((Utilizacao)null);
            Assert.Throws<Excecoes.ObjetoNaoEncontradoException>(() => _negocio.Atualizar(_imobilizadoId, new Utilizacao()));
        }

        [Fact()]
        public void AtualizarTest_AcaoProibida()
        {
            var utilizacao = InstanciarListaUtilizacaoIdValido().First();
            _utilizacaoRepo.Setup(s => s.Obter(It.IsAny<ObjectId>())).Returns(utilizacao);
            _utilizacaoRepo.Setup(s => s.ItemEmuso(It.IsAny<Utilizacao>())).Returns(true);

            Assert.Throws<Excecoes.AcaoProibidaException>(() => _negocio.Atualizar(_imobilizadoId, new Utilizacao()));
        }

        [Fact()]
        public void AtualizarTest_Ok()
        {
            var utilizacao = InstanciarListaUtilizacaoIdValido().First();
            _utilizacaoRepo.Setup(s => s.Obter(It.IsAny<ObjectId>())).Returns(utilizacao);
            _utilizacaoRepo.Setup(s => s.ItemEmuso(It.IsAny<Utilizacao>())).Returns(false);
            _utilizacaoRepo.Setup(s => s.Atualizar(It.IsAny<Utilizacao>())).Returns(utilizacao);

            var retorno = _negocio.Atualizar(_imobilizadoId, utilizacao);
            Assert.Equal(utilizacao, retorno);
        }

        [Fact()]
        public void ApagarTest_IdInvalido()
        {
            Assert.Throws<FormatException>(() => _negocio.Apagar("123"));
        }

        [Fact()]
        public void ApagarTest_NaoEncontrado()
        {
            _utilizacaoRepo.Setup(s => s.Obter(It.IsAny<ObjectId>())).Returns((Utilizacao)null);
            Assert.Throws<Excecoes.ObjetoNaoEncontradoException>(() => _negocio.Apagar(_imobilizadoId));
        }

        [Fact()]
        public void ApagarTest_Ok()
        {
            var utilizacao = InstanciarListaUtilizacaoIdValido().First();
            _utilizacaoRepo.Setup(s => s.Obter(It.IsAny<ObjectId>())).Returns(utilizacao);
            _utilizacaoRepo.Setup(s => s.Apagar(It.IsAny<ObjectId>()));

            var retorno = _negocio.Apagar(_imobilizadoId);
            Assert.Equal(utilizacao, retorno);
        }

        private static List<Utilizacao> InstanciarListaUtilizacaoIdInvalido()
        {
            return new List<Utilizacao>()
            {
                new Utilizacao()
                {
                    ItemUtilizadoId = "123"
                }
            };
        }

        private static List<Utilizacao> InstanciarListaUtilizacaoIdValido()
        {
            return new List<Utilizacao>()
            {
                new Utilizacao()
                {
                    ItemUtilizadoId = _imobilizadoId,
                    ItemUtilizado = InstanciarListaImobilizado().First()
                }
            };
        }

        private static List<Imobilizado> InstanciarListaImobilizado()
        {
            return new List<Imobilizado>()
            {
                new Imobilizado()
                {
                    Nome = "Teste",
                    Descricao = "Teste teste",
                    TipoImobilizadoId = _tipoImobilizadoId,
                    Ativo = true,
                    _id = ObjectId.Parse(_imobilizadoId)
                }
            };
        }
    }
}