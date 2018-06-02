using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPIMongoDBExample.Excecoes
{
    public class ObjetoNaoEncontradoException : Exception
    {
        public ObjetoNaoEncontradoException() { }
        public ObjetoNaoEncontradoException(string message) : base(message) { }
        public ObjetoNaoEncontradoException(string message, Exception inner) : base(message, inner) { }
    }
}