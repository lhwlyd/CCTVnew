using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {

    private int score;

    public Text scoreText;
    public Transform catSpawnPoint;
    public GameObject catPrefab;
    public GameObject demon;

    public void addScore(int score) {
        this.score += score;

        if (score > 200) {

            score = 100;
            GameObject newCat = Instantiate( catPrefab, catSpawnPoint.transform.position, Quaternion.identity) as GameObject;
        }
        scoreText.text = "" + this.score;

    }

    public void removeScore(int score)
    {
        this.score -= score;

        if (score < 0) {
            GameObject newDemon = Instantiate(demon, catSpawnPoint.transform.position, Quaternion.identity) as GameObject;

        }
        scoreText.text = "" + this.score;
    }

    public int getScore() {
        return score;
    }

    private void Start()
    {
        score = 100;
    }
}
