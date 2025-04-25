using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameV10.GameStates.Menus
{
    internal class Score
    {
        public string PlayerName;
        public int PlayerScore;
        public float Survivaltime;
        public int Kills;
        public Score(string _PlayerName, int _PlayerScore, float survivaltime, int kills)
        {
            PlayerName = _PlayerName;
            PlayerScore = _PlayerScore;
            Survivaltime = survivaltime;
            Kills = kills;
        }

        public string WriteScoreRecord()
        {
            return PlayerName + "," + PlayerScore.ToString() + "," + Survivaltime.ToString() + "," + Kills.ToString();
        }
    }
}
