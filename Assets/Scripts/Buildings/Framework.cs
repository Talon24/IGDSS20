using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Framework : Building{
    public Framework()
    {
        cost.Add((int)Ressources.Money, initialCost);
        cost.Add((int)Ressources.Planks, 2);
        inputRessource = (int)Ressources.Wool;
        outputRessource = (int)Ressources.Cloth;
    }
}
