using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotatoFarm : Building
{

    public void Start() {
        Name = "PotatoFarm";
        initialCost = 100;
        cost.Add(new Plank(), 2);
        upkeep = 20;
        inputRessource = null;
        outputRessource = new Potato();
        outputAmount = 1;
        ProcessingTime = 30;
        build_requirement = null;

    }
}
