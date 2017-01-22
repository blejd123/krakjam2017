using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour {

    Dictionary<Coord, Tile> map = new Dictionary<Coord, Tile>();

    public int VisibleTilesX;
    public int VisibleTilesY;

    public GameObject debugFullObject;
    public GameObject debugEmptyObject;

    void Start()
    {
        GenerateBase();
    }

    void Update()
    {
        var center = new Coord(0, 0);
        if (FollowingCamera.Instance != null)
        {
            var cameraPos = FollowingCamera.Instance.transform.position;
            center = Coord.PositionToCoord(cameraPos);
        }

        Generate(new Coord(center.x - VisibleTilesX / 2, center.y - VisibleTilesY / 2),
            new Coord(center.x + VisibleTilesX / 2, center.y + VisibleTilesY / 2));
    }

    void GenerateBase()
    {
        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                map.Add(new Coord(x, y), new Tile(TileType.Empty));
            }
        }
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
        Tile tile = null;
        map.TryGetValue(coord, out tile);

        if(tile == null)
        {
            bool empty = true;// Random.Range(0, 5) != 1;
            tile = new Tile(empty ? TileType.Empty : TileType.Full);
            map.Add(coord, tile);
        }

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
