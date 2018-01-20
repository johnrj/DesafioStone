using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DesafioStone.Models
{
    public class Imobilizado : DocumentoBase
    {
        [BsonElement("Nome")]
        public string Nome { get; set; }
        [BsonElement("Descricao")]
        public string Descricao { get; set; }
        [BsonElement("Ativo")]
        public bool Ativo { get; set; }
        [BsonIgnore]
        public string TipoImobilizadoId { get; set; }
        [BsonElement("TipoImobilizado")]
        public TipoImobilizado TipoImobilizado { get; set; }
        [BsonElement("Utilizacao")]
        public List<Utilizacao> Utilizacao { get; set; }
    }
}