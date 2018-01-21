using System.Collections.Generic;
using DesafioStone.Models;
using MongoDB.Bson;

namespace DesafioStone.Repository
{
    public interface IImobilizadoRepository
    {
        Imobilizado Apagar(ObjectId id);
        Imobilizado Atualizar(Imobilizado obj);
        Imobilizado Inserir(Imobilizado obj);
        Imobilizado Obter(ObjectId id);
        List<Imobilizado> Obter(bool ativo);
        List<Imobilizado> ObterTodos();
    }
}