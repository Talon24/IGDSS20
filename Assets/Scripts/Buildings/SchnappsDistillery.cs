using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SchnappsDistillery : BuildingProduction{

    public SchnappsDistillery()
    {
        Name = "Distillery";
        cost.Add((int)Ressources.Money, initialCost);
        inputRessource = (int)Ressources.Potato;
        outputRessource = (int)Ressources.Schnapps;

    }
}
