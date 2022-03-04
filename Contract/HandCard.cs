using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace poker_game_dotnet.Contract
{
    public class HandCard
    {
        private IEnumerable<string> currentHand;
        public HandCard(IEnumerable<string> currentHand)
        {
            this.currentHand=currentHand;
        }
        
    }
}