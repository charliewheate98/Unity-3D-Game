using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuCtrl : MonoBehaviour
{
    // Public Object Variables
    public Text _text;

    public Animator _anim;
    public Animator _text_anim;

    public Camera _playerCam;
    public GameObject _player;
    public GameObject _ann;

    // delay
    private int delay_timer;

    // If this bool is true then is plays the camera animation
    bool startAnim = false;

    // The length of the animation
    float animLength = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        // Initialise
        _text.enabled = true; // make the menu text visible
        _anim.enabled = false; // make the animation not play
        _text_anim.enabled = false; // make the company intro anim not play

        // Lock the Player
        _playerCam.GetComponent<MouseLook>().LockCamera      = true;
        _player.GetComponent<FPSController>().FreezeMovement = true;
        _player.GetComponent<Rigidbody>().isKinematic        = true;
    }

    // Update is called once per frame
    void Update()
    {
        // if the startAnim boolean is true, then start the animation timer
        if(startAnim) animLength += Time.deltaTime;

        // if any key is pressed then disable the text and start playing the animation
        if (Input.anyKey)
        {
            startAnim     = true;

            _text.enabled = false;
            _anim.enabled = true;
            _text_anim.enabled = true;
        }

        // Check if the animation is finished
        if (animLength >= _anim.GetComponent<Animator>().runtimeAnimatorController.animationClips[0].length)
        {
            delay_timer++;
            if (delay_timer >= 60000 * Time.deltaTime) _ann.GetComponent<MoveOnPath>().play = true;


            // unfreeze the player
            _playerCam.GetComponent<MouseLook>().LockCamera       = false;
            _player.GetComponent<FPSController>().FreezeMovement  = false;
            _player.GetComponent<Rigidbody>().isKinematic         = false;

            // disable/stop the animation
            _anim.enabled = false;
        }
    }
}
