using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;
using System.Collections;

public class HandController : MonoBehaviour
{
    private Valve.VR.EVRButtonId gripButton = Valve.VR.EVRButtonId.k_EButton_Grip;
    private Valve.VR.EVRButtonId triggerButton = Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger;

    public SteamVR_Controller.Device controller { get { return SteamVR_Controller.Input((int)trackedObj.index); } }
    public SteamVR_TrackedObject trackedObj;

    public GameObject pickup;
    public bool dragging;
    public LayerMask layer;

    // Use this for initialization
    void Start()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
    }

    // Update is called once per frame
    void Update()
    {
        if (controller == null)
        {
            Debug.Log("Controller not initialized");
            return;
        }

        if (controller.GetPressDown(gripButton) && pickup != null && !dragging)
        {
            if (pickup.GetComponent<Rigidbody>() == null || pickup.name.ToString().Equals("Floor") ) { return; }
            pickup.transform.parent = this.transform;
            pickup.GetComponent<Rigidbody>().useGravity = false;
            dragging = true;
        }
        if (controller.GetPressUp(gripButton) && pickup != null && dragging)
        {
            if (pickup.GetComponent<Rigidbody>() == null || pickup.name.ToString().Equals("Floor")) { return; }
            pickup.transform.parent = null;
            pickup.GetComponent<Rigidbody>().useGravity = true;
            dragging = false;
        }

        if (controller.GetPressDown(triggerButton) && pickup != null ) {
            if (pickup.tag.Equals("Cat") ) {
                Debug.Log("good");
                Cat cat = pickup.GetComponent<Cat>();
                cat.getTouched();
                cat.GetComponent<Animation>().Stop();
            }

            if (pickup.name.Equals("FoodSeller") ) {
                FoodGenerator seller = pickup.GetComponent<FoodGenerator>();
                seller.buyFood();
            }
        }

        if (controller.GetPressUp(triggerButton) && pickup != null)
        {
           
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (!dragging && collider.gameObject.layer != layer)
        {
            pickup = collider.gameObject;
            Debug.Log(pickup.name.ToString());
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (!dragging) {
            if (collider == pickup)
            pickup = null;
        }
    }
}