using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JobManager : MonoBehaviour
{

    private List<Job> _availableJobs = new List<Job>();
    public List<Worker> _unoccupiedWorkers = new List<Worker>();
    public List<Worker> _nonWorkers = new List<Worker>();



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
            List<Job> freeJobs = _availableJobs.FindAll(x => x.free());  // This might need to be a list that is maintained instead of done every time.
            if (freeJobs.Count > 0) {
                int job_index = Random.Range(0,freeJobs.Count);
                Worker w = _unoccupiedWorkers[0];
                Job j = freeJobs[job_index];
                j.AssignWorker(w);
                _unoccupiedWorkers.RemoveAt(0);
            }
        }
    }

    public int PopulationNumber(){
        int result = 0;
        result += _availableJobs.FindAll(x => !x.free()).Count;
        result += _unoccupiedWorkers.Count;
        return result;
    }

    public int NonWorkerNumber(){
        return _nonWorkers.Count;
    }

    public int freeWorkers(){
        return _unoccupiedWorkers.Count;
    }


    public void RegisterWorker(Worker w)
    {
        _unoccupiedWorkers.Add(w);
    }



    public void RemoveWorker(Worker w)
    {
        _unoccupiedWorkers.Remove(w);
        Job workersJob = _availableJobs.Find(job => job._worker == w);
        if (workersJob != null){
            workersJob.RemoveWorker();
        }
        
    }

    #endregion

    public void RegisterJob(Job j)
    {
        _availableJobs.Add(j);
    }

    public void UnregisterJob(Job j)
    {
        if (!j.free()) {
            _unoccupiedWorkers.Add(j._worker);
            Worker w = j._worker;
            w.SendHome();
        }
        j.RemoveWorker();
        _availableJobs.Remove(j);
    }

    public Job getJob(Worker w){
        return _availableJobs.Find(x => x._worker == w);
    }

    public void addNonWorker(Worker w){
        if (!_nonWorkers.Contains(w)){_nonWorkers.Add(w); }
    }

    public void removeNonWorker(Worker w)
    {
        if (_nonWorkers.Contains(w)) { _nonWorkers.Remove(w); }
    }

}
