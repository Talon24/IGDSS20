using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Building : MonoBehaviour
{
    #region Manager References
    public JobManager jobManager; //Reference to the JobManager
    #endregion
    
    #region Workers
    public List<Worker> _workers; //List of all workers associated with this building, either for work or living
    #endregion

    #region Jobs
    public List<Job> _jobs; // List of all available Jobs. Is populated in Start()
    public Transform[] build_requirement;
    public float efficiency;
    public Transform SurroundingTile;
    public Tile tile;
    public int minSurroundingTiles;
    public int maxSurroundingTiles;
    public Dictionary<int, int> cost = new Dictionary<int, int>();
    public RessourceManager ressourceManager;
    #endregion
    

    #region Methods   
    public void WorkerAssignedToBuilding(Worker w)
    {
        _workers.Add(w);
    }

    public void WorkerRemovedFromBuilding(Worker w)
    {
        _workers.Remove(w);
    }
    #endregion

    public virtual void launch()
    {

    }

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
}
