using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

enum TileType
{
    Empty,
    Full
}

class Tile
{

    public TileType type;

    public Tile(TileType type)
    {
        this.type = type;
    }
}
