using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMovement : MonoBehaviour
{
    public float flySpeed = 1f;
    public float movementSpeed = 1f;
    public float rotationSpeed = 100f;

    private bool isWandering = false;
    private bool isRotatingLeft = false;
    private bool isRotatingRight = false;
    private bool isWalking = false;
    private bool isFlyingUp = false;
    private bool isFlyingDown = false;
    private bool isStoping = false;

    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isWandering)
        {
            StartCoroutine(Wander());
        }

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
            rb.AddForce(transform.forward * -movementSpeed);
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

        int stopTime = Random.Range(1, 5);

        isWandering = true;

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
        rb.velocity = rb.velocity * 0.99f;
    }
}
