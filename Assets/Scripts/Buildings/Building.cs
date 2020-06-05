using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Building : MonoBehaviour
{
    public int initialCost;
    public Dictionary<Ressource, int> cost = new Dictionary<Ressource, int>();
    public int upkeep;
    public Ressource inputRessource;
    public Ressource outputRessource;
    public int outputAmount = 1;
    public int ProcessingTime;
    public Tile build_requirement;
}
