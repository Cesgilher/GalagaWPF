using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalagaWPF.Models;

namespace GalagaWPF.Controller
{
    public class Scoreboard : IDisposable
    {
        private List<Score> scores = new();
        private DBContext db = new DBContext();

        public Scoreboard()
        {
            this.scores = db.Scores.ToList();
        }


        public List<Score> GetScores()
        {           
            return scores;
        }

        public void SafeScores()
        {
            var existingScores = db.Scores.ToList();

            foreach (var existingScore in existingScores)
            {
                var localScore = scores.FirstOrDefault(s => s.Id == existingScore.Id);
                if (localScore == null) 
                {
                    db.Scores.Remove(existingScore);
                }
                else
                {
                    db.Entry(existingScore).CurrentValues.SetValues(localScore);
                }
            }
            
            foreach (var newScore in scores.Where(s => !existingScores .Any(e => e.Id == s.Id)))
            {
                db.Scores.Add(newScore);

            }

            db.SaveChanges();
            
        }

        public void AddScore(Score score)
        {
            bool allPointsLower = scores
                .Where(s => s.IdUser == score.IdUser)
                .All(s => s.Points < score.Points);

            if (allPointsLower)
            {
                scores.Add(score);
            }

        }

        public void Dispose()
        {
            SafeScores();

            GC.SuppressFinalize(this);
        }



    }
}
