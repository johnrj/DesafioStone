using System.Collections.Generic;
using WebAPIMongoDBExample.Models;

namespace WebAPIMongoDBExample.Negocio
{
    public interface ITipoImobilizadoNegocio
    {
        TipoImobilizado Apagar(string id);
        TipoImobilizado Atualizar(string id, TipoImobilizado obj);
        TipoImobilizado Inserir(TipoImobilizado obj);
        TipoImobilizado Obter(string id);
        List<TipoImobilizado> ObterTodos();
    }
}