using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDead : MonoBehaviour
{
    public Transform spawnPoint;

    //Desert
    public bool isQuicksand;
    //Vector3 originalGravity = new Vector3();
    float originalGravity = 0;
    float timer = 3f;



    private void OnTriggerEnter(Collider other)
    {
        if(!isQuicksand)
        if(other.tag == "Player")
        {
                //Debug.Log("OnTriggerEnter");

                other.transform.position = spawnPoint.position;
            other.GetComponent<PlayerMovement>().playerHP = 100;

            
        }
    }

    private void OnTriggerStay(Collider other)
    {

        if (isQuicksand)
        {
            if (other.tag == "Player")
            {
                //Debug.Log("OnTriggerStay");

                timer -= Time.deltaTime;
                //originalGravity = other.GetComponent<PlayerMovement>().gravity;
                originalGravity = other.GetComponent<PlayerMovement>().gravity;
                //originalGravity = other.attachedRigidbody.velocity;

                //other.GetComponent<PlayerMovement>().gravity = 50;
                other.GetComponent<PlayerMovement>().gravity *= -0.9f;
                //other.attachedRigidbody.velocity *= 0.9f;

                if (timer<= 0)
                {
                    other.transform.position = spawnPoint.position;
                    other.GetComponent<PlayerMovement>().playerHP = 100;
                    timer = 3f;
                }


            }
        }
    }

    private void OnTriggerExit(Collider other)
    {

        if (isQuicksand)
        {
            if (other.tag == "Player")
            {
                //Debug.Log("OnTriggerExit");

                timer -= Time.deltaTime;
                other.GetComponent<PlayerMovement>().gravity = originalGravity;
                //other.attachedRigidbody.velocity = originalGravity;

                if (timer <= 0)
                {
                    other.transform.position = spawnPoint.position;
                    other.GetComponent<PlayerMovement>().playerHP = 100;
                    timer = 3f;
                }
            }
        }
    }
}
