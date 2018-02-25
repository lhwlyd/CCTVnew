using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmojiSpinner : MonoBehaviour {
    public float rotationPerMinute = 20.0f;
	// Update is called once per frame
	void Update () {
        transform.Rotate(0, 6.0f * rotationPerMinute * Time.deltaTime, 0);
    }
}
