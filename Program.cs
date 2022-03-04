// See https://aka.ms/new-console-template for more information
Console.WriteLine("Start App!");
int counter = 0;  
var path = Path.Combine(Directory.GetCurrentDirectory(), "poker-hands.txt");
Console.WriteLine(path);
// Read the file and display it line by line.  
foreach (string line in System.IO.File.ReadLines(path))
{  
    System.Console.WriteLine(line);  
    counter++;  
}  
  
System.Console.WriteLine("There were {0} lines.", counter); 