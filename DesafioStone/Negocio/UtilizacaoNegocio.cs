using DesafioStone.Models;
using DesafioStone.Repository;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

namespace DesafioStone.Negocio
{
    public class UtilizacaoNegocio : IUtilizacaoNegocio
    {
        IUtilizacaoRepository repo;
        IImobilizadoRepository repoImobilizado;
        public UtilizacaoNegocio()
        {
            repo = new UtilizacaoRepository();
            repoImobilizado = new ImobilizadoRepository();
        }

        public List<Utilizacao> ObterTodos()
        {
            var retorno = repo.ObterTodos();
            var idsItensUtilizados = retorno.Select(s => ObjectId.Parse(s.ItemUtilizadoId)).Distinct().ToList();

            var itensUtilizados = repoImobilizado.Obter(idsItensUtilizados).ToDictionary(k => k._id, v => v);

            retorno.ForEach(f => f.ItemUtilizado = itensUtilizados[ObjectId.Parse(f.ItemUtilizadoId)]);

            return retorno;
        }

        public Utilizacao Inserir(Utilizacao obj)
        {
            var imobilizado = repoImobilizado.Obter(ObjectId.Parse(obj.ItemUtilizadoId));
            if (imobilizado == null)
            {
                throw new Excecoes.ObjetoNaoEncontradoException();
            }
            if (repo.ItemEmuso(obj))
            {
                throw new Excecoes.AcaoProibidaException();
            }

            var retorno = repo.Inserir(obj);
            return obj;
        }

        public Utilizacao Atualizar(string id, Utilizacao obj)
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

        public Utilizacao Apagar(string id)
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