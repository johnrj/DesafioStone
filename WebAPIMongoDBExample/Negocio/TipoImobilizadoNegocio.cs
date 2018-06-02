using WebAPIMongoDBExample.Models;
using WebAPIMongoDBExample.Repository;
using MongoDB.Bson;
using System.Collections.Generic;
using System.Linq;

namespace WebAPIMongoDBExample.Negocio
{
    public class TipoImobilizadoNegocio : ITipoImobilizadoNegocio
    {
        ITipoImobilizadoRepository _repo;
        IImobilizadoRepository _imobilizadoRepo;

        public TipoImobilizadoNegocio(ITipoImobilizadoRepository repo, IImobilizadoRepository imobilizadoRepo)
        {
            _repo = repo;
            _imobilizadoRepo = imobilizadoRepo;
        }

        public List<TipoImobilizado> ObterTodos()
        {
            var retorno = _repo.ObterTodos();
            return retorno;
        }

        public TipoImobilizado Obter(string id)
        {
            var retorno = _repo.Obter(ObjectId.Parse(id));
            return retorno;
        }

        public TipoImobilizado Inserir(TipoImobilizado obj)
        {
            var objExistente = _repo.Obter(obj.Nome);
            if (objExistente != null)
            {
                throw new Excecoes.ObjetoDuplicadoException();
            }

            var retorno = _repo.Inserir(obj);
            return retorno;
        }

        public TipoImobilizado Atualizar(string id, TipoImobilizado obj)
        {
            var objExistente = _repo.Obter(ObjectId.Parse(id));
            if (objExistente == null)
            {
                throw new Excecoes.ObjetoNaoEncontradoException();
            }

            obj._id = objExistente._id;
            var retorno = _repo.Atualizar(obj);
            return retorno;
        }

        public TipoImobilizado Apagar(string id)
        {
            var obj = _repo.Obter(ObjectId.Parse(id));
            if (obj == null)
            {
                throw new Excecoes.ObjetoNaoEncontradoException();
            }

            if (_imobilizadoRepo.ObterPeloTipo(id).Any())
            {
                throw new Excecoes.AcaoProibidaException();
            }

            _repo.Apagar(obj._id);

            return obj;
        }
    }
}