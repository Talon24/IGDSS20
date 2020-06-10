using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Framework : Building{
    public Framework()
    {
        cost.Add((int)Ressources.Money, 400);
        cost.Add((int)Ressources.Planks, 2);
        upkeep = 50;
        inputRessource = new Wool();
        outputRessource = new Cloth_();
        outputAmount = 1;
        ProcessingTime = 30;
    }
}
