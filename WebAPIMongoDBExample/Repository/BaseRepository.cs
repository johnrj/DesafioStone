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
            db = client.GetDatabase("DesafioStone");
        }

        private string ObterStringConexao()
        {
            var stringConexaoModelo = ConfigurationManager.AppSettings.Get("StringConexao");
            var usuario = ConfigurationManager.AppSettings.Get("DesafioStoneBDUsuario");
            var senha = ConfigurationManager.AppSettings.Get("DesafioStoneBDSenha");
            var host = ConfigurationManager.AppSettings.Get("DesafioStoneBDHost");
            var porta = ConfigurationManager.AppSettings.Get("DesafioStoneBDPorta");

            return string.Format(stringConexaoModelo, new string[] { usuario, senha, host, porta });
        }
    }
}