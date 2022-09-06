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

        string userInput = Console.ReadLine();
        int userInputInt;


        bool success = int.TryParse(userInput, out userInputInt);

        if (!success)
        {
            Console.WriteLine("Der von Ihnen eingegebene Wert ist eine ungültige Zahl. Geben Sie eine Zahl von 1-3 ein:");
            continue;
        }

        if (userInputInt > 3 || userInputInt < 1)
        {
            Console.WriteLine("Bitte geben Sie eine Zahl von 1-3 ein:");
        
           continue;
       }



        switch (userInputInt)
        {
            case 1:

                Console.WriteLine("Wählen Sie eine Schwierigkeitsstufe:");
                Console.WriteLine(" Leicht(1) / Mittel(2) / Schwer(3):");

                string userChoice = Console.ReadLine();
                int userChoiceInt;


                bool successed = int.TryParse(userChoice, out userChoiceInt);

                if (!successed)
                {
                    Console.WriteLine("Der von Ihnen eingegebene Wert ist eine ungültige Zahl. Geben Sie eine Zahl von 1-3 ein:");
                    continue;
                }

                if (userInputInt > 3 || userInputInt < 1)
                {
                    Console.WriteLine("Bitte geben Sie eine Zahl von 1-3 ein:");

                    continue;
                }
                switch (userChoiceInt)
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

                System.Environment.Exit(0);
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


}