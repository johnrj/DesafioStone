using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPIMongoDBExample.Models
{
    public class Disponibilidade
    {
        public Imobilizado Imobilizado { get; set; }
        public List<DateTime> HorasDisponiveis { get; set; }
        public List<DateTime> HorasIndisponiveis { get; set; }
    }
}