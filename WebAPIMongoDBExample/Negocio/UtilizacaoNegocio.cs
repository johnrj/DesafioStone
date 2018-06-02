using WebAPIMongoDBExample.Models;
using WebAPIMongoDBExample.Repository;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

namespace WebAPIMongoDBExample.Negocio
{
    public class UtilizacaoNegocio : IUtilizacaoNegocio
    {
        IUtilizacaoRepository _repo;
        IImobilizadoRepository _repoImobilizado;
        public UtilizacaoNegocio(IUtilizacaoRepository repo, IImobilizadoRepository repoImobilizado)
        {
            _repo = repo;
            _repoImobilizado = repoImobilizado;
        }

        public List<Utilizacao> ObterTodos()
        {
            var retorno = _repo.ObterTodos();
            var idsItensUtilizados = retorno.Select(s => ObjectId.Parse(s.ItemUtilizadoId)).Distinct().ToList();

            var itensUtilizados = _repoImobilizado.Obter(idsItensUtilizados).ToDictionary(k => k._id, v => v);

            retorno.ForEach(f => f.ItemUtilizado = itensUtilizados[ObjectId.Parse(f.ItemUtilizadoId)]);

            return retorno;
        }

        public Utilizacao Inserir(Utilizacao obj)
        {
            var imobilizado = _repoImobilizado.Obter(ObjectId.Parse(obj.ItemUtilizadoId));
            if (imobilizado == null)
            {
                throw new Excecoes.ObjetoNaoEncontradoException();
            }
            if (_repo.ItemEmuso(obj))
            {
                throw new Excecoes.AcaoProibidaException();
            }

            var retorno = _repo.Inserir(obj);
            return obj;
        }

        public Utilizacao Atualizar(string id, Utilizacao obj)
        {
            obj._id = ObjectId.Parse(id);
            var objExistente = _repo.Obter(obj._id);
            if (objExistente == null)
            {
                throw new Excecoes.ObjetoNaoEncontradoException();
            }
            if (_repo.ItemEmuso(obj))
            {
                throw new Excecoes.AcaoProibidaException();
            }

            var retorno = _repo.Atualizar(obj);
            return retorno;
        }

        public Utilizacao Apagar(string id)
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