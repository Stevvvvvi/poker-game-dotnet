// See https://aka.ms/new-console-template for more information
using poker_game_dotnet.Contract;

Console.WriteLine("Start App!");
int counter = 0;  
var path = Path.Combine(Directory.GetCurrentDirectory(), "poker-hands.txt");

// Read the file and display it line by line.  
foreach (string line in System.IO.File.ReadLines(path))
{  
    var allCards = line.Split(' ');
    var firstPlayerHand = allCards.Take(5);
    HandCard FirstPlayer = new HandCard(firstPlayerHand);
    var secondPlayerHand = allCards.Skip(5).Take(5);
    HandCard SecondPlayer = new HandCard(secondPlayerHand);
    Console.WriteLine("firstPlayerHand: "+String.Join(" ", firstPlayerHand));
    Console.WriteLine("secondPlayerHand: "+ String.Join(" ", secondPlayerHand));
    counter++;
}  
  
System.Console.WriteLine("There were {0} lines.", counter); 