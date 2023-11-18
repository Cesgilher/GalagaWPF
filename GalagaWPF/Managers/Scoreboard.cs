using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalagaWPF.Models;

namespace GalagaWPF.Controller
{
    public class Scoreboard
    {
        private List<Score> scores = new();
        private DBContext db = new DBContext();

        public void SaveScore(Score score)
        {
            scores = db.Scores.ToList();
            bool allPointsLower = scores
                .Where(s => s.IdUser == score.IdUser)
                .All(s => s.Points < score.Points);

            if (allPointsLower)
            {
                db.Scores.Add(score);
                db.SaveChanges();
            }

        }

        public List<object> GetHighscores()
        {
            var highscores = (
                from score in db.Scores
                join user in db.Users on score.IdUser equals user.Id
                orderby score.Points descending
                select new
                {
                    UserName = user.Name,
                    UserLastName = user.LastName,
                    ScorePoints = score.Points,
                    ScoreLevel = score.Level
                }
            ).Take(10).ToList<object>();
            
            return highscores;

        }

        



    }
}
