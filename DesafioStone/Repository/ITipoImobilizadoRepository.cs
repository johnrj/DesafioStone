using System.Collections.Generic;
using System.Linq;
using DesafioStone.Models;
using MongoDB.Bson;

namespace DesafioStone.Repository
{
    public interface ITipoImobilizadoRepository
    {
        TipoImobilizado Inserir(TipoImobilizado obj);
        TipoImobilizado Obter(string nome);
        List<TipoImobilizado> ObterTodos();
        TipoImobilizado Atualizar(TipoImobilizado obj);
        TipoImobilizado Obter(ObjectId id);
        TipoImobilizado Apagar(ObjectId id);
    }
}