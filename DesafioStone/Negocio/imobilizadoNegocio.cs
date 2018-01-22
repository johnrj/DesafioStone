using DesafioStone.Models;
using DesafioStone.Repository;
using MongoDB.Bson;
using System.Collections.Generic;

namespace DesafioStone.Negocio
{
    public class ImobilizadoNegocio : IImobilizadoNegocio
    {
        IImobilizadoRepository repo;
        public ImobilizadoNegocio()
        {
            repo = new ImobilizadoRepository();
        }

        public List<Imobilizado> ObterTodos()
        {
            var retorno = repo.ObterTodos();
            return retorno;
        }

        public Imobilizado Obter(string id)
        {
            var retorno = repo.Obter(ObjectId.Parse(id));
            return retorno;
        }

        public Imobilizado Inserir(Imobilizado obj)
        {
            ITipoImobilizadoRepository tipoImobilizadoRepo = new TipoImobilizadoRepository();
            var tipoImobilizado = tipoImobilizadoRepo.Obter(ObjectId.Parse(obj.TipoImobilizadoId));

            if (tipoImobilizado == null)
            {
                throw new Excecoes.ObjetoNaoEncontradoException();
            }

            var retorno = repo.Inserir(obj);

            return retorno;
        }

        public Imobilizado Atualizar(string id, Imobilizado obj)
        {
            obj._id = ObjectId.Parse(id);

            var objExistente = repo.Obter(obj._id);
            if (objExistente == null)
            {
                throw new Excecoes.ObjetoNaoEncontradoException();
            }

            var retorno = repo.Atualizar(obj);
            return retorno;
        }

        public Imobilizado Apagar(string id)
        {
            var obj = repo.Obter(ObjectId.Parse(id));
            if (obj == null)
            {
                throw new Excecoes.ObjetoNaoEncontradoException();
            }

            repo.Apagar(obj._id);
            return obj;
        }
    }
}