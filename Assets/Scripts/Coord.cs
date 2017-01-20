using System.Collections.Generic;
using UnityEngine;

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

    public Vector3 position
    {
        get
        {
            const float yDiff = (578.0f - 322.0f)/ 128.0f;
            const float width = 578.0f / 128.0f / 2.0f;
            return new Vector3(x * width + y * yDiff, y, 0);
        }
    }
}
