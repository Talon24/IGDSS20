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
    public string Name;
    public bool inProgress;
    public float progress;

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
