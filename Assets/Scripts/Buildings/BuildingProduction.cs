using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class BuildingProduction : Building
{
    public int initialCost;
    public int upkeep;
    public Nullable<int> inputRessource;
    public Nullable<int> outputRessource;
    public int outputAmount = 1;
    public int ProcessingTime;
    public bool inProgress;
    public float progress;

    public void Awake(){
    }

    public void Start()
    {
        for (int workplace = 0; workplace < maxWorkers; workplace++)
        {
            Job job = new Job(this, transform.name);
            _jobs.Add(job);
            jobManager.RegisterJob(job);
        }
    }

    public override void Update() {
        base.Update();
        // This needs to be done befeore efficiency==0 check
        ressourceManager.buyAllowNegative((Time.deltaTime / (float)ressourceManager.upkeepInterval) * (float)upkeep);
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
    }

    public void startWorking(){
        if (inputRessource == null || ressourceManager.isAvailable((int)inputRessource, 1))
        {
            if (inputRessource != null) {ressourceManager.Consume((int)inputRessource, 1); }
            inProgress = true;
            progress = 0f;
        }
        else
        {
            return;
        }
    }

    private float averageWorkerHappiness(){
        if (_workers.Count == 0){
            return 0;
        }
        float accu = 0f;
        foreach (Worker worker in _workers)
        {
            accu += worker.happiness;
        }
        return accu / _workers.Count;
    }

    public override void setEfficiency()
    {
        if (SurroundingTile == null)
        {
            efficiency = 1;
            return;
        }
        int found = 0;
        foreach (Tile surroundTile in tile.neighbors)
        {
            if (SurroundingTile.GetComponent<Tile>().TileType == surroundTile.TileType)
            {
                found += 1;
            }
        }
        // Apply efficiency from surrounding tiles
        efficiency = Mathf.Clamp((float)(found - (minSurroundingTiles - 1)) / (maxSurroundingTiles - (minSurroundingTiles - 1)), 0f, 1f);
        // Apply efficiency from number of workers
        efficiency *= (float)_workers.Count / maxWorkers;
        // Apply efficiency from workers happiness
        efficiency *= 0.5f + averageWorkerHappiness() / 2f;

        // Debug.Log(string.Format("Efficiency of building is {0} with {1} found tiles", efficiency, found));
    }
    public void OnDestroy(){
        foreach (Job job in _jobs)
        {
            jobManager.UnregisterJob(job);
        }
    }
}
