using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fishery : Building
{

    public Fishery() {
        initialCost = 100;
        cost.Add(new Plank(), 2);
        upkeep = 40;
        inputRessource = null;
        outputRessource = new Fish();
        outputAmount = 1;
        ProcessingTime = 30;
        build_requirement = null;

    }
}
