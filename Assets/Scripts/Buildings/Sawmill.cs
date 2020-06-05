using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sawmill : Building
{

    public Sawmill()
    {
        initialCost = 100;
        upkeep = 10;
        inputRessource = new Wood();
        outputRessource = new Plank();
        outputAmount = 2;
        ProcessingTime = 15;
        build_requirement = null;

    }
}
