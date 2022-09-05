public class Class1
{
    public Class1()
    {
    }
}
public void Zaehler()
{
    Field offset =
    {
        new Field(-1, 1),
        new Field(0, -1),
        new Field(1, -1),
        new Field(-1, 0),
        new Field(1, 0),
        new Field(-1, 1),
        new Field(0, 1),
        new Field(1, 1),
    };

    int MinenZaehler = 0;

    for (int y = 0; y < 20; y++)
    {
        for (int x = 0; x < 20; x++)
        {
            MinenZaehler = 0;

            if (!field[new field(x, y)].isMine)
            {
                int i = 0;
                while (i < 8)
                {
                    if (Field.ContainsKey(new Field(x + offset[i].X, y + offset[i].Y)))
                    {
                        if (Field[new Field(x + offset[i].X, y + offset[i].Y)].isMine)
                        {
                            MinenZaehler++;
                        }
                    }
                    i++;
                }
                if (MinenZaehler == 0)
                {
                    Field[new Field(x, y)].type = 9;
                }
                else
                {
                    Field[new Field(x, y)].type = MinenZaehler;
                }
            }
        }        
        
                 
}
