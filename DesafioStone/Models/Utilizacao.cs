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
        [BsonElement("ItemUtilizado")]
        public Imobilizado ItemUtilizado { get; set; }
        [BsonElement("Responsavel")]
        public string Responsavel { get; set; }
        [BsonElement("Andar")]
        public int Andar { get; set; }
        [BsonElement("Sala")]
        public string Sala { get; set; }
        [BsonElement("InicioUso")]
        public DateTime? InicioUso { get; set; }
        [BsonElement("FimUso")]
        public DateTime? FimUso { get; set; }
    }
}