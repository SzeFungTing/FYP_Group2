using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerTrap : MonoBehaviour
{
    public ParticleSystem Trap;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.transform.tag == "Player")
        {
            Trap.Play();
        }
    }
}
