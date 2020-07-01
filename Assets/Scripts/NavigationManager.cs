using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavigationManager : MonoBehaviour
{
    public GameManager gameManager;

    private Tile[,] map;


    private struct PathStep
    {
        public Tile tile;
        public int weight;
    }

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
        List<PathStep> queue = new List<PathStep>();
        foreach (Tile tileNeighbor in tile.neighbors)
        {
            PathStep step = new PathStep();
            step.tile = tileNeighbor;
            step.weight = 0;
            queue.Add(step);
        }

        while (queue.Count > 0)
        {
            List<PathStep> newQueue = new List<PathStep>();
            foreach (PathStep step in queue)
            {
                Tile newTile = step.tile;
                foreach (Tile tileNeighbor in newTile.neighbors)
                {
                    if (!visited.Contains(tileNeighbor))
                    {
                        PathStep newStep = new PathStep();
                        newStep.tile = tileNeighbor;
                        newStep.weight = step.weight + tile.navigationWeight;
                        newQueue.Add(newStep);
                    }   
                }
            }
            queue = newQueue;
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
