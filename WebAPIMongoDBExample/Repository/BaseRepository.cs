using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace WebAPIMongoDBExample.Repository
{
    public abstract class BaseRepository
    {
        public IMongoDatabase db;

        public BaseRepository()
        {
            MongoClient client = new MongoClient(ObterStringConexao());
            db = client.GetDatabase("WebAPIMongoDBExample");
        }

        private string ObterStringConexao()
        {
            var stringConexaoModelo = ConfigurationManager.AppSettings.Get("StringConexao");
            var usuario = ConfigurationManager.AppSettings.Get("BDUsuario");
            var senha = ConfigurationManager.AppSettings.Get("BDSenha");
            var host = ConfigurationManager.AppSettings.Get("BDHost");
            var porta = ConfigurationManager.AppSettings.Get("BDPorta");

            return string.Format(stringConexaoModelo, new string[] { usuario, senha, host, porta });
        }
    }
}