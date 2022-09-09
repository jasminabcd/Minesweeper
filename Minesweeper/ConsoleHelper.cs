using System.Text.RegularExpressions;

namespace Minesweeper
{
    internal class ConsoleHelper
    {

        public static void PrintGame(Field[][] field)
        {
            Console.Clear();

            var cols = 1;
            Console.Write("   ");
            foreach (var value in field)
            {
                if (cols < 10 || cols == 1)
                {
                    var colInfoWithNull =(" " + "0" + cols + " ");
                    Console.Write(colInfoWithNull);
                    cols++;
                }
                else
                {
                    var colInfo = " " + cols + " ";
                    Console.Write(colInfo);
                    cols++;
                }
            }

            Console.WriteLine();
            var msg = "  " + string.Join("", Enumerable.Repeat('-', 4 * field[0].Length + 1));
            Console.WriteLine(msg);


            var lineName = 'A';
            foreach (var line in field)
            {
                Console.Write(lineName + " ");
                lineName++;
                foreach (var value in line)
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write("| ");
                    Console.Write(value.PrintValue());
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write(" ");
                }
                Console.Write(" |");
                Console.WriteLine();

            }
        }

        public static int UserInput()
        {
            while (true)
            {
                string userChoice = Console.ReadLine();
                int userChoiceInt;

                bool successed = int.TryParse(userChoice, out userChoiceInt);

                if (!successed)
                {
                    Console.WriteLine("Der von Ihnen eingegebene Wert ist eine ungültige Zahl. Geben Sie eine Zahl von 1-3 ein:");
                    continue;
                }

                if (userChoiceInt > 3 || userChoiceInt < 1)
                {
                    Console.WriteLine("Bitte geben Sie eine Zahl von 1-3 ein:");
                    continue;
                }
                
                return userChoiceInt;
            }
        }
            

        public static int ReadNumber()
        {
            int value = 0;
            string input;
            do
            {
                input = Console.ReadLine();
                if(input == null)
                {
                    Console.WriteLine("Bitte geben Sie etwas ein:");
                   continue;
                }
            } while (!int.TryParse(input, out value));

            return value;
        }



        public static Coordinate ReadCoordinates()
        {
            var formatRegex = new Regex("^(([A-Z][1-9][0-9]?)|([1-9][0-9]?[A-Z]))$");
            var numberRegex = new Regex("[0-9]+");
            var LetterRegex = new Regex("[A-Z]");

            Coordinate validCoordinates = null;

            var isValid = false;
            while (!isValid)
            {
                var input = Console.ReadLine().ToUpper();

                var match = formatRegex.Match(input);

                if (!match.Success)
                {
                    Console.WriteLine("Bitte geben sie eine Zahl und einen Buschtaben ein:");
                    continue;
                }

                var numberMatch = numberRegex.Match(input);
                var numberValue = int.Parse(numberMatch.Value);

                var sideLength = ConstHelper.SideLength;

                if (numberValue > sideLength || numberValue < 0)
                {
                    Console.WriteLine("Der Wert liegt auserhalb des Feld. Bitte geben Sie einen gültigen Wert ein:");
                    continue;
                }

                var LetterMatch = LetterRegex.Match(input);
                var lettervalue = LetterMatch.Value;
                var y = (int)lettervalue[0] - 64;

                if (y > sideLength || y < 0)
                {
                    Console.WriteLine("Der Wert liegt auserhalb des Feld. Bitte geben Sie einen gültigen Wert ein:");
                    continue;
                }

                isValid = true;
                validCoordinates = new Coordinate(numberValue, y);
            }

            return validCoordinates!;
        }

        
    }
}