using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Eat : MonoBehaviour
{
    bool isEaten = false;
    //bool isPooed = false;
    public bool canPoo = false;
    float time/*,Ftime*/;
    public GameObject FajroCore;
    public GameObject Food;
    Vector3 offset;                         //the fajrpCore spwan point

    Vector3 foodScaleChange;                    //the food scale change after Animo ate
    public float foodScaleChangeValue = 0.3f;



    // Start is called before the first frame update
    private void Start()
    {
        Food = null;
        time = 180;
        //Ftime = 3;
        foodScaleChange = Vector3.one * foodScaleChangeValue;
    }

    // Update is called once per frame
    void Update()
    {
        if (isEaten)
        {
            if (time > 0)           //how long(180s) can eat
            {
                time -= Time.deltaTime;
                //Ftime -= Time.deltaTime;

                if (time <= 0) {            //after 180s reset, Amino can eat angin
                    //isPooed = false;
                    isEaten = false;
                    time = 180;
                }
                   
               
                //if (Ftime <= 0 && !isPooed)         //after 3s to poo the Fajro
                if(canPoo)
                {
                    //isPooed = true;
                    canPoo = false;
                    //GameObject food = Instantiate(Food, transform.position, Quaternion.identity,transform);
                    //food.transform.localScale = scaleChange;

                    //offset = transform.position;            //Fajro spawn point offset
                    //offset.x += 0.8f;
                    //offset.y += 0.2f;
                    offset = SpawnAroundWithRadius();

                    Instantiate(FajroCore, offset, Quaternion.identity);        //Fajro spawn
                    //Ftime =3;
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
                Food.transform.GetComponent<Target>().enabled = false;                  //close the "Target" script, so player can not inhale the food
                Food.transform.GetComponent<Collider>().enabled = false;                //close the isTrigger Collider, the food will not run the fnuction again
                Food.transform.GetChild(0).GetComponent<Collider>().enabled = false;    //close the Collider, so the food will not collider with the Animo
                Food.GetComponent<Rigidbody>().isKinematic = true;                      //close the Rigidbody, so the food will not drop down
                Food.transform.position = transform.position;                           //the food delivery into the Animo
                Food.transform.SetParent(transform);                                    //set the food parent be the Animo
                Food.transform.localScale = foodScaleChange;                            //scale the food size
                Food.GetComponentInChildren<DissolveObject>().isAte = true;             //the food start to Digestion
                //Destroy(other.gameObject);

                isEaten = true;
                

            }
        }   
    }

    private Vector3 SpawnAroundWithRadius()
    {
        float radius = 0.8f;
        Vector3 randomPos = Random.insideUnitSphere * radius;  //get a random point inside or on a sphere with radius     
        randomPos += transform.position;            //the randomPos well based on this gameObject
        randomPos.y = transform.position.y + 1;         //the randomPosY set to same at this gameObject

        Vector3 direction = randomPos - transform.position;
        direction.Normalize();

        float dotProduct = Vector3.Dot(transform.forward, direction);
        float dotProductAngle = Mathf.Acos(dotProduct / transform.forward.magnitude * direction.magnitude);

        randomPos.x = Mathf.Cos(dotProductAngle) * radius + transform.position.x;
        randomPos.z = Mathf.Sin(dotProductAngle * (Random.value > 0.5f ? 1f : -1f)) * radius + transform.position.z;

        return randomPos;
    }
}
