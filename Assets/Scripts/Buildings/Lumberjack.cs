using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lumberjack : Building
{

    public Lumberjack()
    {
        Name = "Lumberjack";
        cost.Add((int)Ressources.Money, initialCost);
        inputRessource = null;
        outputRessource = (int)Ressources.Wood;

    }
}
