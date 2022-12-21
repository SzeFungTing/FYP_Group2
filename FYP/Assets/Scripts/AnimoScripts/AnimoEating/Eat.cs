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
    Vector3 offset;
    Vector3 scaleChange;



    // Start is called before the first frame update
    private void Start()
    {
        Food = null;
        time = 180;
        //Ftime = 3;
        scaleChange = new Vector3(0.3f, 0.3f, 0.3f);
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

                    offset = transform.position;            //Fajro spawn point offset
                    offset.x += 0.8f;
                    offset.y += 0.2f;
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
                Food.GetComponent<Collider>().enabled = false;
                Food.transform.position = transform.position;
                Food.transform.SetParent(transform);
                Food.transform.localScale = scaleChange;
                Food.GetComponent<DissolveObject>().isAte = true;
                //Destroy(other.gameObject);

                isEaten = true;
                

            }


        }
        
    }
}
