using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Eat : MonoBehaviour
{
    bool isEaten = false;
    bool isPooed = false;
    float time,Ftime;
    public GameObject FajroCore;
    public GameObject Food;
    Vector3 offset;
    Vector3 scaleChange;



    // Start is called before the first frame update
    private void Start()
    {
        time = 180;
        Ftime = 3;
        scaleChange = new Vector3(0.3f, 0.3f, 0.3f);
    }

    // Update is called once per frame
    void Update()
    {
        if (isEaten)
        {
           

            if (time > 0)
            {
                time -= Time.deltaTime;
                Ftime -= Time.deltaTime;

                if (time <= 0) {
                    isPooed = false;
                    isEaten = false;
                    time = 180;
                }
                   
               
                if (Ftime <= 0 && !isPooed)
                {
                    isPooed = true;
                    GameObject food = Instantiate(Food, transform.position, Quaternion.identity,transform);
                    food.transform.localScale = scaleChange;
                    Instantiate(FajroCore, offset, Quaternion.identity);
                    Ftime =3;
                }
     

            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (!isEaten) { 
            
            if (other.tag == "Food")
            {
                Food = other.gameObject;
                //Destroy(other.gameObject);
                isEaten = true;
                offset = transform.position;
                offset.x += 2;

            }


        }
        
    }
}
