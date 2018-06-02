using System;
using System.Collections.Generic;
using WebAPIMongoDBExample.Models;

namespace WebAPIMongoDBExample.Negocio
{
    public interface IDisponibilidadeNegocio
    {
        List<Disponibilidade> ObterTodasDisponibilidadesDoDia();
        List<Disponibilidade> ObterTodasDisponibilidadesDoDia(DateTime data);
        List<Disponibilidade> ObterTodasDisponibilidadesDoDia(string id, DateTime data);
    }
}