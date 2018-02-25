using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodGenerator : MonoBehaviour {

    public Transform generator;
    public ScoreManager scoreManager;
    public GameObject[] foodArray;
    AudioSource audioSource;

    public void buyFood() {
        for (int i=0; i<1; i++) {
            int num = Random.Range(0, foodArray.Length);
            GameObject tempFood = Instantiate(foodArray[num], generator.position, Quaternion.identity) as GameObject;
        }
        scoreManager.removeScore(5);
        audioSource.Play();
    }

    private void Start()
    {
        audioSource = this.gameObject.GetComponent<AudioSource>();
    }
}
