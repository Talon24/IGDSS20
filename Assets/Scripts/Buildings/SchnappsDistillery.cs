using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SchnappsDistillery : Building{

    public SchnappsDistillery()
    {
        initialCost = 100;
        cost.Add(new Plank(), 2);
        upkeep = 40;
        inputRessource = new Potato();
        outputRessource = new Schnapps();
        outputAmount = 1;
        ProcessingTime = 30;
        build_requirement = null;

    }
}
