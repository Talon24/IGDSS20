using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SchnappsDistillery : Building{

    public SchnappsDistillery()
    {
        Name = "Distillery";
        cost.Add((int)Ressources.Money, initialCost);
        inputRessource = (int)Ressources.Potato;
        outputRessource = (int)Ressources.Schnapps;

    }
}
