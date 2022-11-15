using ConsoleTables;
using efCoreTest;
using Microsoft.EntityFrameworkCore;
using Minesweeper;
using Minesweeper.Persistence;
using Personen;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Reflection.Metadata.Ecma335;

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

    var savedGame = false;
    while (!game.IsGameover && !game.IsWon() && !savedGame)
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
                savedGame = true;
                break;
        }

        Console.WriteLine(game.GetDuration());

    }
    var duration = game.GetDuration();

    if (savedGame)
    {
        new PersistenceService().SaveOrUpdateGame(game);
        Console.Clear();
        continue;
    }

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

    var rowGames = games.Select(g => new RowGame()
    {
        Difficulty = g.Difficulty,
        PlayerName = g.PlayerName,
        LastPlayedOn = g.LastPlayedOn,
        Index = ++cnt
    });

    //foreach (var game in games)
    //{
    //    Console.WriteLine($"{++cnt}.\t{game.PlayerName}\t{game.LastPlayedOn:dd.MM.yyyy hh:mm}");
    //}

    ConsoleTable
        .From<RowGame>(rowGames)
        .Write();

    Console.WriteLine("Geben Sie die ID Ihres gewünschten Spiels ein:");

    while (true)
    {
        string input = Console.ReadLine();
        int intInput;

        bool successed = int.TryParse(input, out intInput);
        
        if (!successed)
        {
            Console.WriteLine("Der von Ihnen eingegebene Wert ist eine ungültige ID-Nummer. Geben Sie eine gültige ID ein:");
            continue;
        }

        if (intInput > cnt || intInput < 1)
        {
            Console.WriteLine("Bitte geben Sie eine gültige ID ein:");
            continue;
        }

        if (successed)
        {
            var id = games[intInput].ID;
            return persistanceService.RestoreGame(intInput);
        }

    }
}

public class RowGame
{
    public int Index { get; set; }

    public string PlayerName { get; set; }

    public string Difficulty { get; set; }

    public DateTime LastPlayedOn { get; set; }
}