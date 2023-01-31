using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMovement : MonoBehaviour
{
    public float flySpeed = 1f;
    public float movementSpeed = 1f;
    public float rotationSpeed = 100f;
    public float eatCooldown = 200f;
    public float findFoodDistance = 500f;

    //[SerializeField] Eat _eat;
    [SerializeField] EatFood _eatFood;
    [SerializeField] EatFajro _eatFajro;

    private bool isWandering = false;
    private bool isRotatingLeft = false;
    private bool isRotatingRight = false;
    private bool isWalking = false;
    private bool isFlyingUp = false;
    private bool isFlyingDown = false;
    private bool isStoping = false;
    //private bool isHungry = true;
    //private bool hvFood = false;
    //private bool isWalkingToFood = false;
    //private bool isRotatingToFood = false;

    //private float perviousEatTime = 0f;
    //private GameObject closestFood;

    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Stop();
    }

    // Update is called once per frame
    void Update()
    {
        //if (Time.deltaTime - perviousEatTime > eatCooldown)
        //{
        //    isHungry = true;
        //}

        //if (isHungry)
        //{
        //    closestFood = FindClosestFood();
        //}

        if (_eatFajro.GetHvFajro())
        {
            StopCoroutine(Wander());
        }

        if (_eatFood && _eatFood.GetHvFood())
        {
            StopCoroutine(Wander());
        }

        if (!isWandering)
        {
            StartCoroutine(Wander());
        }

        //if (isRotatingToFood)
        //{
        //    Vector3 lookPos = closestFood.transform.position - transform.position;
        //    Quaternion rotation = Quaternion.LookRotation(lookPos);
        //    transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 1.5f);
        //    //transform.LookAt(lookPos);
        //}

        //if (isWalkingToFood)
        //{
        //    //Vector3 lookPos = closestFood.transform.position - transform.position;

        //    //rb.AddForce(transform.forward * movementSpeed);
        //    //if (lookPos.y > transform.position.y)
        //    //{
        //    //    rb.AddForce(transform.up * movementSpeed);
        //    //}
        //    //else
        //    //{
        //    //    rb.AddForce(transform.up * -movementSpeed);
        //    //}
        //    //transform.position = Vector3.MoveTowards(transform.position, closestFood.transform.position, 0.01f);
        //    transform.position = Vector3.Lerp(transform.position, closestFood.transform.position, 0.001f);
        //}

        if (isRotatingRight)
        {
            //Debug.Log("isRotatingRight");
            transform.Rotate(transform.up * Time.deltaTime * rotationSpeed);
        }

        if (isRotatingLeft)
        {
            //Debug.Log("isRotatingLeft");
            transform.Rotate(transform.up * Time.deltaTime * -rotationSpeed);
        }

        if (isFlyingUp)
        {
            //Debug.Log("isFlyingUp");
            rb.AddForce(transform.up * flySpeed);
            rb.AddForce(transform.forward * movementSpeed);
        }

        if (isFlyingDown)
        {
            //Debug.Log("isFlyingDown");
            rb.AddForce(transform.up * -flySpeed);
            rb.AddForce(transform.forward * movementSpeed);
        }

        if (isWalking)
        {
            //Debug.Log("isWalking");
            rb.AddForce(transform.forward * movementSpeed);
        }

        if (isStoping)
        {
            //Debug.Log("isStoping");
            Stop();
        }
    }

    IEnumerator Wander()
    {
        int rotationTime = Random.Range(1, 3);
        int rotateWait = Random.Range(1, 3);
        int rotateDirection = Random.Range(1, 3);
        int flyOrWalk = Random.Range(1, 3);
        int walkWait = Random.Range(1, 5);
        int walkTime = Random.Range(1, 3);
        int flyDirection = Random.Range(1, 3);
        int flyWait = Random.Range(1, 5);
        int flyTime = Random.Range(1, 3);
        int eatWait = Random.Range(1, 5);

        int stopTime = Random.Range(1, 5);

        isWandering = true;

        //if (isHungry)
        //{
        //    if (hvFood)
        //    {
        //        isRotatingToFood = true;
        //        yield return new WaitForSeconds(rotationTime);
        //        isRotatingToFood = false;

        //        isWalkingToFood = true;
        //        yield return new WaitForSeconds(10);
        //        isWalkingToFood = false;

        //        hvFood = false;
        //        isHungry = false;
        //    }
        //}

        if (flyOrWalk == 1)
        {
            yield return new WaitForSeconds(flyWait);

            if (flyDirection == 1)
            {
                isFlyingUp = true;
                yield return new WaitForSeconds(flyTime);
                isFlyingUp = false;
            }
            if (flyDirection == 2)
            {
                isFlyingDown = true;
                yield return new WaitForSeconds(flyTime);
                isFlyingDown = false;
            }

        }

        if (flyOrWalk == 2)
        {
            yield return new WaitForSeconds(walkWait);
            isWalking = true;
            yield return new WaitForSeconds(walkTime);
            isWalking = false;
        }

        isStoping = true;
        yield return new WaitForSeconds(stopTime);
        isStoping = false;

        //if (isHungry)
        //{
        //    if (hvFood)
        //    {
        //        yield return new WaitForSeconds(rotationTime);

        //        isWalkingToFood = true;
        //        yield return new WaitForSeconds(eatWait);
        //        isWalkingToFood = false;

        //        hvFood = false;
        //        isHungry = false;
        //    }
        //}

        yield return new WaitForSeconds(rotateWait);

        if (rotateDirection == 1)
        {
            isRotatingLeft = true;
            yield return new WaitForSeconds(rotationTime);
            isRotatingLeft = false;
        }

        if (rotateDirection == 2)
        {
            isRotatingRight = true;
            yield return new WaitForSeconds(rotationTime);
            isRotatingRight = false;
        }

        isWandering = false;
    }

    private void Stop()
    {
        rb.velocity = rb.velocity * 0.995f;
    }

    //private GameObject FindClosestFood()
    //{
    //    GameObject[] foods;
    //    foods = GameObject.FindGameObjectsWithTag("Food");
    //    GameObject closest = null;
    //    float distance = Mathf.Infinity;
    //    foreach (GameObject food in foods)
    //    {
    //        if (food == _eat.Food)
    //        {
    //            Vector3 diff = food.transform.position - transform.position;
    //            float curDistance = diff.sqrMagnitude;
    //            if (curDistance < distance)
    //            {
    //                closest = food;
    //                distance = curDistance;
    //            }
    //        }
    //    }

    //    //Debug.Log("distance: " + distance);

    //    if (distance <= findFoodDistance)
    //    {
    //        hvFood = true;
    //        return closest;
    //    }
    //    else
    //    {
    //        hvFood = false;
    //        return null;
    //    }
    //}
}
