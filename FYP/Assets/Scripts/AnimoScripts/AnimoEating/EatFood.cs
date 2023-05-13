using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EatFood : MonoBehaviour
{
    bool isEaten = false;
    //bool isPooed = false;
    public bool canPoo = false;
    float time/*,Ftime*/;
    public GameObject FajroCore;
    public GameObject Food;
    Vector3 offset;                         //the fajroCore spwan point

    Vector3 foodScaleChange;                    //the food scale change after Animo ate
    public float foodScaleChangeValue = 0.3f;

    [SerializeField] private Transform dissolvePoint;
    [SerializeField] private AudioClip eatSound, pooSound;

    //for eat food AI
    private bool isHungry = true;
    private bool hvFood = false;
    private bool isWalkingToFood = false;
    private bool isRotatingToFood = false;
    private bool isEatingFood = false;
    private bool isStoping = false;
    public float findFoodDistance = 500f;
    public float eatCooldown = 200f;
    private float perviousEatTime = 0f;
    private GameObject closestFood;
    Rigidbody rb;

    [SerializeField] AIMovement _aIMovement;
    [SerializeField] EatFajro _eatFajro;

    // Start is called before the first frame update
    private void Start()
    {
        Food = null;
        time = 180;
        //Ftime = 3;
        foodScaleChange = new Vector3(foodScaleChangeValue, foodScaleChangeValue, foodScaleChangeValue);

        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.deltaTime - perviousEatTime > eatCooldown)
        {
            isHungry = true;
        }

        if (isHungry)
        {
            closestFood = FindClosestFood();
        }

        if (hvFood && !_eatFajro.GetHvFajro())
        {
            _aIMovement.enabled = false;
            if (!isEatingFood)
            {
                StartCoroutine(EatingFood());
            }
        }
        else
        {
            _aIMovement.enabled = true;
        }

        if (_eatFajro.GetHvFajro())
        {
            StopCoroutine(EatingFood());
        }

        if (isRotatingToFood)
        {
            Vector3 lookPos = closestFood.transform.position - transform.position;
            Quaternion rotation = Quaternion.LookRotation(lookPos);
            transform.rotation = Quaternion.Slerp(transform.rotation, new Quaternion(0f, rotation.y, 0f, rotation.w), Time.deltaTime * 1.5f);
        }

        if (isWalkingToFood && closestFood != null)
        {
            //transform.position = Vector3.Lerp(transform.position, closestFood.transform.position, 0.001f);
            rb.AddForce((closestFood.transform.position - transform.position) * _aIMovement.movementSpeed * 0.08f);
        }

        if (isStoping)
        {
            //Debug.Log("isStoping");
            Stop();
        }

        if (isEaten)
        {
            if (time > 0)           //how long(180s) can eat
            {
                time -= Time.deltaTime;
                //Ftime -= Time.deltaTime;

                if (time <= 0)
                {            //after 180s reset, Amino can eat angin
                    //isPooed = false;
                    isEaten = false;
                    time = 180;
                }


                //if (Ftime <= 0 && !isPooed)         //after 3s to poo the Fajro
                if (canPoo)
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
                    AudioSource.PlayClipAtPoint(pooSound, transform.position);
                    //Ftime =3;
                }


            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {

        if (!isEaten)
        {

            if (other.tag == "Food")
            {
                Food = other.gameObject;
                Food.transform.GetComponent<Target>().enabled = false;                  //close the "Target" script, so player can not inhale the food
                Food.transform.GetComponent<Collider>().enabled = false;                //close the isTrigger Collider, the food will not run the fnuction again
                Food.transform.GetChild(0).GetComponent<Collider>().enabled = false;    //close the Collider, so the food will not collider with the Animo
                Food.GetComponent<Rigidbody>().isKinematic = true;                      //close the Rigidbody, so the food will not drop down
                Food.transform.position = dissolvePoint.position;                           //the food delivery into the Animo
                Food.transform.SetParent(transform);                                    //set the food parent be the Animo
                Food.transform.localScale = foodScaleChange;                            //scale the food size
                Food.GetComponentInChildren<DissolveObject>().isAte = true;             //the food start to Digestion
                AudioSource.PlayClipAtPoint(eatSound, transform.position);
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

    IEnumerator EatingFood()
    {
        int rotationTime = Random.Range(1, 3);
        int eatWait = Random.Range(4, 6);
        int stopTime = Random.Range(1, 5);

        if (hvFood)
            isEatingFood = true;

        isRotatingToFood = true;
        yield return new WaitForSeconds(rotationTime);
        isRotatingToFood = false;

        isWalkingToFood = true;
        yield return new WaitForSeconds(eatWait);
        isWalkingToFood = false;

        isStoping = true;
        yield return new WaitForSeconds(stopTime);
        isStoping = false;

        isEatingFood = false;
    }

    private GameObject FindClosestFood()
    {
        GameObject[] foods;
        foods = GameObject.FindGameObjectsWithTag("Food");

        //Debug.Log(foods.Length);

        GameObject closest = null;
        float distance = Mathf.Infinity;
        foreach (GameObject food in foods)
        {
            if (food.transform.parent == Food)
            {
                Vector3 diff = food.transform.position - transform.position;
                float curDistance = diff.sqrMagnitude;
                if (curDistance < distance)
                {
                    closest = food;
                    distance = curDistance;
                }
            }
        }

        //Debug.Log("distance: " + distance);

        if (distance <= findFoodDistance)
        {
            hvFood = true;
            return closest;
        }
        else
        {
            hvFood = false;
            return null;
        }
    }

    public bool GetHvFood()
    {
        //Debug.Log(hvFood);
        return hvFood;
    }

    private void Stop()
    {
        rb.velocity = rb.velocity * 0.995f;
    }

    public bool GetIsHungey()
    {
        return isHungry;
    }
}
