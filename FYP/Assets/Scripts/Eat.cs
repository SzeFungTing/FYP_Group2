using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Eat : MonoBehaviour
{
    bool isEaten = false;
    float time;
    public GameObject FajroCore;
    Vector3 offset;



    // Start is called before the first frame update
    private void Start()
    {
        time = 180;
    }

    // Update is called once per frame
    void Update()
    {
        if (isEaten)
        {



            if(time > 0)
            {
                time -= Time.deltaTime;
               

                if (time <= 0) { 
                    isEaten = false;
                    time = 180;
                }
                   
                    

            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isEaten) { 
            
            if (other.tag == "Food")
        {
            Destroy(other.gameObject);
            isEaten = true;
            offset = transform.position;
            offset.x += 1;
            Instantiate(FajroCore,offset, Quaternion.identity);
            }
        }
        
    }
}
