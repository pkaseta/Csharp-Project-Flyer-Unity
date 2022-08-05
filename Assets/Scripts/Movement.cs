using System.Threading;
using System.Collections.Specialized;
using System.Numerics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    //Variable to control the amount of trust for the rocket
    [SerializeField] float thrustPower = 100f;
    [SerializeField] float rotationSpeed = 1f;

    //rb = Rigidbody component with Rigdbbody type
    Rigidbody rb;

    // Start is called before the first frame update
    void Start() {
       rb = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update() {
        ProcessThrust();
        ProcessRotation();
    }

    void ProcessThrust() {
        if(Input.GetKey("space")) {
            rb.AddRelativeForce(0, thrustPower * Time.deltaTime, 0);
        }

    }

    void ProcessRotation() {
        if(Input.GetKey("a")) {
            ApplyRotation(rotationSpeed);
        } else if(Input.GetKey("d")) {
             ApplyRotation(-rotationSpeed);
          }
    }

    void ApplyRotation(float rotation){
        //Freeze rotation from physics sytem when moving
        rb.freezeRotation = true;
        transform.Rotate(0,0,rotation * Time.deltaTime);
        //Unfreeze so physics system can take over again
        rb.freezeRotation = false;
    }
}
