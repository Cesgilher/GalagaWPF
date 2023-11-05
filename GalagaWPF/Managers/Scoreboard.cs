using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalagaWPF.Models;

namespace GalagaConC_.Controller
{
    public class Scoreboard
    {
        private List<Score> scores = new();
        private DBContext dB = new DBContext();


        public List<Score> GetScores()
        {
            scores = dB.Scores.ToList();
            return scores;
        }
        //public void SafeToScoreFile()
        //{
        //    dB.SaveAll(scores);
        //}

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

        //public void ListScores()
        //{
        //    foreach (Score score in scores)
        //    {
        //        Console.WriteLine($"Score: {score.Points}, Level: {score.Level}, User: {score.IdUser} ");

        //    }

        //}
    }
}
