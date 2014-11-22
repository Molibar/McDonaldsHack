using System;
using System.Web.Http;

namespace McDonaldsHack.Api.Controllers
{
    [AcceptCors]
    public class LoginController : ApiController
    {
        [HttpGet]
        [Route("api/Login/{name}/{password}")]
        public LoginViewModel CheckIn(string name, string password)
        {
            var token = Guid.NewGuid();
            return new LoginViewModel {Name = name, Password = password, Token = token.ToString()};
        }
    }

    public class LoginViewModel
    {
        public string Name { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }
    }
}