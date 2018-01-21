using System.Collections.Generic;
using System.Linq;
using DesafioStone.Models;
using MongoDB.Bson;

namespace DesafioStone.Repository
{
    public interface ITipoImobilizadoRepository
    {
        List<TipoImobilizado> ObterTodos();
        TipoImobilizado Obter(ObjectId id);
        TipoImobilizado Obter(string nome);
        TipoImobilizado Inserir(TipoImobilizado obj);
        TipoImobilizado Atualizar(TipoImobilizado obj);
        void Apagar(ObjectId id);
    }
}