using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Building : MonoBehaviour
{
    public int initialCost;
    public Dictionary<int, int> cost = new Dictionary<int, int>();
    public int upkeep;
    public Ressource inputRessource;
    public Ressource outputRessource;
    public int outputAmount = 1;
    public int ProcessingTime;
    public Transform[] build_requirement;
    public string Name;
    public Tile tile;
}
