using System.Collections.Generic;
using WebAPIMongoDBExample.Models;

namespace WebAPIMongoDBExample.Negocio
{
    public interface IUtilizacaoNegocio
    {
        Utilizacao Apagar(string id);
        Utilizacao Atualizar(string id, Utilizacao obj);
        Utilizacao Inserir(Utilizacao obj);
        List<Utilizacao> ObterTodos();
    }
}