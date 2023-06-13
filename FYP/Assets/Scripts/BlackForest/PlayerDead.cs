using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDead : MonoBehaviour
{
    public Transform spawnPoint;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            other.transform.position = spawnPoint.position;
            if(TryGetComponent<PlayerMovement>(out PlayerMovement playerMovement))
                other.GetComponent<PlayerMovement>().playerHP = 100;

            if (TryGetComponent<PlayerMovement2>(out PlayerMovement2 playerMovement2))
                other.GetComponent<PlayerMovement2>().playerHP = 100;


        }
    }
}
