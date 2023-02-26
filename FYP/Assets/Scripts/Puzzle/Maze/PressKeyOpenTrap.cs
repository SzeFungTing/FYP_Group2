using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressKeyOpenTrap : MonoBehaviour
{
    public GameObject Instruction;
    public GameObject InvisibleDoor;
    public GameObject Trigger;
    public ParticleSystem Trap;
    public bool Action = false;

    void Start()
    {
        Instruction.SetActive(false);

    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.transform.tag == "Player")
        {
            Instruction.SetActive(true);
            Action = true;
        }
    }

    void OnTriggerExit(Collider collision)
    {
        Instruction.SetActive(false);
        Action = false;
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (Action == true)
            {
                Instruction.SetActive(false);
                Action = false;
                Trap.Play();
                Trigger.SetActive(false);
                InvisibleDoor.SetActive(false);
            }
        }

    }
}
