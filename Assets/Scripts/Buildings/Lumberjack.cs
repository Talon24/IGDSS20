using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lumberjack : Building
{

    public Lumberjack()
    {
        Name = "Lumberjack";
        cost.Add((int)Ressources.Money, 100);
        upkeep = 10;
        inputRessource = null;
        outputRessource = new Wood();
        outputAmount = 1;
        ProcessingTime = 15;

    }
}
