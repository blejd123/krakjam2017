using System.Collections.Generic;

class Coord: IEqualityComparer<Coord>
{
    public int x;
    public int y;

    public Coord(int x, int y)
    {
        this.x = x;
        this.y = y;
    }

    public bool Equals(Coord a, Coord b)
    {
        return a.x == b.x && a.y == b.y;
    }

    public int GetHashCode(Coord coord)
    {
        return coord.x + coord.y * 1000000;
    }
}
