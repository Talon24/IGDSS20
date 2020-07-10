using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BuildingHousing : Building
{
    // Start is called before the first frame update
    public Transform Inhabitant;

    public float newInhabitantProgress;
    public float newInhabitantTime;
    public float taxAmount;


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

        if (_workers.Count == 0){  // if everyone died, repopulate house
            Worker w = Instantiate(Inhabitant, transform.position, transform.rotation, transform).GetComponent<Worker>();
            w.BecomeOfAge();
            WorkerAssignedToBuilding(w);
        }

        ressourceManager.put((int)Ressources.Money, (efficiency * taxAmount * _workers.Count * Time.deltaTime) / ressourceManager.upkeepInterval);

        newInhabitantProgress += (Time.deltaTime / newInhabitantTime * efficiency);
        if (newInhabitantProgress >= 1f) {
            newInhabitantProgress = 0f;
            if (_workers.Count < 10){
                Worker w = Instantiate(Inhabitant, transform.position, transform.rotation, transform).GetComponent<Worker>();
                WorkerAssignedToBuilding(w);
            }
        }

    }

    

    public override void launch()
    {
        for (int i = 0; i <= 1; i++)
        {
            Worker w = Instantiate(Inhabitant, transform.position, transform.rotation, transform).GetComponent<Worker>();
            // _workers.Add(w);
            w.BecomeOfAge();
            WorkerAssignedToBuilding(w);
        }
    }

    public override void setEfficiency()
    {
        float accu = 0f;
        foreach (Worker worker in _workers)
        {
            accu += worker.happiness;
        }
        accu = accu / _workers.Count;
        if (_workers.Count == 0){
            accu = 0f;
        }
        efficiency = accu;
    }

    public void OnDestroy(){
        foreach (Worker worker in _workers)
        {
            jobManager.RemoveWorker(worker);
            jobManager.removeNonWorker(worker);
        }
    }
}
