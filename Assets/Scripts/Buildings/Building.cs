﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Building : MonoBehaviour
{
    #region Manager References
    public JobManager jobManager; //Reference to the JobManager
    #endregion
    
    #region Workers
    public List<Worker> _workers = new List<Worker>(); //List of all workers associated with this building, either for work or living
    public int maxWorkers;
    #endregion

    #region Jobs
    public List<Job> _jobs = new List<Job>(); // List of all available Jobs. Is populated in Start()
    public Transform[] build_requirement;
    public float efficiency;
    public Transform SurroundingTile;
    public Tile tile;
    public int minSurroundingTiles;
    public int maxSurroundingTiles;
    public Dictionary<int, int> cost = new Dictionary<int, int>();
    public RessourceManager ressourceManager;
    public string Name;
    #endregion
    

    #region Methods  

    public virtual void Update(){
        setEfficiency();
    }

    public void WorkerAssignedToBuilding(Worker w)
    {
        if (_workers.Count < maxWorkers)
        {
            _workers.Add(w);
            // jobManager._unoccupiedWorkers.Add(w);
        }
        else
        {
            // TODO: Maybe throw exception? or return boolean?
        }
    }

    public void WorkerRemovedFromBuilding(Worker w)
    {
        _workers.Remove(w);
    }
    #endregion

    public virtual void launch()
    {

    }
    public abstract void setEfficiency();
}
