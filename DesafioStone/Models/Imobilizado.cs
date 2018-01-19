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
        [BsonElement("Ativo")]
        public bool Ativo { get; set; }
        [BsonElement("DataCadastro")]
        public DateTime DataCadastro { get; set; }
        [BsonElement("TipoImobilizado")]
        public TipoImobilizado TipoImobilizado { get; set; }
    }
}