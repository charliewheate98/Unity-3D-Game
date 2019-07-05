using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSController : MonoBehaviour
{
    public float speed = 10.0f;
    public bool FreezeMovement = false;
    public bool FreezeSprinting = true;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float translation = Input.GetAxis("Vertical") * speed;
        float straffe     = Input.GetAxis("Horizontal") * speed;

        if(!FreezeMovement)
        {
            translation *= Time.deltaTime;
            straffe *= Time.deltaTime;

            transform.Translate(straffe, 0, translation);
        }

        if (Input.GetKeyDown("escape"))
            Cursor.lockState = CursorLockMode.None;

        if(!FreezeSprinting)
        {
            if (Input.GetKey(KeyCode.LeftShift)) speed = 10.0f;
            else speed = 5.0f;
        }
    }
}
