using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotatoFarm : Building
{
    public PotatoFarm()
    {
        Name = "PotatoFarm";
        upkeep = 10;
        cost.Add((int)Ressources.Money, 100);
        cost.Add((int)Ressources.Planks, 2);
        outputRessource = new Potato();
        outputAmount = 1;
        ProcessingTime = 30;

    }
}
