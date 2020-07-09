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
    NavigationManager _navigationManager;
    #endregion

    public float _age; // The age of this worker
    [SerializeField]
    private enum ages : int {Child, Worker, Retiree};
    private enum Positions : int { Home, Workplace };
    private int ageState;
    private int positionState;
    public int secondsToAge = 15;
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

    [SerializeField]
    float timeAtPlace = 0;
    public float stayAtPlace;
    private float stayAtPlacePreference;
    public float walkProgress;
    public List<Tile> walkQueue = new List<Tile>();

    private Animator animator;

    // Start is called before the first frame update
    void Awake()
    {
        // GameObject worky = Instantiate(Worker());
        _jobManager = FindObjectOfType<JobManager>();
        _gameManager = FindObjectOfType<GameManager>();
        _ressourceManager = FindObjectOfType<RessourceManager>();
        _navigationManager = FindObjectOfType<NavigationManager>();
        _age = 0;
        ageState = (int)ages.Child;
        _jobManager.addNonWorker(this);
        positionState = (int)Positions.Home;
        animator = GetComponent<Animator>();
        animator.SetBool("isWalking", false);
        stayAtPlacePreference = UnityEngine.Random.Range(-1f, 1f);
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

        commute();
    }

    void FixedUpdate(){
        if (walkQueue.Count > 1)
        {
            if (walkProgress < 1f){
                float speed = (0.8f / walkQueue[0].navigationWeight);
                animator.SetFloat("speed", speed);
                // speed = speed * 0.8f;  // to match animation and actual
                float diff = Time.deltaTime * speed;
                diff = diff / 2.0f;
                walkProgress += diff;
                // transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y + 10, 0);
                Vector3 relative = walkQueue[1].position_absolute2D() - walkQueue[0].position_absolute2D();
                transform.rotation = Quaternion.LookRotation(relative, Vector3.up);
                transform.Translate(transform.forward * walkQueue[0].realDistance2D(walkQueue[1]) * diff, Space.World);
                Vector3 pos = transform.position;
                pos.y = walkProgress < 0.5 ? walkQueue[0].transform.position.y : walkQueue[1].transform.position.y;
                transform.position = pos;
            } else if (walkProgress >= 1f) {
                transform.position = walkQueue[1].transform.position;

                // transform.rotation = Quaternion.Euler(0, 60, 0);
                // Vector3 relative = walkQueue[0].position_absolute2D() - walkQueue[1].position_absolute2D();
                // transform.rotation = Quaternion.LookRotation(relative, Vector3.up);
                walkQueue.RemoveAt(0);
                walkProgress = 0f;
                // Vector3.RotateTowards(transform.position, walkQueue[1].position_absolute(), 999f, 0f);
            }
        } else if (walkQueue.Count == 1){
            walkQueue.RemoveAt(0);
            timeAtPlace = 0;
            positionState += 1;
            positionState %= 2;
            animator.SetBool("isWalking", false);
        } else {
            timeAtPlace += Time.deltaTime;
        }

        // if (ageState == (int)ages.Worker && walkQueue.Count == 0 && timeAtPlace >= stayAtPlace){
        //     timeAtPlace = 0f;
        //     commute();
        // }
    }

    private void commute()
    {
        Job job = _jobManager.getJob(this);
        if (ageState == (int)ages.Worker && job != null && walkQueue.Count == 0 && timeAtPlace >= stayAtPlace + stayAtPlacePreference){
            List<Tile> route = new List<Tile>();
            if (positionState == (int)Positions.Home){
                route = _navigationManager.getPath(gameObject.GetComponentInParent<Building>().tile, job._building.tile);
            } else if (positionState == (int)Positions.Workplace){
                route = _navigationManager.getPath(job._building.tile, gameObject.GetComponentInParent<Building>().tile);
            }
            walkQueue.AddRange(route);
            animator.SetBool("isWalking", true);
        }
    }

    public void SendHome(){
        walkProgress = 0;
        walkQueue.Clear();
        animator.SetBool("isWalking", false);
        transform.position = gameObject.GetComponentInParent<Building>().transform.position;
        positionState = (int)Positions.Home;
    }


    private void Age()
    {
        //TODO: Implement a life cycle, where a Worker ages by 1 year every 15 real seconds.
        //When becoming of age, the worker enters the job market, and leaves it when retiring.
        //Eventually, the worker dies and leaves an empty space in his home. His Job occupation is also freed up.

        _age += Time.deltaTime / secondsToAge;
        if (ageState == (int)ages.Child && _age > 14)
        {
            BecomeOfAge();
        }
        else if (ageState == (int)ages.Worker && _age > 64)
        {
            Retire();
        }
        else if (ageState == (int)ages.Retiree && _age > 100)
        {
            Die();
        }
        else {
            return;
        }
        // If one of the aging conditions met
        ageState += 1;
    }


    public void BecomeOfAge()
    {
        _jobManager.removeNonWorker(this);
        _jobManager.RegisterWorker(this);
        if (_age < 14){
            // If forced by being generated with building
            _age = 14.0001f;
            ageState = (int)ages.Worker;
        }
    }

    private void Retire()
    {
        _jobManager.RemoveWorker(this);
        _jobManager.addNonWorker(this);
    }

    private void Die()
    {
        this.GetComponentInParent<Building>().WorkerRemovedFromBuilding(this);
        _jobManager.removeNonWorker(this);
        Destroy(this.gameObject, 1f);
    }

}
