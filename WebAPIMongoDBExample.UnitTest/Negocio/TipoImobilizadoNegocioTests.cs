using WebAPIMongoDBExample.Models;
using WebAPIMongoDBExample.Repository;
using MongoDB.Bson;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace WebAPIMongoDBExample.Negocio.Tests
{
    public class TipoImobilizadoNegocioTests
    {
        Mock<ITipoImobilizadoRepository> _tipoImobilizadoRepo;
        Mock<IImobilizadoRepository> _imobilizadoRepo;
        ITipoImobilizadoNegocio _negocio;
        private const string _tipoImobilizadoId = "5a63c2bda872872654b15637";
        private const string _imobilizadoId = "5a63b3f7a872872ce8b6235f";

        public TipoImobilizadoNegocioTests()
        {
            _tipoImobilizadoRepo = new Mock<ITipoImobilizadoRepository>();
            _imobilizadoRepo = new Mock<IImobilizadoRepository>();
            _negocio = new TipoImobilizadoNegocio(_tipoImobilizadoRepo.Object, _imobilizadoRepo.Object);
        }

        [Fact()]
        public void ObterTest_IdInvalido()
        {
            Assert.Throws<FormatException>(() => _negocio.Obter("123"));
        }

        [Fact()]
        public void InserirTest_NomeDuplicado()
        {
            var tipoImobilizado = InstanciarTipoImobilizado();
            _tipoImobilizadoRepo.Setup(s => s.Obter(It.IsAny<string>())).Returns(tipoImobilizado);

            Assert.Throws<Excecoes.ObjetoDuplicadoException>(() => _negocio.Inserir(tipoImobilizado));
        }

        [Fact()]
        public void InserirTest_Ok()
        {
            var tipoImobilizado = InstanciarTipoImobilizado();
            _tipoImobilizadoRepo.Setup(s => s.Obter(It.IsAny<string>())).Returns((TipoImobilizado)null);
            _tipoImobilizadoRepo.Setup(s => s.Inserir(It.IsAny<TipoImobilizado>())).Returns(tipoImobilizado);

            var retorno = _negocio.Inserir(tipoImobilizado);

            Assert.Equal(tipoImobilizado, retorno);
        }

        [Fact()]
        public void AtualizarTest_IdInvalido()
        {
            Assert.Throws<FormatException>(() => _negocio.Atualizar("123", InstanciarTipoImobilizado()));
        }

        [Fact()]
        public void AtualizarTest_NaoEncontrado()
        {
            var tipoMobilizado = InstanciarTipoImobilizado();
            _tipoImobilizadoRepo.Setup(s => s.Obter(It.IsAny<ObjectId>())).Returns((TipoImobilizado)null);

            Assert.Throws<Excecoes.ObjetoNaoEncontradoException>(() => _negocio.Atualizar(_tipoImobilizadoId, tipoMobilizado));
        }

        [Fact()]
        public void AtualizarTest_Encontrado()
        {
            var tipoMobilizado = InstanciarTipoImobilizado();
            _tipoImobilizadoRepo.Setup(s => s.Obter(It.IsAny<ObjectId>())).Returns(tipoMobilizado);
            _tipoImobilizadoRepo.Setup(s => s.Atualizar(It.IsAny<TipoImobilizado>())).Returns(tipoMobilizado);

            var retorno = _negocio.Atualizar(_tipoImobilizadoId, tipoMobilizado);

            Assert.Equal(tipoMobilizado, retorno);
        }

        [Fact()]
        public void ApagarTest_IdInvalido()
        {
            Assert.Throws<FormatException>(() => _negocio.Apagar("123"));
        }

        [Fact()]
        public void ApagarTest_NaoEncontrado()
        {
            _tipoImobilizadoRepo.Setup(s => s.Obter(It.IsAny<ObjectId>())).Returns((TipoImobilizado)null);

            Assert.Throws<Excecoes.ObjetoNaoEncontradoException>(() => _negocio.Apagar(_tipoImobilizadoId));
        }

        [Fact()]
        public void ApagarTest_AcaoProibida()
        {
            var tipoMobilizado = InstanciarTipoImobilizado();
            _tipoImobilizadoRepo.Setup(s => s.Obter(It.IsAny<ObjectId>())).Returns(tipoMobilizado);
            _imobilizadoRepo.Setup(s => s.ObterPeloTipo(It.IsAny<string>())).Returns(InstanciarListaImobilizado());

            Assert.Throws<Excecoes.AcaoProibidaException>(() => _negocio.Apagar(_tipoImobilizadoId));
        }

        [Fact()]
        public void ApagarTest_Encontrado()
        {
            var tipoMobilizado = InstanciarTipoImobilizado();
            _tipoImobilizadoRepo.Setup(s => s.Obter(It.IsAny<ObjectId>())).Returns(tipoMobilizado);
            _imobilizadoRepo.Setup(s => s.ObterPeloTipo(It.IsAny<string>())).Returns(new List<Imobilizado>());
            _tipoImobilizadoRepo.Setup(s => s.Apagar(It.IsAny<ObjectId>()));

            var retorno = _negocio.Apagar(_tipoImobilizadoId);
            Assert.Equal(tipoMobilizado, retorno);
        }

        private static TipoImobilizado InstanciarTipoImobilizado()
        {
            return new TipoImobilizado()
            {
                Nome = "teste",
                _id = ObjectId.Parse(_tipoImobilizadoId)
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
                    _id = new ObjectId(_imobilizadoId)
                }
            };
        }
    }
}