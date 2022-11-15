namespace Minesweeper
{
    public class Field
    {
        private readonly int _id;
        public Field(PersistenceField persistenceField)
        {
            IsDiscovered = persistenceField.IsDiscovered;
            IsBomb = persistenceField.IsBomb;
            IsFlag = persistenceField.IsFlag;
            BombsAroundMe = persistenceField.BombsAroundMe;
            IsFlag = false;
            _id = persistenceField.ID;

        }
        public Field(bool isBomb, int bombsAroundMe)
        {
            IsDiscovered = false;
            IsBomb = isBomb;
            BombsAroundMe = bombsAroundMe;
            _id = 0;
        }

        public bool IsBomb { get; }
        public int BombsAroundMe { get; }
        public bool IsDiscovered { get; private set; }
        public bool IsFlag { get; set; }


        public void Discover()
        {
            IsDiscovered = true;
        }

        public void Flag()
        {
            if (IsDiscovered == false)
            {
                IsFlag = true;
                return;
            }

            Console.WriteLine("Es kann keine Flagge auf ein bereits aufgedecktes Feld gesetzt werden.");
        }

        public void NoFlag()
        {
            IsFlag = false;
        }


        public string PrintValue()
        {
            if (IsFlag)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                return "<";
            }

            if (!IsDiscovered)
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;
                return "?";
            }

            if (IsBomb)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                //Console.WriteLine(IsBomb);
                return "X";
            }

            var bombsAroundMe = BombsAroundMe.ToString();

            switch (bombsAroundMe)
            {
                case "1":
                    Console.ForegroundColor = ConsoleColor.Green;
                    break;
                case "2":
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    break;
                case "3":
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    break;
                case "4":
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    break;
                case "5":
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    break;
                case "6":
                    Console.ForegroundColor = ConsoleColor.DarkMagenta;
                    break;
                case "7":
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
                case "8":
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    break;


            }


            return bombsAroundMe;


        }

        public PersistenceField ToPersistenceField(int position)
        {

            var field = new PersistenceField()
            {
                IsDiscovered = IsDiscovered,
                IsBomb = IsBomb,
                IsFlag = IsFlag,
                BombsAroundMe = BombsAroundMe,
                Position = position,
                ID = _id
            };

            return field;
        }


  
    }
}