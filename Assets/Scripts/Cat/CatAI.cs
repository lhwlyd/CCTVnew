using UnityEngine;
using System.Collections;
using UnityEngine.AI;
using System;

// Source: https://forum.unity.com/threads/solved-random-wander-ai-using-navmesh.327950/
public class CatAI : MonoBehaviour
{

    public float wanderRadius;
    public float wanderTimer;

    private float stopTimer;

    private Transform target;
    private NavMeshAgent agent;
    private float timer;
    private bool rest;

    private Cat cat;
    private Animator catAnimator;
    private int state;

    private bool grounded;
    private bool eating;
    private float eatingTimer;
    public float eatingTimeLength;

    public LayerMask layerToSearch;

    bool protect;

    // Use this for initialization
    void OnEnable()
    {
        agent = GetComponent<NavMeshAgent>();
        timer = wanderTimer;
        cat = this.gameObject.GetComponent<Cat>();
        catAnimator = this.gameObject.GetComponent<Animator>();
        state = State.idling;
        NewTarget();

    }


    void NewTarget() {
        switch ( state ) {
            case 0: // idling
                catAnimator.SetTrigger("StartWalking");
                break;

            case 2: // sleeping
                catAnimator.SetTrigger("StartWalking");
                break;

            case 1: // walking
                break;

            case 3: // eating
                catAnimator.SetTrigger("StartWalking");
                break;
            default:
                catAnimator.SetTrigger("StartWalking");
                break;

        }

        


        state = State.walking;
        // Change target
        Vector3 newPos = RandomNavSphere(transform.position, wanderRadius, -1);
        if (cat.isHungry())
        {
            // Radar scan for the closest food
            newPos = scanForFood();
        }

        agent.SetDestination(newPos);
        rest = false;
        timer = 0;
        stopTimer = UnityEngine.Random.Range(wanderTimer/2, wanderTimer);

        if (!protect)
        {
            protect = true;
        }
        else {
            cat.Tired();
        }
    }

    private Vector3 scanForFood() {
        Collider[] colliders = Physics.OverlapBox(transform.position, new Vector3(2000f, 2000f, 2000f), Quaternion.identity, layerToSearch);
        Debug.Log("colliders length: " + colliders.Length);
        float shortestDis = 9999f;
        Collider bestFood = null;
        foreach ( Collider collider in colliders ) {
            float dis = Vector3.Distance(collider.transform.position, this.transform.position);
            if (dis < shortestDis) {
                shortestDis = dis;
                bestFood = collider;
            }
        }

        if (bestFood == null) {
            Debug.Log("food is null" );
            cat.decreaseMood(5);
            // sad because no food available
            return this.transform.position;
        }

        Debug.Log(" find food! ");
        return bestFood.transform.position;
    }





    // Update is called once per frame
    void Update()
    {
        if (eating) {
            eatingTimer += Time.deltaTime;
            // Wait till it finishes eating
            if (eatingTimer < eatingTimeLength)
            {
                if (!catAnimator.GetBool("Eating"))
                {
                    catAnimator.SetBool("Eating", true);
                }
                return;
            }
            else {
                eating = false;
                catAnimator.SetBool("Eating", false);
                catAnimator.SetTrigger("BeHumble");
            }

        }

        if (!rest)
        {
            timer += Time.deltaTime;

            if ( Vector3.Distance(this.transform.position, agent.destination) < 0.125f || timer > wanderTimer)
            {
                // Should do encapsulation
                stopMoving();
            }
            else
            {
                // Moving
                Collider[] foodList = Physics.OverlapBox(transform.position, new Vector3(0.25f, 0.25f, 0.25f), Quaternion.identity, layerToSearch);
                if ( foodList.Length > 0 && cat.isHungry() ) {
                    // Move onto a food and is hungry...eat it
                    Debug.Log("in if");
                    eating = true;
                    eatingTimer = 0f;
                    CatFood food = foodList[0].GetComponentInParent<CatFood>();
                    if(food!= null)
                    food.getEaten(this.cat);
                    agent.SetDestination(this.transform.position);
                    return;
                }


            }
        }
        else {
            // Rest
            timer += Time.deltaTime;

            if (timer >= stopTimer)
            {
                NewTarget();
            }
            else
            {
            }
        }


    }

    private void stopMoving() {
        Debug.Log("stopped");
        timer = 0;
        rest = true;
        agent.SetDestination(this.transform.position); // So that the cat won't float
        int random = UnityEngine.Random.Range(0, 3);
        switch (random) {
            case 0:
                catAnimator.SetTrigger("StopWalking");
                state = State.idling;
                break;

            case 1:
                catAnimator.SetTrigger("BeHumble");
                state = State.idling;
                break;
            case 2:
                catAnimator.SetTrigger("Sleep");
                state = State.sleeping;
                break;
        }
    }

    public static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
    {

        Vector3 randDirection = UnityEngine.Random.insideUnitSphere * dist;

        randDirection += origin;

        NavMeshHit navHit;

        NavMesh.SamplePosition(randDirection, out navHit, dist, layermask);

        
        return navHit.position;
    }

    public void goToPlayer(Transform target) {
        NewTarget();
        this.agent.SetDestination(target.position);
    }

    public bool isEating() {
        return eating;
    }

    class State {
        public static readonly int walking = 1;
        public static readonly int sleeping = 2;
        public static readonly int eating = 3;
        public static readonly int idling = 0;


    }
}
