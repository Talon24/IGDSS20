using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingHousing : Building
{
    // Start is called before the first frame update
    public Transform Inhabitant;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void launch()
    {
        for (int i = 0; i <= 1; i++)
        {
        Worker w = Instantiate(Inhabitant, new Vector3(0,0,0), new Quaternion(0,0,0,0), transform).GetComponent<Worker>();
        // _workers.Add(w);
        WorkerAssignedToBuilding(w);
        }
    }
}
