using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Building : MonoBehaviour
{
    public int initialCost;
    public Dictionary<int, int> cost = new Dictionary<int, int>();
    public int upkeep;
    public Ressource inputRessource;
    public Ressource outputRessource;
    public int outputAmount = 1;
    public int ProcessingTime;
    public Transform[] build_requirement;
    public string Name;
    public Tile tile;
    public int minSurroundingTiles;
    public int maxSurroundingTiles;
    public Transform SurroundingTile;
    public float efficiency;

    public void setEfficiency(){
        if (SurroundingTile == null){
            efficiency = 1;
            return;
        }
        int found = 0;
        foreach (Tile surroundTile in tile.neighbors)
        {
            if (SurroundingTile.GetComponent<Tile>().TileType == surroundTile.TileType){
                found += 1;
            }
        }
        efficiency = Mathf.Clamp((float) (found - (minSurroundingTiles - 1)) / (maxSurroundingTiles - (minSurroundingTiles - 1)), 0f, 1f);
        Debug.Log(string.Format("Efficiency of building is {0} with {1} found tiles", efficiency, found));
    }
}
