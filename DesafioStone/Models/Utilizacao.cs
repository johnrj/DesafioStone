using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DesafioStone.Models
{
    public class Utilizacao
    {
        public Imobilizado ItemUtilizado { get; set; }
        public string Responsavel { get; set; }
        public int Andar { get; set; }
        public string Sala { get; set; }
        public DateTime? InicioUso { get; set; }
        public DateTime? FimUso { get; set; }
    }
}