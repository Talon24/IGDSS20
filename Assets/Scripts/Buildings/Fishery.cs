using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fishery : Building
{
    public Fishery() {
        cost.Add((int)Ressources.Money, 100);
        cost.Add((int)Ressources.Planks, 2);
        upkeep = 40;
        inputRessource = null;
        outputRessource = new Fish();
        outputAmount = 1;
        ProcessingTime = 30;
    }
}
