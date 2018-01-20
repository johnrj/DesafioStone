﻿using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DesafioStone.Models
{
    public class Imobilizado : DocumentoBase
    {
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public bool Ativo { get; set; }
        public string TipoImobilizadoId { get; set; }
        [BsonIgnore]
        public List<Utilizacao> Utilizacao { get; set; }
    }
}