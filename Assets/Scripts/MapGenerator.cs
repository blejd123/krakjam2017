using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour {

    public int VisibleTilesX;
    public int VisibleTilesY;

    public GameObject debugFullObject;
    public GameObject debugEmptyObject;

    void Start()
    {
        Generate(new Coord(VisibleTilesX / 2, VisibleTilesY / 2),
            new Coord(VisibleTilesX / 2, VisibleTilesY / 2));
    }

    void Generate(Coord min, Coord max)
    {
        for(int x=min.x; x<=max.x; x++)
        {
            for(int y=min.y; y<max.y; y++)
            {
                GenerateTile(new Coord(x, y));
            }
        }
    }

    void GenerateTile(Coord coord)
    {
        bool empty = true;// Random.Range(0, 5) != 1;
        Tile tile = new Tile(empty ? TileType.Empty : TileType.Full);

        if(!tile.instantiated)
        {
            var prefab = tile.type == TileType.Full ? debugFullObject : debugEmptyObject;
            var newObject = Lean.LeanPool.Spawn(prefab, coord.position, Quaternion.Euler(0, 0, Random.Range(0, 4) * 90));
            newObject.GetComponent<Despawner>().tile = tile;
            newObject.transform.localScale = new Vector3(Random.Range(0, 2) == 0 ? -1 : 1, Random.Range(0, 2) == 0 ? -1 : 1, 1);
            tile.instantiated = true;
        }
    }
	
}
