using System;
using System.Collections.Generic;
using DesafioStone.Models;

namespace DesafioStone.Repository
{
    public interface IDisponibilidadeRepository
    {
        List<Disponibilidade> ObterDisponibilidade(string id = null, DateTime? data = null);
    }
}