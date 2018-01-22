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

namespace DesafioStone.Negocio.Tests
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
        public void InserirTest_NaoEncontrado()
        {
            _tipoImobilizadoRepo.Setup(s => s.Obter(ObjectId.Parse(_tipoImobilizadoId))).Returns((TipoImobilizado)null);

            Assert.Throws<Excecoes.ObjetoNaoEncontradoException>(() => _negocio.Inserir(InstanciarImobilizado()));
        }

        [Fact()]
        public void InserirTest_Encontrado()
        {
            var tipoImobilizado = InstanciarTipoImobilizado();
            var imobilizado = InstanciarImobilizado();
            _tipoImobilizadoRepo.Setup(s => s.Obter(ObjectId.Parse(_tipoImobilizadoId))).Returns(tipoImobilizado);
            _imobilizadoRepo.Setup(s => s.Inserir(imobilizado)).Returns(imobilizado);

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
            _imobilizadoRepo.Setup(s => s.Obter(ObjectId.Parse(_imobilizadoId))).Returns((Imobilizado)null);
            Assert.Throws<Excecoes.ObjetoNaoEncontradoException>(() => _negocio.Atualizar(_imobilizadoId, InstanciarImobilizado()));
        }

        [Fact()]
        public void AtualizarTest_Ok()
        {
            var imobilizado = InstanciarImobilizado();
            _imobilizadoRepo.Setup(s => s.Obter(ObjectId.Parse(_imobilizadoId))).Returns(imobilizado);
            _imobilizadoRepo.Setup(s => s.Atualizar(imobilizado)).Returns(imobilizado);

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
            _imobilizadoRepo.Setup(s => s.Obter(ObjectId.Parse(_imobilizadoId))).Returns((Imobilizado)null);
            Assert.Throws<Excecoes.ObjetoNaoEncontradoException>(() => _negocio.Apagar(_imobilizadoId));
        }

        [Fact()]
        public void ApagarTest_Ok()
        {
            var objId = ObjectId.Parse(_imobilizadoId);

            var imobilizado = InstanciarImobilizado();
            _imobilizadoRepo.Setup(s => s.Obter(objId)).Returns(imobilizado);
            _imobilizadoRepo.Setup(s => s.Apagar(objId));

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