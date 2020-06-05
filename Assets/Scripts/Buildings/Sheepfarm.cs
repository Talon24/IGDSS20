using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sheepfarm : Building
{

    public Sheepfarm()
    {
        initialCost = 100;
        cost.Add(new Plank(), 2);
        upkeep = 20;
        inputRessource = null;
        outputRessource = new Wool();
        outputAmount = 1;
        ProcessingTime = 30;
        build_requirement = null;

    }
}
