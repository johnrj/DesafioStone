using System.Collections.Generic;
using DesafioStone.Models;

namespace DesafioStone.Negocio
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