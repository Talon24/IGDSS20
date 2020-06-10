using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SchnappsDistillery : Building{

    public SchnappsDistillery()
    {
        Name = "Distillery";
        initialCost = 100;
        cost.Add((int)Ressources.Money, 100);
        upkeep = 40;
        inputRessource = new Potato();
        outputRessource = new Schnapps();
        outputAmount = 1;
        ProcessingTime = 30;

    }
}
