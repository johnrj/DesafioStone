using System.Collections.Generic;
using DesafioStone.Models;

namespace DesafioStone.Negocio
{
    public interface IUtilizacaoNegocio
    {
        Utilizacao Apagar(string id);
        Utilizacao Atualizar(string id, Utilizacao obj);
        Utilizacao Inserir(Utilizacao obj);
        List<Utilizacao> ObterTodos();
    }
}