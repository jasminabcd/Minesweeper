namespace Minesweeper
{
    internal class Game
    {
        private Grid _grid;
        private DateTime _startTime;
        private int _id;
        private string _playerName;
        public bool IsGameover { get; set; }
        public string Difficulty { get; set; }


        /// <summary>
        /// Ctor for start new Game
        /// </summary>
        /// <param name="difficulty"></param>
        /// <param name="playerName"></param>
        /// <exception cref="Exception"></exception>
        public Game(string difficulty, string playerName)
        {
            var gridSize = difficulty switch
            {
                "Leicht" => 4,
                "Mittel" => 8,
                "Schwer" => 12,
                _ => throw new Exception("Not Valid option for gridsize")
            };
            _id = 0;
            _playerName = playerName;
            _grid = new Grid(gridSize);
            IsGameover = false;
            Difficulty = difficulty;
        }

        /// <summary>
        /// Ctor from Persistence Game
        /// </summary>
        /// <param name="difficulty"></param>
        /// <param name="playerName"></param>
        /// <param name="grid"></param>
        public Game(string difficulty, string playerName, Grid grid, int id)
        {
            _id = id;
            _playerName = playerName;
            _grid = grid;
            IsGameover = false;
            Difficulty = difficulty;
        }

        public PersistenceGame ToPersistenceGame()
        {
            var persistenceGame = new PersistenceGame();

            persistenceGame.PlayerName = _playerName;

            persistenceGame.Duration = GetDuration();

            persistenceGame.LastPlayedOn = DateTime.Now;

            persistenceGame.Fields = _grid.GetPersistenceField();

            persistenceGame.Difficulty = Difficulty;

            persistenceGame.ID = _id;


            if (IsWon())
            {
                persistenceGame.GameState = GameState.Won;
            }
            else if (IsGameover)
            {
                persistenceGame.GameState = GameState.GameOver;
            }
            else
            {
                persistenceGame.GameState = GameState.Playing;
            }


            return persistenceGame;
        }


        public static Game FromPersistenceGame(PersistenceGame persistenceGame)
        {
            var grid = Grid.FromFields(persistenceGame.Fields);
            var game = new Game(persistenceGame.Difficulty, persistenceGame.PlayerName, grid, persistenceGame.ID);

            return game;
        }

        public void PrintGame()
        {
            Console.Clear();
            _grid.PrintGrid();
        }

        public void DiscoverField()
        {
            Console.WriteLine("Waehlen Sie ein Feld aus:");
            var koordinaten = ConsoleHelper.ReadCoordinates(_grid.SideLength);
            if (_grid.GetField(koordinaten).IsDiscovered)
            {
                Console.WriteLine("Dieses Feld wurde bereits aufgedeckt. Wählen Sie neu:");
                return;
            }
            IsGameover = _grid.DiscoverFieldAndCheckGameOver(koordinaten);
            _grid.PrintGrid();
        }

        public void SetFlag()
        {
            Console.WriteLine("Waehlen Sie ein Feld aus:");
            var koordinaten = ConsoleHelper.ReadCoordinates(_grid.SideLength);
            _grid.FlagField(koordinaten);
            _grid.PrintGrid();
        }

        public void RemoveFlag()
        {
            Console.WriteLine("Waehlen Sie ein Feld aus:");
            var koordinaten = ConsoleHelper.ReadCoordinates(_grid.SideLength);
            _grid.RemoveFlagField(koordinaten);
            _grid.PrintGrid();
        }

        public void Start()
        {
            _startTime = DateTime.Now;
        }
        public bool IsWon()
        {
            return _grid.IsWon();

        }

        internal TimeSpan GetDuration()
        {
            return DateTime.Now - _startTime;
        }
    }
}


