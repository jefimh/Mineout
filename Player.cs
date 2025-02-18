using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JefimHolmgrenBreakout
{
    /// <summary>
    /// Spelarobjekt som bevarar namn och högsta poäng för att kunna sparas
    /// till en poänglista och eventuellt textfil. 
    /// </summary>
    class Player
    {
        public string name;
        public int highScore;

        public Player(string name, int highScore)
        {
            this.name = name;
            this.highScore = highScore;
        }
    }
}
