using System.Collections.Generic;
using WebAPIMongoDBExample.Models;

namespace WebAPIMongoDBExample.Negocio
{
    public interface IImobilizadoNegocio
    {
        Imobilizado Apagar(string id);
        Imobilizado Atualizar(string id, Imobilizado obj);
        Imobilizado Inserir(Imobilizado obj);
        Imobilizado Obter(string id);
        List<Imobilizado> ObterTodos();
    }
}