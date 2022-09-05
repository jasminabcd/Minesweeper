using Minesweeper;

var startTime = DateTime.Now;
const int sideLength = ConstHelper.SideLength;

var game = new Game(sideLength);

game.PrintGame();

string auswahl;

while (!game.IsGameover && !game.IsWon())
{
    Console.WriteLine("Wähle aus:");
    Console.WriteLine("1. Feld aufdecken");
    Console.WriteLine("2. Flage setzen");
    Console.WriteLine("3. Flage entfernen");

    auswahl = Console.ReadLine();
    int auswahlInt;


    bool success = int.TryParse(auswahl, out auswahlInt);

    if (!success)
    {
        Console.WriteLine("Der von Ihnen eingegebene Wert ist eine ungültige Zahl. Geben Sie eine Zahl von 1-3 ein:");
        continue;
    }

    if (auswahlInt > 3 || auswahlInt < 1)
    {
        Console.WriteLine("Der von Ihnen eingegebene Wert ist ungueltig. Wähle neu:");
        continue;
    }



    switch (auswahlInt)
    {
        case 1:
            game.DiscoverField();
            break;

        case 2:
            game.SetFlag();
            break;

        case 3:
            game.RemoveFlag();
            break;

    }

    var duration = DateTime.Now - startTime;
    Console.WriteLine(duration);
}

if (game.IsGameover)
{
    Console.WriteLine("Sie haben verloren :(");
    var gameOverArt = File.ReadAllText("GameOver.txt");
    Console.ForegroundColor = ConsoleColor.Magenta;
    Console.WriteLine(gameOverArt);
    Console.ForegroundColor = ConsoleColor.White;




}

if (game.IsWon())
{
    Console.WriteLine("Sie haben gewonnen! :)");
    var gameOverArt = File.ReadAllText("SieHabenGewonnen.txt");
    Console.ForegroundColor = ConsoleColor.Magenta;
    Console.WriteLine(gameOverArt);
    Console.ForegroundColor = ConsoleColor.White;
}