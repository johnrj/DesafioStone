using System.Collections.Generic;
using System.Linq;
using DesafioStone.Models;
using MongoDB.Bson;

namespace DesafioStone.Repository
{
    public interface ITipoImobilizadoRepository
    {
        List<TipoImobilizado> ObterTodos();
        TipoImobilizado Obter(string nome);
        TipoImobilizado Inserir(TipoImobilizado obj);
        TipoImobilizado Atualizar(string id, TipoImobilizado obj);
        TipoImobilizado Apagar(string id);
    }
}