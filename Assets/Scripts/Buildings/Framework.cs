using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Framework : Building{

    public Framework()
    {
        initialCost = 400;
        cost.Add(new Plank(), 2);
        upkeep = 50;
        inputRessource = new Wool();
        outputRessource = new Cloth_();
        outputAmount = 1;
        ProcessingTime = 30;
        build_requirement = null;
    }
}