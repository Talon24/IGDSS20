using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sawmill : Building
{
    public ResourceManager R;

    //public Sawmill()
    //    initialCost = 100;
    //    upkeep = 10;
    //    inputRessource = new Wood();
    //    outputRessource = new Plank();
    //    outputAmount = 2;
    //    ProcessingTime = 15;
    //    build_requirement = null

    private void Start()
    {
        R.updateUpkeep(-10);
        R.updateWood(-2);
        R.updatePlanks(2);
    }

}
