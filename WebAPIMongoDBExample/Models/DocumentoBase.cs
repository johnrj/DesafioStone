using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace WebAPIMongoDBExample.Models
{
    public abstract class DocumentoBase
    {
        [BsonId]
        public ObjectId _id { get; set; }
    }
}