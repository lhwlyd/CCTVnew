using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatEmoji : MonoBehaviour {
    public GameObject[] emojiArray; // 0:happy, 1: ok, 2: angry
    GameObject player;

    AudioSource[] audioSources;

    public void React(Cat cat) {
        int moodPts = cat.getMood();

        if (moodPts >= 100) {
            // Happy
            createEmoji(0, cat);
            audioSources[Random.Range(0, audioSources.Length)].Play();
        }

        if (moodPts >= 60 && moodPts < 100) {
            // OK
            createEmoji(1, cat);
            audioSources[Random.Range(0, audioSources.Length)].Play();
        }

        if (moodPts < 60) {
            // Angry
            createEmoji(2, cat);

        }
    }

    public void React( string anger, Cat cat)
    {

        if (anger.Equals("angry"))
        {
            // Angry
            createEmoji(2, cat);
        }
    }

    private void createEmoji(int idx, Cat cat) {
        GameObject emoji = Instantiate(emojiArray[idx], new Vector3(cat.transform.position.x, cat.transform.position.y + 0.5f, cat.transform.position.z), Quaternion.identity);
        player = GameObject.Find("Player");
        emoji.transform.LookAt(player.transform);
        Destroy(emoji, 3f);
    }

    private void Start()
    {
        audioSources = this.gameObject.GetComponents<AudioSource>();
    }

}
