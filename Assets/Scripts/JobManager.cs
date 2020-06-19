using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JobManager : MonoBehaviour
{

    private List<Job> _availableJobs = new List<Job>();
    public List<Worker> _unoccupiedWorkers = new List<Worker>();



    #region MonoBehaviour
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        HandleUnoccupiedWorkers();
    }
    #endregion


    #region Methods

    private void HandleUnoccupiedWorkers()
    {
        if (_unoccupiedWorkers.Count > 0)
        {

            //TODO: What should be done with unoccupied workers?
            List<Job> freeJobs = _availableJobs.FindAll(x => x._worker == null);  // This might need to be a list that is maintained instead of done every time.
            if (freeJobs.Count > 0) {
                int job_index = Random.Range(0,freeJobs.Count);
                Worker w = _unoccupiedWorkers[0];
                Job j = _availableJobs[job_index];
                j.AssignWorker(w);
                _unoccupiedWorkers.RemoveAt(0);
            }
        }
    }


    public void RegisterWorker(Worker w)
    {
        _unoccupiedWorkers.Add(w);
    }



    public void RemoveWorker(Worker w)
    {
        _unoccupiedWorkers.Remove(w);
        Job workersJob = _availableJobs.Find(job => job._worker == w);
        workersJob.RemoveWorker();
        
    }

    #endregion

    public void RegisterJob(Job j)
    {
        _availableJobs.Add(j);
    }

    public void UnregisterJob(Job j)
    {
        _unoccupiedWorkers.Remove(j._worker);
        j.RemoveWorker();
        _availableJobs.Remove(j);
    }
}
