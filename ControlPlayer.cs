using UnityEngine;
using System.Collections;


public class ControlPlayer : MonoBehaviour {

   
    public float amountOfRotation;
    public float movementSpeed;
    //public FloatingJoystick inputMove;

    // Use this for initialization
    void Start () {

    }

    // Update is called once per frame
    void Update () {
        if (Input.GetAxisRaw("Vertical") < 0) {
             transform.position -= transform.forward * movementSpeed;
        }
        else if (0 < Input.GetAxisRaw("Vertical")) {
            transform.position += transform.forward * movementSpeed;
        }
        else{

        }

        if (Input.GetAxisRaw("Horizontal") < 0) {
            transform.Rotate (Vector3.up, -amountOfRotation);
        }
        else if (0 < Input.GetAxisRaw("Horizontal")) {
            transform.Rotate (Vector3.up, amountOfRotation);
        }
        else{

        }
    }
}