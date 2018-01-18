using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Bson;

namespace DesafioStone.Models
{
    public abstract class DocumentoBase
    {
        public ObjectId _id { get; set; }
    }
}