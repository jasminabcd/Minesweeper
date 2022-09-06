namespace Minesweeper
{
    internal class Game
    {

        private Grid _grid;



        public Game(int sideLength)
        {
            _grid = new Grid(sideLength);
            IsGameover = false;

            
        }

        //public Home()
        //{
            
        //}

        public bool IsGameover { get; set; }
        


        public void PrintGame()
        {
            _grid.PrintGrid();
        }

        public void DiscoverField()
        {
            Console.WriteLine("Waehlen Sie ein Feld aus:");
            var koordinaten = ConsoleHelper.ReadCoordinates();
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
            var koordinaten = ConsoleHelper.ReadCoordinates();
            _grid.FlagField(koordinaten);
            _grid.PrintGrid();

        }

        public void RemoveFlag()
        {
            Console.WriteLine("Waehlen Sie ein Feld aus:");
            var koordinaten = ConsoleHelper.ReadCoordinates();
            _grid.RemoveFlagField(koordinaten);
            _grid.PrintGrid();


        }

        public bool IsWon()
        {
            return _grid.IsWon();


        }

    }
}


