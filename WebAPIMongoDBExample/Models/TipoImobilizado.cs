using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace WebAPIMongoDBExample.Models
{
    public class TipoImobilizado : DocumentoBase
    {
        public string Nome { get; set; }
    }
}