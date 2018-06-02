using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace WebAPIMongoDBExample.Models
{
    public class Utilizacao : DocumentoBase
    {
        public string ItemUtilizadoId { get; set; }
        [BsonIgnore]
        public Imobilizado ItemUtilizado { get; set; }
        public string Responsavel { get; set; }
        public int Andar { get; set; }
        public string Sala { get; set; }
        [BsonIgnore]
        private DateTime _inicioUso;
        public DateTime InicioUso { get { return _inicioUso.ToLocalTime(); } set { _inicioUso = value.ToUniversalTime(); } }
        [BsonIgnore]
        private DateTime? _fimUso { get; set; }
        public DateTime? FimUso
        {
            get { return _fimUso.HasValue ? _fimUso.Value.ToLocalTime() : (DateTime?)null; }
            set { _fimUso = value.HasValue ? value.Value.ToUniversalTime() : (DateTime?)null; }
        }
        public bool Devolvido { get; set; }
    }
}