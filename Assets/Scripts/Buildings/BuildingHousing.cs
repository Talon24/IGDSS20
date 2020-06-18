using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BuildingHousing : Building
{
    // Start is called before the first frame update
    public Transform Inhabitant;


    // Todo: Refractor this!
    public float fishValue;
    public float clothValue;
    public float schnappsValue;

    public float fishProcessingTime;
    public float clothProcessingTime;
    public float schnappsProcessingTime;
    public float fishProgress;
    public float clothProgress;
    public float schnappsProgress;

    public bool fishInProgress;
    public bool clothInProgress;
    public bool schnappsInProgress;

    public float newInhabitantProgress;
    public float newInhabitantTime;

    // public struct Needs
    // {
    //     public string name;
    //     public float happinessValue;
    //     public float progress;
    //     public float processingTime;
    //     public bool processing;
    //}

    // public void Start()
    // {
    //     // needs = new Needs[Enum.GetNames(typeof(Ressources)).Length];
    //     // Debug.Log(string.Format("Needs: {0}", needs));
    // }


    // Update is called once per frame
    // void Update()
    // {

    // }
    public override void Update() {
        base.Update();

        newInhabitantProgress += (Time.deltaTime / newInhabitantTime * efficiency);
        if (newInhabitantProgress >= 1f) {
            newInhabitantProgress = 0f;
            if (_workers.Count < 10){
                Worker w = Instantiate(Inhabitant, transform.position, transform.rotation, transform).GetComponent<Worker>();
                WorkerAssignedToBuilding(w);
            }
        }

        if (fishInProgress)  {fishProgress += (Time.deltaTime / fishProcessingTime * _workers.Count);} 
        if (clothInProgress) { clothProgress += (Time.deltaTime / clothProcessingTime * _workers.Count); }
        if (schnappsInProgress) { schnappsProgress += (Time.deltaTime / schnappsProcessingTime * _workers.Count); }

        if (fishProgress >= 1f) {fishInProgress = false; fishProgress = 0f;}
        if (clothProgress >= 1f) { clothInProgress = false; clothProgress = 0f; }
        if (schnappsProgress >= 1f) { schnappsInProgress = false; schnappsProgress = 0f; }

        if (!fishInProgress && ressourceManager.isAvailable((int)Ressources.Fish, 1)) 
        {
            ressourceManager.Consume((int)Ressources.Fish, 1);
            fishInProgress = true;
        }
        if (!clothInProgress && ressourceManager.isAvailable((int)Ressources.Cloth, 1))
        {
            ressourceManager.Consume((int)Ressources.Cloth, 1);
            clothInProgress = true;
        }
        if (!schnappsInProgress && ressourceManager.isAvailable((int)Ressources.Schnapps, 1))
        {
            ressourceManager.Consume((int)Ressources.Schnapps, 1);
            schnappsInProgress = true;
        }
        // ressourceManager.buyAllowNegative((Time.deltaTime / ressourceManager.upkeepInterval) * upkeep);
    }

    

    public override void launch()
    {
        for (int i = 0; i <= 1; i++)
        {
        Worker w = Instantiate(Inhabitant, transform.position, transform.rotation, transform).GetComponent<Worker>();
        // _workers.Add(w);
        WorkerAssignedToBuilding(w);
        }
    }

    public override void setEfficiency()
    {
        // Efficiency is analoguous to happiness
        float totalValue = fishValue + clothValue + schnappsValue;
        float happiness = (fishValue * Convert.ToInt32(fishInProgress) / totalValue) +
                           (clothValue * Convert.ToInt32(clothInProgress) / totalValue) +
                           (schnappsValue * Convert.ToInt32(schnappsInProgress) / totalValue);
        efficiency = happiness;


    }
}
