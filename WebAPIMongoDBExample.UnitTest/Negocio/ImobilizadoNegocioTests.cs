using WebAPIMongoDBExample.Models;
using WebAPIMongoDBExample.Repository;
using MongoDB.Bson;
using Moq;
using System;
using Xunit;

namespace WebAPIMongoDBExample.Negocio.Tests
{
    public class ImobilizadoNegocioTests
    {
        IImobilizadoNegocio _negocio;
        Mock<IImobilizadoRepository> _imobilizadoRepo;
        Mock<ITipoImobilizadoRepository> _tipoImobilizadoRepo;
        private const string _imobilizadoId = "5a63b3f7a872872ce8b6235f";
        private const string _tipoImobilizadoId = "5a63c2bda872872654b15637";

        public ImobilizadoNegocioTests()
        {
            _imobilizadoRepo = new Mock<IImobilizadoRepository>();
            _tipoImobilizadoRepo = new Mock<ITipoImobilizadoRepository>();
            _negocio = new ImobilizadoNegocio(_imobilizadoRepo.Object, _tipoImobilizadoRepo.Object);
        }

        [Fact()]
        public void ObterTest_IdInvalido()
        {
            Assert.Throws<FormatException>(() => _negocio.Obter("123"));
        }

        [Fact()]
        public void InserirTest_NaoEncontrado()
        {
            _tipoImobilizadoRepo.Setup(s => s.Obter(It.IsAny<ObjectId>())).Returns((TipoImobilizado)null);

            Assert.Throws<Excecoes.ObjetoNaoEncontradoException>(() => _negocio.Inserir(InstanciarImobilizado()));
        }

        [Fact()]
        public void InserirTest_Encontrado()
        {
            var tipoImobilizado = InstanciarTipoImobilizado();
            var imobilizado = InstanciarImobilizado();
            _tipoImobilizadoRepo.Setup(s => s.Obter(It.IsAny<ObjectId>())).Returns(tipoImobilizado);
            _imobilizadoRepo.Setup(s => s.Inserir(It.IsAny<Imobilizado>())).Returns(imobilizado);

            var retorno = _negocio.Inserir(imobilizado);
            Assert.Equal(imobilizado, retorno);
        }

        [Fact()]
        public void AtualizarTest_IdInvalido()
        {
            Assert.Throws<FormatException>(() => _negocio.Atualizar("123", InstanciarImobilizado()));
        }

        [Fact()]
        public void AtualizarTest_NaoEncontrado()
        {
            _imobilizadoRepo.Setup(s => s.Obter(It.IsAny<ObjectId>())).Returns((Imobilizado)null);
            Assert.Throws<Excecoes.ObjetoNaoEncontradoException>(() => _negocio.Atualizar(_imobilizadoId, InstanciarImobilizado()));
        }

        [Fact()]
        public void AtualizarTest_Ok()
        {
            var imobilizado = InstanciarImobilizado();
            _imobilizadoRepo.Setup(s => s.Obter(It.IsAny<ObjectId>())).Returns(imobilizado);
            _imobilizadoRepo.Setup(s => s.Atualizar(It.IsAny<Imobilizado>())).Returns(imobilizado);

            var retorno = _negocio.Atualizar(_imobilizadoId, imobilizado);

            Assert.Equal(imobilizado, retorno);
        }

        [Fact()]
        public void ApagarTest_IdInvalido()
        {
            Assert.Throws<FormatException>(() => _negocio.Apagar("123"));
        }

        [Fact()]
        public void ApagarTest_NaoEncontrado()
        {
            _imobilizadoRepo.Setup(s => s.Obter(It.IsAny<ObjectId>())).Returns((Imobilizado)null);
            Assert.Throws<Excecoes.ObjetoNaoEncontradoException>(() => _negocio.Apagar(_imobilizadoId));
        }

        [Fact()]
        public void ApagarTest_Ok()
        {
            var imobilizado = InstanciarImobilizado();
            _imobilizadoRepo.Setup(s => s.Obter(It.IsAny<ObjectId>())).Returns(imobilizado);
            _imobilizadoRepo.Setup(s => s.Apagar(It.IsAny<ObjectId>()));

            var retorno = _negocio.Apagar(_imobilizadoId);

            Assert.Equal(imobilizado, retorno);
        }

        private static Imobilizado InstanciarImobilizado()
        {
            return new Imobilizado()
            {
                Nome = "Teste",
                Descricao = "Teste teste",
                TipoImobilizadoId = _tipoImobilizadoId,
                Ativo = true,
                _id = ObjectId.Parse(_imobilizadoId)
            };
        }

        private static TipoImobilizado InstanciarTipoImobilizado()
        {
            return new TipoImobilizado()
            {
                Nome = "teste",
                _id = ObjectId.Parse(_tipoImobilizadoId)
            };
        }
    }
}