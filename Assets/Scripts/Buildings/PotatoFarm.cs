using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotatoFarm : BuildingProduction
{
    public PotatoFarm()
    {
        Name = "PotatoFarm";
        cost.Add((int)Ressources.Money, initialCost);
        cost.Add((int)Ressources.Planks, 2);
        outputRessource = (int)Ressources.Potato;

    }
}
