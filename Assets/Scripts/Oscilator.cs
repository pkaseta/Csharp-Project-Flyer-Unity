using UnityEngine;

public class Oscilator : MonoBehaviour
{
   Vector3 startingPosition;
   
   [SerializeField] Vector3 movementVector;
   [SerializeField] [Range(0,1)] float movementFactor;
   [SerializeField] float period = 2f;

   void Start() {
    startingPosition = transform.position;
   }

   void Update() {
    const float tau = Mathf.PI * 2;

    //Mathf.Epsilon is the smallest float
    if(period <= Mathf.Epsilon){
        return;
    }
    float cycles = Time.time / period;
    float rawSinWave = Mathf.Sin(cycles * tau);

    movementFactor = (rawSinWave + 1f) / 2f;

    Vector3 offset = movementVector * movementFactor;
    transform.position = startingPosition + offset;
   }
}
