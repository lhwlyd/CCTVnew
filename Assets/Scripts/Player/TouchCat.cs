using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchCat : ITouch {

    Cat cat;

    public void Touch(GameObject obj)
    {
        cat = obj.GetComponent<Cat>();
        cat.getTouched();
    }
}
