using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    //Serialized fields Vars
    [SerializeField] float thrustPower = 100f;
    [SerializeField] float rotationSpeed = 1f;
    [SerializeField] AudioClip mainEngine;
    [SerializeField] ParticleSystem thrusterParticles;

    //rigidBody = Rigidbody component with Rigdbbody type
    Rigidbody rigidBody;
    //audio Source Component
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start() {
       rigidBody = GetComponent<Rigidbody>();
       audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update() {
        ProcessThrust();
        ProcessRotation();
    }

    //Handle Thrusting
    void ProcessThrust() {
        if(Input.GetKey("space")) {
          StartThrusting();
        } else {
            StopThrusting();
          }
    }

    //Handle rotation
    void ProcessRotation() {
        if(Input.GetKey("a")) {
            ApplyRotation(rotationSpeed);
        } else if(Input.GetKey("d")) {
             ApplyRotation(-rotationSpeed);
          }
    }

    //Function that controls the rotation based on which button you click
    void ApplyRotation(float rotation){
        //Freeze rotation from physics sytem when moving
        rigidBody.freezeRotation = true;
        transform.Rotate(0,0,rotation * Time.deltaTime);
        //Unfreeze so physics system can take over again
        rigidBody.freezeRotation = false;
    }

    //Function to handle thrusting for rocket and effects
    void StartThrusting() {
          if(!thrusterParticles.isPlaying) {
            thrusterParticles.Play();
            }

            rigidBody.AddRelativeForce(0, thrustPower * Time.deltaTime, 0);

            if(!audioSource.isPlaying) {
            audioSource.PlayOneShot(mainEngine);
            }
    }

    //Function that handles effects when you stop thrusting
    void StopThrusting() {
        thrusterParticles.Stop();
        audioSource.Stop();
    }
}
