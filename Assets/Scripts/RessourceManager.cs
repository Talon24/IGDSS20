using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RessourceManager : MonoBehaviour
{
    // public GameManager manager;
    // Start is called before the first frame update
    // public int Money;
    // public int Planks;
    // public int Cloth;
    // public int Schnapps;
    
    
    // public int Wood;
    // public int Wool;
    // public int Potato;

    // public int[] Stockpile;

    public int upkeepInterval = 60;

    [Serializable]
    public struct Item
    {
        public string name;
        public float amount;
    }
    public Item[] Stockpile;

    void Start()
    {
        Stockpile = new Item[Enum.GetNames(typeof(Ressources)).Length];
        Stockpile[(int)Ressources.Money].amount = 10000;
        Stockpile[(int)Ressources.Planks].amount = 4;


        Array values = Enum.GetValues(typeof(Ressources));
        foreach (Ressources val in values)
        {
            Stockpile[(int)val].name = val.ToString();
        }
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void buyBuilding(Building building)
    {
        Dictionary<int, int> cost = building.cost;
        // prettyprint(cost);
        foreach (KeyValuePair<int, int> kv in cost)
        {
            Stockpile[kv.Key].amount -= kv.Value;
        }
    }
    public bool canAfford(Building building){
        Dictionary<int, int> cost = building.cost;
        // prettyprint(cost);
        foreach (KeyValuePair<int, int> kv in cost)
        {
            if (Stockpile[kv.Key].amount < kv.Value){
                return false;
            }
        }
        return true;
    }

    public void showStockpile(){
        Array values = Enum.GetValues(typeof(Ressources));
        foreach (Ressources val in values)
        {
            Debug.Log(string.Format("{0} of {1}", (int)val, val));
        }
    }

    private void prettyprint(Dictionary<int, int> dictionary){
        foreach (KeyValuePair<int, int> kvp in dictionary)
        {
            Debug.Log(String.Format("Key = {0}, Value = {1}", kvp.Key, kvp.Value));
        }
        Debug.Log(string.Format("Finished writing dict"));
    }

    public bool isAvailable(int key, int amount){
        return (Stockpile[key].amount >= amount);
    }
    public bool Consume(int key, int amount)
    {
        bool available = isAvailable(key, amount);
        if (available)
        {
            Stockpile[key].amount -= amount;
            if (Stockpile[key].amount < 0) {
                throw new Exception("Negative stockpile!!! Race condition probably!");
            }
        }
        return available;
    }

    public void put(int key, int amount){
        Stockpile[key].amount += amount;
    }
    
    public void buyAllowNegative(float amount){
        Stockpile[(int)Ressources.Money].amount -= amount;
    }
}
