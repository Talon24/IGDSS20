using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sheepfarm : BuildingProduction
{

    public Sheepfarm()
    {
        Name = "Sheep farm";
        cost.Add((int)Ressources.Money, initialCost);
        inputRessource = null;
        outputRessource = (int)Ressources.Wool;

    }
}
