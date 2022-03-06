using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace poker_game_dotnet.Contract
{
    public class HandCard
    {
        public List<string> CurrentHand { get; private set; } = new List<string>();
        public int HandRank { get; private set; } = 1;
        public char HighCardInRank { get; private set; } = '2';
        public char SecondaryHighCardInRank { get; private set; } = '2';

        public IEnumerable<char> Suits { get; private set; } = new List<char>(){'C', 'D', 'H', 'S'};
        public IEnumerable<char> CardRanks { get; private set; } = new List<char>(){
            '2','3','4','5','6','7','8','9','T','J','Q','K','A'
        };
        public HandCard(IEnumerable<string> inputHand)
        {
            // we sort the card based on number first
            foreach(var cardRank in CardRanks){
                foreach(var currentInputCard in inputHand){
                    if (cardRank == currentInputCard.ToCharArray()[0]){
                        this.CurrentHand.Add(currentInputCard);
                    }
                }
            }

            EvaluateHand();
        }
        private bool IsFlush(){
            //get all Suits
            var EachTypesOfSuit= GetEachSuitAmount();
            if (EachTypesOfSuit.Any(e=>e.Value==5)){
                return true;
            }else{
                return false;
            }
        }
        private Dictionary<char, int> GetEachSuitAmount(){
            var handSuits = CurrentHand.Select(e=>e.ToCharArray()[1]);
            var EachTypesOfSuit = new Dictionary<char,int>();
            foreach(var suit in handSuits.Distinct()){
                EachTypesOfSuit.Add(suit, 0);
            }
            foreach(var eachSuit in handSuits){
                EachTypesOfSuit[eachSuit]++;
            }
            return EachTypesOfSuit;
        }
        private Dictionary<char, int> GetEachCardAmount(){
            // get all card number
            var handCardNumbers = CurrentHand.Select(e=>e.ToCharArray()[0]);
            var EachTypesOfCard = new Dictionary<char, int>();
            foreach(var card in handCardNumbers.Distinct()){
                EachTypesOfCard.Add(card, 0);
            }
            foreach(var cardRank in handCardNumbers){
                EachTypesOfCard[cardRank]++;
            }
            return EachTypesOfCard;
        }
        private string IsStraight(){
            var startCardIndex = CardRanks.ToList().FindIndex(e=>e==CurrentHand[0].ToCharArray()[0]);
            var cardStraightRank = String.Join("",CardRanks.ToList().Skip(startCardIndex).Take(5));
            var currentHandCardConcat = String.Join("", this.CurrentHand.Select(e=>e.ToCharArray()[0]));
            if (currentHandCardConcat=="TJQKA" && currentHandCardConcat == cardStraightRank){
                return "royalStraight";
            }else if (currentHandCardConcat==cardStraightRank){
                return "straight";
            }else{
                return "notStraight";
            }
        }
        private int GetHowManyPairs(){
            var eachCardAmount = GetEachCardAmount();
            var pairAmount = eachCardAmount.Where(e=>e.Value == 2).Count();
            return pairAmount;
        }

        public void EvaluateHand(){
            var eachCardAmount = GetEachCardAmount();
            // search for royalFlush
            if (IsFlush() && IsStraight()=="royalStraight"){
                HandRank = 10;
            }else if (IsFlush() && IsStraight() == "straight"){
                // straight Flush
                HandRank = 9;
                this.HighCardInRank = CurrentHand.Last().ToCharArray()[0];
            }else if (eachCardAmount.Any(e=>e.Value==4)){
                // Four of kind
                HandRank = 8;
                this.HighCardInRank = eachCardAmount.First(e=>e.Value==4).Key;
            }else if (eachCardAmount.Any(e=>e.Value==3) && GetHowManyPairs() == 1){
                // Full house
                HandRank = 7;
                this.HighCardInRank = eachCardAmount.First(e=>e.Value==3).Key;
                this.SecondaryHighCardInRank = eachCardAmount.First(e=>e.Value==2).Key;
            }else if (IsFlush()==true){
                // flush
                HandRank = 6;
                this.HighCardInRank = CurrentHand.Last().ToCharArray()[0];
            }else if (IsStraight() == "straight"){
                // straight
                HandRank = 5;
                this.HighCardInRank = CurrentHand.Last().ToCharArray()[0];
            }else if (eachCardAmount.Any(e=>e.Value==3)){
                //Three of a Kind
                HandRank = 4;
                this.HighCardInRank = eachCardAmount.First(e=>e.Value==3).Key;
            }else if (GetHowManyPairs()==2){
                // 2 pairs
                HandRank = 3;
                var pairAmount = eachCardAmount.Where(e=>e.Value == 2);
                this.HighCardInRank = pairAmount.Last().Key;
                this.SecondaryHighCardInRank = pairAmount.First().Key;
            }else if (GetHowManyPairs()==1){
                // 1 pair
                HandRank = 2;
                this.HighCardInRank = eachCardAmount.First(e=>e.Value==2).Key;
            }else{
                HandRank = 1;
                this.HighCardInRank = CurrentHand.Last().ToCharArray()[0];
            }
        }
        
    }
}