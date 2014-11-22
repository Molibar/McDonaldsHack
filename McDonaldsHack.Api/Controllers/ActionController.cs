using System;
using System.Configuration;
using System.Linq;
using System.Web.Http;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace McDonaldsHack.Api.Controllers
{
    [AcceptCors]
    public class LoginController : ApiController
    {
        [HttpGet]
        [Route("api/Login/{name}/{password}")]
        public LoginViewModel Login(string name, string password)
        {
            var token = Guid.NewGuid();
            var user = new User
            {
                Name = name,
                Password = password,
                Token = token.ToString()
            };
            var collection = DatabaseRepository.GetCollection<User>();
            collection.Insert(user);
            return new LoginViewModel { Name = user.Name, Password = user.Password, Token = user.Token };
        }

        [HttpGet]
        [Route("api/User/{token}")]
        public LoginViewModel GetUser(string token)
        {
            var collection = DatabaseRepository.GetCollection<User>();
            var query =
                from e in collection.AsQueryable()
                where e.Token == token
                select e;
            var user = query.FirstOrDefault();
            if (user == null) return null;
            return new LoginViewModel { Name = user.Name, Password = user.Password, Token = user.Token };
        }
    }

    public static class DatabaseRepository
    {
        private static MongoDatabase GetDatabase()
        {
            var connectionstring = ConfigurationManager.AppSettings.Get("MONGOLAB_URI");
            var url = new MongoUrl(connectionstring);
            var client = new MongoClient(url);
            var server = client.GetServer();
            var database = server.GetDatabase(url.DatabaseName);
            return database;
        }

        public static MongoCollection<T> GetCollection<T>()
        {
            var database = GetDatabase();
            return database.GetCollection<T>(typeof(T).FullName);
        }
    }

    public class LoginViewModel
    {
        public string Name { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }
    }


    public class User
    {
        public ObjectId Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }
    }
}