using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DesafioStone.Models
{
    public class TipoImobilizado : DocumentoBase
    {
        [BsonElement("Nome")]
        public string Nome { get; set; }
    }
}