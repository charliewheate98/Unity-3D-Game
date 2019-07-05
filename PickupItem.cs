using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupItem : MonoBehaviour
{
    public Camera camera;
    public float distanceFromCamera;

    private GameObject gameObject;
    private bool carry = false;

    void Carry()
    {
        gameObject.GetComponent<Rigidbody>().isKinematic = true;
        gameObject.GetComponent<Rigidbody>().transform.position = camera.transform.position + camera.transform.forward * distanceFromCamera;
    }

    void Drop()
    {
        carry = false;
        gameObject.GetComponent<Rigidbody>().isKinematic = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (carry) Carry();

        if (Input.GetKey(KeyCode.E))
        {
            RaycastHit hit;

            Ray ray = camera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                if(hit.collider.tag == "PickupItem")
                {
                    gameObject = hit.collider.gameObject;

                    carry = true;
                }
            } 
        }
    }
}
