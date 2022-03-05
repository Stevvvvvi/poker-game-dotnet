using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace poker_game_dotnet.Contract
{
    public class HandCard
    {
        public List<string> currentHand = new List<string>();
        private int rank;

        private IEnumerable<char> suits = new List<char>(){'C', 'D', 'H', 'S'};
        private IEnumerable<char> cardRanks = new List<char>(){
            '2','3','4','5','6','7','8','9','T','J','Q','K','A'
        };
        public HandCard(IEnumerable<string> inputHand)
        {
            // we sort the card based on number first
            foreach(var cardRank in cardRanks){
                foreach(var currentInputCard in inputHand){
                    if (cardRank == currentInputCard.ToCharArray()[0]){
                        this.currentHand.Add(currentInputCard);
                    }
                }
            }
        }
        private bool IsFlush(){
            //get all suits
            var handSuits = currentHand.Select(e=>e.ToCharArray()[1]);
            var EachTypesOfSuit = new Dictionary<char,int>();
            foreach(var eachSuit in suits){
                EachTypesOfSuit[eachSuit]++;
            }
            if (EachTypesOfSuit.Any(e=>e.Value==5)){
                return true;
            }else{
                return false;
            }
        }

        public void EvaluateHand(){
            // search for royal flush

        }
        
    }
}