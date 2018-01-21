﻿using System;
using System.Collections.Generic;
using DesafioStone.Models;
using MongoDB.Bson;

namespace DesafioStone.Repository
{
    public interface IUtilizacaoRepository
    {
        void Apagar(ObjectId id);
        Utilizacao Atualizar(Utilizacao obj);
        Utilizacao Inserir(Utilizacao obj);
        Utilizacao Obter(ObjectId id);
        List<Utilizacao> ObterTodos();
        List<Utilizacao> Obter(DateTime data);
        List<Utilizacao> Obter(string id, DateTime data);
        bool ItemEmuso(Utilizacao obj);
    }
}