using DesafioStone.Models;
using DesafioStone.Repository;
using MongoDB.Bson;
using System.Collections.Generic;
using System.Linq;

namespace DesafioStone.Negocio
{
    public class TipoImobilizadoNegocio : ITipoImobilizadoNegocio
    {
        ITipoImobilizadoRepository repo;
        public TipoImobilizadoNegocio()
        {
            repo = new TipoImobilizadoRepository();
        }

        public List<TipoImobilizado> ObterTodos()
        {
            var retorno = repo.ObterTodos();
            return retorno;
        }

        public TipoImobilizado Obter(string id)
        {
            var retorno = repo.Obter(ObjectId.Parse(id));
            return retorno;
        }

        public TipoImobilizado Inserir(TipoImobilizado obj)
        {
            var objExistente = repo.Obter(obj.Nome);
            if (objExistente != null)
            {
                throw new Excecoes.ObjetoDuplicadoException();
            }

            var retorno = repo.Inserir(obj);
            return retorno;
        }

        public TipoImobilizado Atualizar(string id, TipoImobilizado obj)
        {
            var objExistente = repo.Obter(ObjectId.Parse(id));
            if (objExistente == null)
            {
                throw new Excecoes.ObjetoNaoEncontradoException();
            }

            var retorno = repo.Atualizar(obj);
            return retorno;
        }

        public TipoImobilizado Apagar(string id)
        {
            var obj = repo.Obter(ObjectId.Parse(id));
            if (obj == null)
            {
                throw new Excecoes.ObjetoNaoEncontradoException();
            }

            IImobilizadoRepository imobilizadoRepo = new ImobilizadoRepository();
            if (imobilizadoRepo.ObterPeloTipo(id).Any())
            {
                throw new Excecoes.AcaoProibidaException();
            }

            repo.Apagar(obj._id);

            return obj;
        }
    }
}