namespace Minesweeper
{
    public class Field
    {
        public Field(bool isBomb, int bombsAroundMe, Field? top, Field? left, Field? leftBottom, Field? leftTop)
        {
            IsDiscovered = false;
            IsBomb = isBomb;
            BombsAroundMe = bombsAroundMe;
            Top = top;
            Left = left;
            LeftBottom = leftBottom;
            LeftTop = leftTop;
            top?.SetButtom(this);
            top?.SetRight(this);
            top?.SetRightTop(this);
            top?.SetRightBottom(this);
        }

        public Field? Top { get; }
        public Field? Bottom { get; private set; }
        public Field? Left { get; }
        public Field? LeftBottom { get; }
        public Field? LeftTop { get; }
        public Field? Right { get; private set; }
        public Field? RightTop { get; private set; }
        public Field? RightBottom { get; private set; }

        public bool IsBomb { get; }
        public int BombsAroundMe { get; }
        public bool IsDiscovered { get; private set; }
        public bool IsGameOver { get; private set; }
        public bool IsFlag { get; set; }


        private void SetButtom(Field buttomField)
        {
            Bottom = buttomField;
        }

        private void SetRight(Field rightField)
        {
            Right = rightField;
        }

        private void SetRightTop(Field rightTopField)
        {
            RightTop = rightTopField;
        }

        private void SetRightBottom(Field rightBottomField)
        {
            RightBottom = rightBottomField;
        }

        public void Discover()
        {
            if (!IsDiscovered)
            {
                IsDiscovered = true;
            }

            if (IsBomb)
            {
                IsGameOver = true;
            };
        }

        public void NotDiscovered()
        {
            IsDiscovered = false;
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
    }
}