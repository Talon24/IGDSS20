using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lumberjack : Building
{
    public ResourceManager R;

    //public Lumberjack()
    //    initialCost = 100;
    //   upkeep = 10;
    //    inputRessource = null;
    //    outputRessource = new Wood();
    //    outputAmount = 1;
    //    ProcessingTime = 15;
    //    build_requirement = null;

    private void Start()
    {
        R.updateUpkeep(-10);
        R.updateWood(1);
    }
}
