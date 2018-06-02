using WebAPIMongoDBExample.Models;
using WebAPIMongoDBExample.Repository;
using MongoDB.Bson;
using System.Collections.Generic;

namespace WebAPIMongoDBExample.Negocio
{
    public class ImobilizadoNegocio : IImobilizadoNegocio
    {
        IImobilizadoRepository _repo;
        ITipoImobilizadoRepository _tipoImobilizadoRepo;

        public ImobilizadoNegocio(IImobilizadoRepository repo, ITipoImobilizadoRepository tipoImobilizadoRepo)
        {
            _repo = repo;
            _tipoImobilizadoRepo = tipoImobilizadoRepo;
        }

        public List<Imobilizado> ObterTodos()
        {
            var retorno = _repo.ObterTodos();
            return retorno;
        }

        public Imobilizado Obter(string id)
        {
            var retorno = _repo.Obter(ObjectId.Parse(id));
            return retorno;
        }

        public Imobilizado Inserir(Imobilizado obj)
        {
            var tipoImobilizado = _tipoImobilizadoRepo.Obter(ObjectId.Parse(obj.TipoImobilizadoId));

            if (tipoImobilizado == null)
            {
                throw new Excecoes.ObjetoNaoEncontradoException();
            }

            var retorno = _repo.Inserir(obj);

            return retorno;
        }

        public Imobilizado Atualizar(string id, Imobilizado obj)
        {
            obj._id = ObjectId.Parse(id);

            var objExistente = _repo.Obter(obj._id);
            if (objExistente == null)
            {
                throw new Excecoes.ObjetoNaoEncontradoException();
            }

            var retorno = _repo.Atualizar(obj);
            return retorno;
        }

        public Imobilizado Apagar(string id)
        {
            var obj = _repo.Obter(ObjectId.Parse(id));
            if (obj == null)
            {
                throw new Excecoes.ObjetoNaoEncontradoException();
            }

            _repo.Apagar(obj._id);
            return obj;
        }
    }
}