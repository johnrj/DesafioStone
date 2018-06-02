using System.Collections.Generic;
using MongoDB.Bson;
using WebAPIMongoDBExample.Models;

namespace WebAPIMongoDBExample.Repository
{
    public interface ITipoImobilizadoRepository
    {
        void Apagar(ObjectId id);
        TipoImobilizado Atualizar(TipoImobilizado obj);
        TipoImobilizado Inserir(TipoImobilizado obj);
        TipoImobilizado Obter(ObjectId id);
        TipoImobilizado Obter(string nome);
        List<TipoImobilizado> ObterTodos();
    }
}