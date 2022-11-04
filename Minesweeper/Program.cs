using efCoreTest;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Minesweeper;
using Minesweeper.Persistence;
using Personen;

string difficultyText = string.Empty;
using (var context = new MinesweeperContext())
{
    context.Database.Migrate();
}

while (true)
{

    Game game = null;
    do
    {
       
        ConsoleHelper.WelcomPrint();



        ConsoleHelper.StartMenue();

        var userInputInt = ConsoleHelper.UserInput(1, 4);

        switch (userInputInt)
        {
            case 1:

                Console.WriteLine("Geben Sie einen Spielernamen ein:");
                string playerName = ConsoleHelper.PlayerName();
                Console.WriteLine("Wählen Sie eine Schwierigkeitsstufe:");
                Console.WriteLine("Leicht(1) / Mittel(2) / Schwer(3):");
                var difficulty = ConsoleHelper.DifficultyInput();
                game = new Game(difficulty, playerName);

                break;

            case 2:
                game = LoadGame();
                
                //PersistenceService.LoadChoosenGame();

                break;

            case 3:
                ConsoleHelper.PrintHighscore();
                break;
            case 4:
                Environment.Exit(0);
                

                break;
        }
    }
    while (game == null);

    game.Start();
    game.PrintGame();



    while (!game.IsGameover && !game.IsWon())
    {
        Console.WriteLine("Wähle aus:");
        Console.WriteLine("1. Feld aufdecken");
        Console.WriteLine("2. Flage setzen");
        Console.WriteLine("3. Flage entfernen");
        Console.WriteLine("4. Spiel abbrechen und Speichern");
        var activity = ConsoleHelper.UserInput(1, 4);


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
                
            case 4:
                
                new PersistenceService().SaveOrUpdateGame(game);
                ConsoleHelper.StartMenue();
                return;
               


        }

        Console.WriteLine(game.GetDuration());
        
    }
    var duration = game.GetDuration();

    if (game.IsGameover)
    {

        ConsoleHelper.IfGameIsOver();
    }

    if (game.IsWon())
    {

        ConsoleHelper.IfGameIsWon();


        Console.Write("Tippen Sie einen Spielernamen ein:");
        string playerName;
        playerName = ConsoleHelper.PlayerName();

        var sqlHelper = new SqlHelper(ConstHelper.connectionString);

        sqlHelper.AddHighscore(duration.Seconds, playerName, DateTime.Now, difficultyText);
    }
}

Game LoadGame()
{
    Console.Clear();
    var persistanceService = new PersistenceService();

    var games = persistanceService.LoadGames();

    var cnt = 0;
    Console.WriteLine("Number\tName\tDate");
    foreach (var game in games)
    {
        Console.WriteLine($"{++cnt}.\t{game.PlayerName}\t{game.LastPlayedOn:dd.MM.yyyy hh:mm}");
    }

    Console.ReadKey();

    var id = games[3].ID;

    return persistanceService.RestoreGame(id);
}