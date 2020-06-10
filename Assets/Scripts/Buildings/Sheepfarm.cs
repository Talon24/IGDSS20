using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sheepfarm : Building
{

    public Sheepfarm()
    {
        Name = "Sheep farm";
        initialCost = 100;
        cost.Add((int)Ressources.Money, 100);
        upkeep = 20;
        inputRessource = null;
        outputRessource = new Wool();
        outputAmount = 1;
        ProcessingTime = 30;

    }
}
