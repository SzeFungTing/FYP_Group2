using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressKeyOpenPortal : MonoBehaviour
{
    public GameObject OpenInstruction;
    public GameObject CloseInstruction;
    public ParticleSystem Portal;
    public bool Action = false;
    public bool isOpen = false;

    void Start()
    {
        OpenInstruction.SetActive(false);

    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.transform.tag == "Player")
        {
            if (!isOpen)
            {
                OpenInstruction.SetActive(true);
                Action = true;
            }
            else
            {
                CloseInstruction.SetActive(true);
            }
        }
    }

    void OnTriggerExit(Collider collision)
    {
        OpenInstruction.SetActive(false);
        Action = false;
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (Action == true)
            {
                OpenInstruction.SetActive(false);
                Portal.Play();
                Action = false;
                isOpen = true;
            }
            else if (isOpen)
            {
                CloseInstruction.SetActive(false);
                Portal.Stop();
                isOpen = false;
            }
        }

    }
}