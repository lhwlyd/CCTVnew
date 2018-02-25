using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManualBook : MonoBehaviour {
    private bool opened = false;

    public void open() {

        if (!opened)
        {
            // Open the book, animation + ... 
            opened = true;
        }
        else {
            // do nothing since it's already opened
        }
    }

    public void close() {
        if (opened)
        {

            opened = false;
        }
        else {
            // do nothing
        }
    }
}
