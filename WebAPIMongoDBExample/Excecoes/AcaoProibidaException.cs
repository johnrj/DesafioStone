using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPIMongoDBExample.Excecoes
{
    public class AcaoProibidaException : Exception
    {
        public AcaoProibidaException() { }
        public AcaoProibidaException(string message) : base(message) { }
        public AcaoProibidaException(string message, Exception inner) : base(message, inner) { }
    }
}