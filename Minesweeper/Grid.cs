using Minesweeper;

public class Grid
{

    private Field[][] _playingField;
    private List<Coordinate> _bombList;
    public int TotalFields { get; }
    public int TotalBombs { get; }
    public int SideLength { get; set; }

    private int _discoveredFieldCount = 0;
    private int _flagFieldCount = 0;

    public Grid(ICollection<PersistenceField> persistenceFields)
    {
        var ordered = persistenceFields.OrderBy(f => f.Position)
            .ToList();
        SideLength = (int)Math.Sqrt(persistenceFields.Count);
        TotalFields = persistenceFields.Count;
        TotalBombs = (int)(persistenceFields.Count * 0.16);

        _playingField = new Field[SideLength][];
        for (int i = 0; i < SideLength; i++)
        {
            _playingField[i] = new Field[SideLength];

            for (var j = 0; j < SideLength; j++)
            {
                var persistenceField = ordered[i * SideLength + j];
                Field field = new Field(persistenceField);
                _playingField[i][j] = field;

            }
        }
    }

    public Grid(int sideLength)
    {
        SideLength = sideLength;
        _bombList = new List<Coordinate>();
        TotalFields = sideLength * sideLength;
        TotalBombs = (int)Math.Round(TotalFields * ConstHelper.BombChance, 0);

        do
        {
            var bombX = Random.Shared.Next(1, TotalFields + 1);
            var bombY = 0;

            while (bombX >= sideLength)
            {
                bombX -= sideLength;
                bombY++;
            }

            var newCoordinate = new Coordinate(bombX, bombY);
            if (!_bombList.Contains(newCoordinate))
            {
                _bombList.Add(newCoordinate);
            }

        } while (_bombList.Count != TotalBombs);

        var field = new Field[sideLength][];
        for (int y = 0; y < field.Length; y++)
        {
            field[y] = new Field[sideLength];
            for (int x = 0; x < field[y].Length; x++)
            {
                int bombsAroundMe = 0;
                var isBomb = false;

                if (_bombList.Contains(new Coordinate(x, y)))
                {

                    isBomb = true;
                }

                else
                {

                    var fieldRight = new Coordinate(x + 1, y);//Rechts
                    var fieldLeft = new Coordinate(x - 1, y);//Links
                    var fieldOver = new Coordinate(x, y - 1); // Oben
                    var fieldUnder = new Coordinate(x, y + 1);//Unten
                    var fieldRightOver = new Coordinate(x + 1, y - 1);//Oben Rechts
                    var fieldRightUnder = new Coordinate(x + 1, y + 1);//Unten Rechts
                    var fieldLeftOver = new Coordinate(x - 1, y - 1);//Oben Links
                    var fieldLeftUnder = new Coordinate(x - 1, y + 1);//Unten Links

                    //Rechts
                    if (_bombList.Contains(fieldRight))
                    {
                        bombsAroundMe++;
                    }
                    // Links
                    if (_bombList.Contains(fieldLeft))
                    {
                        bombsAroundMe++;
                    }
                    // Oben
                    if (_bombList.Contains(fieldOver))
                    {
                        bombsAroundMe++;
                    }
                    //Unten
                    if (_bombList.Contains(fieldUnder))
                    {
                        bombsAroundMe++;
                    }
                    //Oben Rechts
                    if (_bombList.Contains(fieldRightOver))
                    {
                        bombsAroundMe++;
                    }
                    if (_bombList.Contains(fieldRightUnder))
                    {
                        bombsAroundMe++;
                    }
                    //Oben Links
                    if (_bombList.Contains(fieldLeftOver))
                    {
                        bombsAroundMe++;
                    }
                    //Unten Links
                    if (_bombList.Contains(fieldLeftUnder))
                    {
                        bombsAroundMe++;
                    }
                }

                field[y][x] = new Field(isBomb, bombsAroundMe);

            }

        }

        _playingField = field;

    }

    internal void PrintGrid()
    {
        ConsoleHelper.PrintGame(_playingField);
    }

    public Field GetField(Coordinate coordinate)
    {


        var row = _playingField[coordinate.Y - 1];
        var field = row[coordinate.X - 1];

        return field;
    }
    public bool DiscoverFieldAndCheckGameOver(Coordinate coordinate)
    {
        var userField = GetField(coordinate);
        if (userField.IsDiscovered)
        {
            return false;
        }
        else
        {
            _discoveredFieldCount++;
            userField.Discover();
            if (userField.IsBomb)
            {
                return true;
            }
        }

        if (userField.BombsAroundMe > 0 || userField.IsBomb)
        {
            return false;
        }

        var rightField = new Coordinate(coordinate.X + 1, coordinate.Y);
        if (IsCoordinatesInGrid(rightField))
        {
            DiscoverFieldAndCheckGameOver(rightField);
        }

        var leftField = new Coordinate(coordinate.X - 1, coordinate.Y);
        if (IsCoordinatesInGrid(leftField))
        {
            DiscoverFieldAndCheckGameOver(leftField);
        }

        var leftTopField = new Coordinate(coordinate.X - 1, coordinate.Y - 1);
        if (IsCoordinatesInGrid(leftTopField))
        {
            DiscoverFieldAndCheckGameOver(leftTopField);
        }

        var rightTopField = new Coordinate(coordinate.X + 1, coordinate.Y - 1);
        if (IsCoordinatesInGrid(rightTopField))
        {
            DiscoverFieldAndCheckGameOver(rightTopField);
        }
        var topField = new Coordinate(coordinate.X, coordinate.Y - 1);
        if (IsCoordinatesInGrid(topField))
        {
            DiscoverFieldAndCheckGameOver(topField);
        }
        var rightButtomField = new Coordinate(coordinate.X + 1, coordinate.Y + 1);
        if (IsCoordinatesInGrid(rightButtomField))
        {
            DiscoverFieldAndCheckGameOver(rightButtomField);
        }

        var leftButtomField = new Coordinate(coordinate.X - 1, coordinate.Y + 1);
        if (IsCoordinatesInGrid(leftButtomField))
        {
            DiscoverFieldAndCheckGameOver(leftButtomField);
        }

        var buttomField = new Coordinate(coordinate.X, coordinate.Y + 1);
        if (IsCoordinatesInGrid(buttomField))
        {
            DiscoverFieldAndCheckGameOver(buttomField);
        }

        return false;

    }

    public void FlagField(Coordinate coordinate)
    {
        var userField = GetField(coordinate);
        userField.Flag();
        _flagFieldCount++;
    }

    public void RemoveFlagField(Coordinate inputData)
    {
        var userField = GetField(inputData);
        userField.NoFlag();
        _flagFieldCount--;
    }

    public bool IsWon()
    {
        return TotalFields - TotalBombs == _discoveredFieldCount && _flagFieldCount == TotalBombs;
    }

    public bool IsCoordinatesInGrid(Coordinate coordinates)
    {

        if (coordinates.X > SideLength || coordinates.X <= 0)
        {
            return false;
        }

        if (coordinates.Y > SideLength || coordinates.Y <= 0)
        {
            return false;
        }

        return true;
    }

    internal ICollection<PersistenceField> GetPersistenceField()

    {
        var fields = new List<PersistenceField>();

        for (int i = 0; i < _playingField.Length * _playingField.Length; i++)
        {
            int row = i / _playingField.Length;
            int col = i % _playingField.Length;
            var field = _playingField[row][col];
            var persistenceField = field.ToPersistenceField(i);

            fields.Add(persistenceField);

        }
        return fields;
    }

    internal static Grid FromFields(ICollection<PersistenceField> fields)
    {
        return new Grid(fields);
    }
}

