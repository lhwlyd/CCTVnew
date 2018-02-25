using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatFoodPosition : MonoBehaviour {

    private void Awake()
    {
        this.transform.SetPositionAndRotation(new Vector3(this.transform.parent.position.x, 0, this.transform.parent.position.z), Quaternion.identity );
    }
}
