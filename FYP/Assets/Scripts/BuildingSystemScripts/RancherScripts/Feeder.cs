using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Feeder : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.attachedRigidbody && other.CompareTag("Food"))
            other.attachedRigidbody.useGravity = false;
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Food"))
        {
            if (other.attachedRigidbody.velocity.magnitude > 0)
            {
                other.attachedRigidbody.velocity *= 0.9f;

                if (other.attachedRigidbody.velocity.magnitude < 0.1f)
                {
                    other.attachedRigidbody.velocity = Vector3.zero;
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.attachedRigidbody && other.CompareTag("Food"))
            other.attachedRigidbody.useGravity = true;
    }
}
