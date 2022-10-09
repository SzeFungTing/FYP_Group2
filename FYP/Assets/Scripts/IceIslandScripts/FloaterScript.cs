using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloaterScript : MonoBehaviour
{
    //public Rigidbody rigidBody;
    //public float depthBeforeSubmergrd = 1f;
    //public float displacementAmount = 3f;
    //public int floaterCount = 1;
    //public float waterDrge = 0.99f;
    //public float waterAngularDrag = 0.5f;



    //private void FixedUpdate()
    //{
    //    rigidBody.AddForceAtPosition(Physics.gravity / floaterCount, transform.position, ForceMode.Acceleration);

    //    float waveHeight = WaveManagerScript.instance.GetWaveHeight(transform.position.x);
    //    if(transform.position.y < waveHeight)
    //    {
    //        float displacementMultiplier = Mathf.Clamp01((waveHeight - transform.position.y) / depthBeforeSubmergrd) * displacementAmount;
    //        rigidBody.AddForceAtPosition(new Vector3(0f, Mathf.Abs(Physics.gravity.y) * displacementMultiplier, 0f),transform.position, ForceMode.Acceleration);
    //        rigidBody.AddForce(displacementMultiplier * -rigidBody.velocity * waterDrge * Time.fixedDeltaTime, ForceMode.VelocityChange);
    //        rigidBody.AddTorque(displacementMultiplier * -rigidBody.angularVelocity * waterAngularDrag * Time.fixedDeltaTime, ForceMode.VelocityChange);

    //    }
    //}



    [SerializeField]
    float buoyancy_force;
    Rigidbody rig;

    public Transform waterPos;
    public float waterHeight = 16;
    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float y_pos = transform.position.y;
        if (y_pos < waterHeight)
        {
            rig.AddForce(transform.up * buoyancy_force);
        }
    }

}
