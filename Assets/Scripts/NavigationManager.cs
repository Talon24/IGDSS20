using System.Linq;
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
        generateMap(tile);
    }

    public void unregister(Tile tile)
    {
        foreach (Tile mapTile in gameManager.map)
        {
            mapTile.navigationPotentials.Remove(tile);
        }
    }

    private void generateMap(Tile originTile)
    {
        originTile.navigationPotentials[originTile] = 0f;
        List<Tile> visited = new List<Tile>();
        visited.Add(originTile);
        List<Tile> queue = new List<Tile>();
        foreach (Tile tileNeighbor in originTile.neighbors)
        {
            queue.Add(tileNeighbor);
        }

        while (queue.Count > 0)
        {
            List<Tile> newQueue = new List<Tile>();
            foreach (Tile tile in queue)
            {
                visited.Add(tile);
                tile.navigationPotentials[originTile] = tile.distance2D(originTile) + tile.navigationWeight;  // TODO
                foreach (Tile tileNeighbor in tile.neighbors)
                {
                    if (!visited.Contains(tileNeighbor) && !newQueue.Contains(tileNeighbor) && !queue.Contains(tileNeighbor))
                    {
                        newQueue.Add(tileNeighbor);
                    }   
                }
            }
            queue = newQueue;
        }
    }

    public List<Tile> getPath(Tile from, Tile to){
        List<Tile> trace = new List<Tile>();
        trace.Add(from);

        float minPotential = from.neighbors.Select(x => x.navigationPotentials[to]).Min();
        Tile next = from.neighbors.Find(x => x.navigationPotentials[to] == minPotential);
        trace.Add(next);
        while (next != to){
            // If this ever results to just going back and forth it will explode!!!
            IEnumerable<Tile> backwalks = trace.Intersect(next.neighbors);
            List<Tile> candidates = new List<Tile>(next.neighbors);
            candidates.RemoveAll(x => backwalks.Contains(x));
            minPotential = candidates.Select(x => x.navigationPotentials[to]).Min();
            next = next.neighbors.Find(x => x.navigationPotentials[to] == minPotential);
            trace.Add(next);
        }


        // foreach (Tile tile in trace)
        // {
        //     Debug.Log(string.Format("Tile on the way x: {0}, z:", tile.position.x, tile.position.z));
        // }
        return trace;
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
