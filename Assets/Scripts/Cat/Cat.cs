using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


// C:\acsnfs4.ucsd.edu\CifsHomes\648\q3dong\GitHub

public class Cat : MonoBehaviour
{

    private int moodPoints;
    Animation catAnimation;
    Animator animator;

    public Transform target;

    private int state;

    NavMeshAgent agent;

    private float touchTimer;
    private float timer;
    public ScoreManager scoreManager;

    AudioSource audioSource;

    private int hungryPts;

    CatAI catAI;

    CatEmoji emojiManager;

    public void getTouched()
    {
        // Logic for mood
        if (timer > touchTimer && !catAI.isEating() )
        {
            increaseMood(5);
            timer = 0;
        }
        else {
            // Angry
            decreaseMood(5);
            emojiManager.React("angry", this);
        }

    }

    public void increaseMood(int pts)
    {
        moodPoints += pts;
        if(scoreManager != null)
        scoreManager.addScore(10);
        if (emojiManager != null)
        emojiManager.React(this);
    }

    public void decreaseMood(int pts)
    {
        moodPoints -= pts;
        if (scoreManager != null)
        scoreManager.removeScore(1);
        if(emojiManager!=null)
        emojiManager.React(this);
    }

    public Animator getAnimator() {
        return this.animator;
    }

    public void Fed(int fedPoints) {
        this.increaseMood(fedPoints*5);
        hungryPts -= fedPoints*2;
    }

    public void Tired() {
        moodPoints -= hungryPts;
        if (scoreManager != null)
        scoreManager.removeScore(1);
        hungryPts += 1;
    }

    public bool isHungry() {
        Debug.Log(hungryPts);
        return hungryPts >= 0;
    }

    // Deprecated
    public void walkTo(Transform target) {
        this.target = target;
        agent.SetDestination(this.target.position);
        transform.LookAt(target.position);
        state = State.WALKING;
    }


    public float smoothTime = 15F;
    private Vector3 velocity = Vector3.zero;

    void Update()
    {
        timer += Time.deltaTime;

        // Check mood
        if (moodPoints < 30) {
            Destroy(this.gameObject);
        }
    }


    private void Awake()
    {
        //catAnimation = this.gameObject.GetComponent<Animation>();
        emojiManager = this.gameObject.GetComponent<CatEmoji>();
        animator = this.gameObject.GetComponent<Animator>();
        agent = this.gameObject.GetComponent<NavMeshAgent>();
        catAI = this.gameObject.GetComponent<CatAI>();
        scoreManager = GameObject.Find("ScorePage").GetComponent<ScoreManager>();
        state = State.IDLING;
        //walkTo(target);
        touchTimer = 20f;
        moodPoints = 100;
        hungryPts = 0;

        audioSource = this.gameObject.GetComponent<AudioSource>();
    }



    public static class State {
        public static readonly int IDLING = 0;
        public static readonly int WALKING = 1;
        public static readonly int JUMPING = 2;
        public static readonly int ATTACKING = 3;
    }

    public int getMood() {
        return moodPoints;
    }
}
