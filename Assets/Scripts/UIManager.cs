using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text Money;
    public Text Population;
    public Text Fish;
    public Text Planks;
    public Text Wood;
    public Text Wool;
    public Text Cloth;
    public Text Potato;
    public Text Schnapps;
    public RessourceManager ressourceManager;
    public JobManager jobManager;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        string discreteItem = "{0:,0}";
        Money.text = string.Format("{0:#,##0.00}",ressourceManager.get((int)Ressources.Money));
        Fish.text = string.Format(discreteItem, ressourceManager.get((int)Ressources.Fish));
        Planks.text = string.Format(discreteItem, ressourceManager.get((int)Ressources.Planks));
        Wood.text = string.Format(discreteItem, ressourceManager.get((int)Ressources.Wood));
        Wool.text = string.Format(discreteItem, ressourceManager.get((int)Ressources.Wool));
        Cloth.text = string.Format(discreteItem, ressourceManager.get((int)Ressources.Cloth));
        Potato.text = string.Format(discreteItem, ressourceManager.get((int)Ressources.Potato));
        Schnapps.text = string.Format(discreteItem, ressourceManager.get((int)Ressources.Schnapps));
        Population.text = string.Format("{0:,0}+{1:,0} ({2:,0} free)", jobManager.PopulationNumber(), 
                                        jobManager.NonWorkerNumber(), jobManager.freeWorkers());
    }
}
