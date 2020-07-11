using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fishery : BuildingProduction
{
    public Fishery() {
        cost.Add((int)Ressources.Money, initialCost);
        cost.Add((int)Ressources.Planks, 2);
        inputRessource = null;
        outputRessource = (int)Ressources.Fish;
        Name = "Fishery";
    }
}
