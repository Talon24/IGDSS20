using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavigationManager : MonoBehaviour
{
    public GameManager gameManager;

    private Tile[,] map;

    void Start()
    {
        map = gameManager.map;
    }

    public void register(Tile tile)
    {
        generateMap(tile, new List<Tile>());
    }

    public void unregister(Tile tile)
    {

    }

    private void generateMap(Tile tile, List<Tile> visited)
    {
        tile.navigationDirections[tile] = null;
        visited.Add(tile);
        List<Tile> queue = new List<Tile>();
        foreach (Tile tileNeighbor in tile._neighborTiles)
        {
            queue.Add(tileNeighbor);
        }

        while (queue.Count > 0)
        {
            foreach (Tile newTile in queue)
            {

            }
        }
    }

    // private void generateMapChild(Tile tile, Tile origin, List<Tile> visited)
    // {
    //     visited.Add(tile);
    //     foreach (Tile tileNeighbor in tile._neighborTiles)
    //     {
    //         if (!visited.Contains(tile))
    //         {
    //             generateMapChild(tileNeighbor, tile, visited);
    //         }
    //     }
    // }
}
