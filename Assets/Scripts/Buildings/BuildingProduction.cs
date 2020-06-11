using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class BuildingProduction : Building
{
    public int initialCost;
    public Dictionary<int, int> cost = new Dictionary<int, int>();
    public int upkeep;
    public Nullable<int> inputRessource;
    public Nullable<int> outputRessource;
    public int outputAmount = 1;
    public int ProcessingTime;
    public Transform[] build_requirement;
    public string Name;
    public Tile tile;
    public int minSurroundingTiles;
    public int maxSurroundingTiles;
    public Transform SurroundingTile;
    public float efficiency;
    public bool inProgress;
    public float progress;
    public RessourceManager ressourceManager;

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
        // Debug.Log(string.Format("Efficiency of building is {0} with {1} found tiles", efficiency, found));
    }

    public void Update() {
        if (!inProgress){
            startWorking();
        }
        else
        {
            if (efficiency == 0) {return;}
            progress += (Time.deltaTime / ProcessingTime * efficiency);
        }
        if (progress >= 1f){
            ressourceManager.put((int)outputRessource, outputAmount);
            progress = 0f;
            inProgress = false;
        }
        ressourceManager.buyAllowNegative((Time.deltaTime / ressourceManager.upkeepInterval) * upkeep);
    }

    public void startWorking(){
        if (inputRessource == null || ressourceManager.isAvailable((int)inputRessource, 1))
        {
            if (inputRessource != null) {ressourceManager.Retrieve((int)inputRessource, 1); }
            inProgress = true;
            progress = 0f;
        }
        else
        {
            return;
        }
    }
}
