using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    public float Money;
    public float Upkeep;
    public float Wood;
    public float Planks;
    public float Wool;
    public float Cloth;
    public float Potato;
    public float Schnapps;
    public float Fish;
    public float UWood;
    public float UPlanks;
    public float UWool;
    public float UCloth;
    public float UPotato;
    public float USchnapps;
    public float UFish;
    private float waitTime = 1.0f;
    private float timer = 0.0f;
    public float eff;
    // Start is called before the first frame update
    void Start()
    {
        Money = 100;
        Upkeep = 0;
        Wood = 0;
        Wool = 0;
        Planks = 0;
        Cloth = 0;
        Potato = 0;
        Schnapps = 0;
        Fish = 0;
        UWood = 0;
        UWool = 0;
        UPlanks = 0;
        UCloth = 0;
        UPotato = 0;
        USchnapps = 0;
        UFish = 0;
        eff = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > waitTime)
        {
            updateResources();
            timer -= waitTime;
        }
    }

    void updateResources()
    {
        Money += (100 + Upkeep);
        Wool -= (2 * UWool);
        Wood += (4 * UWood);
        Planks += (4 * UPlanks);
        Cloth += (2 * UCloth);
        Potato += (2 * UPotato);
        Schnapps += (2 * USchnapps);
        Fish += (2 * UFish);
    }

    public void updateUpkeep(float value)
    {
        Upkeep += value;
    }

    public void updateWood(float value)
    {
        UWood += (eff * value);
    }

    public void updateWool(float value)
    {
        UWool += (eff * value);
    }

    public void updatePlanks(float value)
    {
        UPlanks += value;
    }

    public void updateCloth(float value)
    {
        UCloth += value;
    }

    public void updatePotato(float value)
    {
        UPotato += (eff * value);
    }

    public void updateSchnapps(float value)
    {
        USchnapps += value;
    }

    public void updateFish(float value)
    {
        UFish += (eff * value);
    }
}
