using System.Collections.Generic;
using WebAPIMongoDBExample.Models;
using MongoDB.Bson;

namespace WebAPIMongoDBExample.Repository
{
    public interface IImobilizadoRepository
    {
        void Apagar(ObjectId id);
        Imobilizado Atualizar(Imobilizado obj);
        Imobilizado Inserir(Imobilizado obj);
        Imobilizado Obter(ObjectId id);
        List<Imobilizado> Obter(bool ativo);
        List<Imobilizado> ObterTodos();
        List<Imobilizado> ObterPeloTipo(string id);
        List<Imobilizado> Obter(List<ObjectId> ids);
    }
}