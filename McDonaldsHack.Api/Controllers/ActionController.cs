using System;
using System.Data.Entity;
using System.Web.Http;

namespace McDonaldsHack.Api.Controllers
{
    [AcceptCors]
    public class LoginController : ApiController
    {

        [HttpGet]
        [Route("api/LogIn/{name}/{password}")]
        public dynamic CheckIn(string name, string password)
        {
            var token = Guid.NewGuid();
            return new {name, password, token };
        }

        [HttpGet]
        [Route("api/Scan/{missionId}/{characterName}")]
        public Game Scan(int missionId, string characterName)
        {
            if (Database.Game == null) Database.Game = Game.Init();
            Database.Game.FinishMission(characterName, missionId);
            return Database.Game;
        }

        [HttpGet]
        [Route("api/CheckOut/{characterName}")]
        public Game CheckOut(string characterName)
        {
            if (Database.Game == null) Database.Game = Game.Init();
            Database.Game.CheckOut(characterName);
            return Database.Game;
        }
    }

    [AcceptCors]
    public class ScoreController : ApiController
    {

        [HttpGet]
        [Route("api/Scores/{table}")]
        public List<HighScore> Get(string table)
        {
            if (Database.HighScores == null) return null;
            return Database.HighScores[table];
        }

        [HttpPost]
        [Route("api/Scores/{table}")]
        public List<HighScore> Post(string table, string name, int score)
        {
            if (Database.Game == null) Database.Game = Game.Init();
            Database.HighScores[table].Add(new HighScore { Name = name, Score = score });
            return Database.HighScores[table];
        }
    }
}