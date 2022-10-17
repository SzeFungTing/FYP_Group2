using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterForSwimming : MonoBehaviour
{

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerMovement movement = other.GetComponent<PlayerMovement>();
            movement.isSwimming = true;
            movement.Swimming();
            movement.ResetVelocity();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerMovement movement = other.GetComponent<PlayerMovement>();
            movement.isSwimming = false;
            movement.Swimming();
            movement.ResetVelocity();
        }
    }
}
