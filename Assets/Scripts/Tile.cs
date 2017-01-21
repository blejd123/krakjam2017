using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public enum TileType
{
    Empty,
    Full
}

public class Tile
{
    public bool instantiated = false;
    public TileType type;

    public Tile(TileType type)
    {
        this.type = type;
    }
}
