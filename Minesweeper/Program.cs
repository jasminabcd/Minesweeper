using Minesweeper;

while(true)
{

    Game game = null;

    do
    {
        var WelcomeArt = File.ReadAllText("Welcome.txt");
        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.WriteLine(WelcomeArt);
        Console.ForegroundColor = ConsoleColor.White;

        Console.WriteLine("1. Neues Spiel starten");
        Console.WriteLine("2. Weiter spielen");
        Console.WriteLine("3. Exit");

        Console.WriteLine("Wähle aus (1, 2 oder 3):");
        var userInputInt = ConsoleHelper.UserInput();

        switch (userInputInt)
        {
            case 1:

                Console.WriteLine("Wählen Sie eine Schwierigkeitsstufe:");
                Console.WriteLine(" Leicht(1) / Mittel(2) / Schwer(3):");
                var difficulty = ConsoleHelper.UserInput();

                switch (difficulty)
                {
                    case 1:

                        ConstHelper.SideLength = 4;
                        // SideLength = 4
                        break;
                    case 2:
                        ConstHelper.SideLength = 8;
                        // SideLength = 8
                        break;
                    case 3:
                        ConstHelper.SideLength = 12;
                        // SideLength = 12
                        break;
                }

                game = new Game(ConstHelper.SideLength);
                break;

            case 2:

                // Spielstand speichern und wieder aufrufen
                break;

            case 3:
                Environment.Exit(0);
                break;
        }
    }
    while (game == null);

    game.PrintGame();

    string auswahl;

    var startTime = DateTime.Now;

    while (!game.IsGameover && !game.IsWon())
    {
        Console.WriteLine("Wähle aus:");
        Console.WriteLine("1. Feld aufdecken");
        Console.WriteLine("2. Flage setzen");
        Console.WriteLine("3. Flage entfernen");
        var activity = ConsoleHelper.UserInput();

        switch (activity)
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
}