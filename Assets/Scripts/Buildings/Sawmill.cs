using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sawmill : BuildingProduction
{

    public Sawmill()
    {
        Name = "Sawmill";
        cost.Add((int)Ressources.Money, initialCost);
        inputRessource = (int)Ressources.Wood;
        outputRessource = (int)Ressources.Planks;

    }
}
