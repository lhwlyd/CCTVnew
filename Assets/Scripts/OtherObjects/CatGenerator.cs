using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatGenerator : MonoBehaviour {
    float timer;
    public GameObject[] catArray;
    ScoreManager scoreManager;

	// Use this for initialization
	void Start () {
        scoreManager = GameObject.Find("ScorePage").GetComponent<ScoreManager>();
	}
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;

        if (timer > 20f && scoreManager.getScore() > 50 ) {
            timer = 0;
            GameObject cat = Instantiate(catArray[Random.Range(0, catArray.Length)], this.transform.position, Quaternion.identity) as GameObject;
        }
	}
}
