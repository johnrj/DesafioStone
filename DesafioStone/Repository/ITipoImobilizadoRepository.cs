﻿using System.Collections.Generic;
using System.Linq;
using DesafioStone.Models;
using MongoDB.Bson;

namespace DesafioStone.Repository
{
    public interface ITipoImobilizadoRepository
    {
        TipoImobilizado InserirTipoImobilizado(TipoImobilizado obj);
        TipoImobilizado Obter(string nome);
        List<TipoImobilizado> ObterTodos();
        void Atualizar(TipoImobilizado obj);
        TipoImobilizado Obter(ObjectId id);
        TipoImobilizado Apagar(ObjectId id);
    }
}