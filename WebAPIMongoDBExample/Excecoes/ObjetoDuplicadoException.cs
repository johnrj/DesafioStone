using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPIMongoDBExample.Excecoes
{
    public class ObjetoDuplicadoException : Exception
    {
        public ObjetoDuplicadoException() { }
        public ObjetoDuplicadoException(string message) : base(message) { }
        public ObjetoDuplicadoException(string message, Exception inner) : base(message, inner) { }
    }
}