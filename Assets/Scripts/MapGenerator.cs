using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour {

    Dictionary<Coord, Tile> map = new Dictionary<Coord, Tile>();
    List<GameObject> objects = new List<GameObject>();

    public GameObject debugFullObject;
    public GameObject debugEmptyObject;

    void Start()
    {
        Regenerate();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.G))
        {
            Regenerate();
        }
    }

    void Regenerate()
    {
        ClearAll();

        GenerateBase();
        Generate(new Coord(-10, -10), new Coord(10, 10));
    }

    void ClearAll()
    {
        map.Clear();

        foreach(GameObject obj in objects)
        {
            GameObject.Destroy(obj);
        }

        objects.Clear();
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
        if(map.ContainsKey(coord))
        {
            return;
        }

        bool empty = Random.Range(0, 5) != 1;
        var tile = new Tile(empty ? TileType.Empty : TileType.Full);
        map.Add(coord, tile);

        var prefab = tile.type == TileType.Full ? debugFullObject : debugEmptyObject;
        objects.Add(GameObject.Instantiate(prefab, coord.position, Quaternion.identity));
    }
	
}
