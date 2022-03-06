// See https://aka.ms/new-console-template for more information
using poker_game_dotnet.Contract;

Console.WriteLine("Start App!");
int counter = 0;  
int firstPlayerWinCount = 0;
int secondPlayerWinCount = 0;
int tie = 0;
var path = Path.Combine(Directory.GetCurrentDirectory(), "poker-hands.txt");

// Read the file and display it line by line.  
foreach (string line in System.IO.File.ReadLines(path))
{  
    var allCards = line.Split(' ');
    var firstPlayerHand = allCards.Take(5);
    HandCard FirstPlayer = new HandCard(firstPlayerHand);
    var secondPlayerHand = allCards.Skip(5).Take(5);
    HandCard SecondPlayer = new HandCard(secondPlayerHand);
    Console.WriteLine("firstPlayerHand: "+String.Join(" ", FirstPlayer.CurrentHand));
    Console.WriteLine("firstPlayerRank: "+String.Join(" ", FirstPlayer.HandRank));
    Console.WriteLine("secondPlayerHand: "+ String.Join(" ", SecondPlayer.CurrentHand));
    Console.WriteLine("secondPlayerRank: "+String.Join(" ", SecondPlayer.HandRank));

    //compare 2 players rank
    if (FirstPlayer.HandRank > SecondPlayer.HandRank){
        firstPlayerWinCount++;
    }else if (FirstPlayer.HandRank < SecondPlayer.HandRank){
        secondPlayerWinCount++;
    }else if (FirstPlayer.HandRank == SecondPlayer.HandRank){
        // 2 players rank the same, compare rank high card first
        var firstPlayerHighCardInRankIndex = FirstPlayer.CardRanks.ToList().IndexOf(FirstPlayer.HighCardInRank);
        var secondPlayerHighCardInRankIndex = SecondPlayer.CardRanks.ToList().IndexOf(SecondPlayer.HighCardInRank);
        if ( firstPlayerHighCardInRankIndex > secondPlayerHighCardInRankIndex){
            firstPlayerWinCount++;
        }else if (firstPlayerHighCardInRankIndex < secondPlayerHighCardInRankIndex){
            secondPlayerWinCount++;
        }else if (firstPlayerHighCardInRankIndex == secondPlayerHighCardInRankIndex){
            // 2 players rank the same in card rank high card too
            var firstPlayerSecondaryHighCardInRank = FirstPlayer.CardRanks.ToList().IndexOf(FirstPlayer.SecondaryHighCardInRank);
            var secondPlayerSecondaryHighCardInRank = SecondPlayer.CardRanks.ToList().IndexOf(SecondPlayer.SecondaryHighCardInRank);
            if (firstPlayerSecondaryHighCardInRank > secondPlayerSecondaryHighCardInRank){
                firstPlayerWinCount++;
            }else if (firstPlayerSecondaryHighCardInRank < secondPlayerSecondaryHighCardInRank){
                secondPlayerWinCount++;
            }else if (firstPlayerSecondaryHighCardInRank == secondPlayerSecondaryHighCardInRank){
                // compare all cards from high to low, convert card high to index for compare
                var player1CardsIndex = FirstPlayer.CurrentHand.Select(e=>FirstPlayer.CardRanks.ToList().IndexOf(e.ToCharArray()[0]));
                var player2CardsIndex = SecondPlayer.CurrentHand.Select(e=>SecondPlayer.CardRanks.ToList().IndexOf(e.ToCharArray()[0]));
                // convert card high to index for compare
                var player1NotInPlayer2Index = player1CardsIndex.Except(player2CardsIndex).ToList();
                var player2NotInPlayer1Index = player2CardsIndex.Except(player1CardsIndex).ToList();
                if (player1NotInPlayer2Index.Last() > player2NotInPlayer1Index.Last()){
                    firstPlayerWinCount++;
                }else if (player1NotInPlayer2Index.Last() < player2NotInPlayer1Index.Last()){
                    secondPlayerWinCount++;
                }else {
                    // 2 player has exact same cards
                    System.Console.WriteLine("2 Players have exact same card"); 
                    tie++;
                }
            }
        }
    }
    counter++;
}  
System.Console.WriteLine("There were {0} lines..........................................", counter);   
System.Console.WriteLine("_________________________________"); 
System.Console.WriteLine("FirstPlayerWin: {0}", firstPlayerWinCount); 
System.Console.WriteLine("SecondPlayerWin: {0}", secondPlayerWinCount); 
System.Console.WriteLine("2 players tie: {0}", tie); 
System.Console.WriteLine("_________________________________"); 
