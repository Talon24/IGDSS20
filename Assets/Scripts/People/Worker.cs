using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Worker : MonoBehaviour
{
    #region Manager References
    JobManager _jobManager; //Reference to the JobManager
    GameManager _gameManager;//Reference to the GameManager
    RessourceManager _ressourceManager;
    #endregion

    public float _age; // The age of this worker
    public float happiness; // The happiness of this worker


    public float fishValue;
    public float clothValue;
    public float schnappsValue;

    public float fishProcessingTime;
    public float clothProcessingTime;
    public float schnappsProcessingTime;
    public float fishProgress;
    public float clothProgress;
    public float schnappsProgress;

    public bool fishInProgress;
    public bool clothInProgress;
    public bool schnappsInProgress;

    // Start is called before the first frame update
    void Start()
    {
        // GameObject worky = Instantiate(Worker());
        _jobManager = FindObjectOfType<JobManager>();
        _gameManager = FindObjectOfType<GameManager>();
        _ressourceManager = FindObjectOfType<RessourceManager>();
    }

    // Update is called once per frame
    void Update()
    {
        Age();
        if (fishInProgress) { fishProgress += (Time.deltaTime / fishProcessingTime); }
        if (clothInProgress) { clothProgress += (Time.deltaTime / clothProcessingTime); }
        if (schnappsInProgress) { schnappsProgress += (Time.deltaTime / schnappsProcessingTime); }

        if (fishProgress >= 1f) { fishInProgress = false; fishProgress = 0f; }
        if (clothProgress >= 1f) { clothInProgress = false; clothProgress = 0f; }
        if (schnappsProgress >= 1f) { schnappsInProgress = false; schnappsProgress = 0f; }

        if (!fishInProgress && _ressourceManager.isAvailable((int)Ressources.Fish, 1))
        {
            _ressourceManager.Consume((int)Ressources.Fish, 1);
            fishInProgress = true;
        }
        if (!clothInProgress && _ressourceManager.isAvailable((int)Ressources.Cloth, 1))
        {
            _ressourceManager.Consume((int)Ressources.Cloth, 1);
            clothInProgress = true;
        }
        if (!schnappsInProgress && _ressourceManager.isAvailable((int)Ressources.Schnapps, 1))
        {
            _ressourceManager.Consume((int)Ressources.Schnapps, 1);
            schnappsInProgress = true;
        }



        float totalValue = fishValue + clothValue + schnappsValue;
        float _happiness = (fishValue * Convert.ToInt32(fishInProgress) / totalValue) +
                           (clothValue * Convert.ToInt32(clothInProgress) / totalValue) +
                           (schnappsValue * Convert.ToInt32(schnappsInProgress) / totalValue);
        happiness = _happiness;
    }


    private void Age()
    {
        //TODO: Implement a life cycle, where a Worker ages by 1 year every 15 real seconds.
        //When becoming of age, the worker enters the job market, and leaves it when retiring.
        //Eventually, the worker dies and leaves an empty space in his home. His Job occupation is also freed up.

        if (_age > 14)
        {
            BecomeOfAge();
        }

        if (_age > 64)
        {
            Retire();
        }

        if (_age > 100)
        {
            Die();
        }
    }


    public void BecomeOfAge()
    {
        _jobManager.RegisterWorker(this);
    }

    private void Retire()
    {
        _jobManager.RemoveWorker(this);
    }

    private void Die()
    {
        Destroy(this.gameObject, 1f);
    }

}
