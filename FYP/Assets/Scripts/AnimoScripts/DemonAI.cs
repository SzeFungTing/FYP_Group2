using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonAI : MonoBehaviour
{
    public float flySpeed = 1f;
    public float movementSpeed = 1f;
    public float rotationSpeed = 100f;
    public float findPlayerDistance = 200f;
    public int hp = 10;
    public float attackCD = 3;

    [SerializeField]
    AudioClip attack;

    private bool isWandering = false;
    private bool isRotatingLeft = false;
    private bool isRotatingRight = false;
    private bool isWalking = false;
    private bool isFlyingUp = false;
    private bool isFlyingDown = false;
    private bool isStoping = false;

    private bool isAttacking = false;
    private bool hvPlayer = false;
    private bool isWalkingToPlayer = false;
    private bool isRotatingToPlayer = false;
    private float previousAttackTimeCount = 0;

    //private float perviousEatTime = 0f;
    private GameObject closestPlayer;
    private float distanceWithPlayer;

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
        previousAttackTimeCount += Time.deltaTime;

        if (hp <= 0)
        {
            Destroy(gameObject);
        }

        closestPlayer = FindClosestPlayer();
        Debug.Log("hvPlayer: " + hvPlayer);

        if (!isWandering && !hvPlayer)
        {
            StartCoroutine(Wander());
        }

        if (hvPlayer)
        {
            distanceWithPlayer = (closestPlayer.transform.position - transform.position).sqrMagnitude;
        }

        if (hvPlayer && !isAttacking)
        {
            StopCoroutine(Wander());
            StartCoroutine(Attack());
        }

        if (!hvPlayer && isAttacking)
        {
            StopCoroutine(Attack());
            StartCoroutine(Wander());
        }

        //if (distanceWithPlayer <= 5f)
        //{
        //    if (Time.deltaTime - previousAttackTime >= attackCD)
        //    {

        //        //attack player

        //        AudioSource.PlayClipAtPoint(attack, transform.position);
        //        previousAttackTime = Time.deltaTime;
        //    }
        //}

        if (isRotatingToPlayer)
        {
            Vector3 lookPos = closestPlayer.transform.position - transform.position;
            Quaternion rotation = Quaternion.LookRotation(lookPos);
            transform.rotation = Quaternion.Slerp(transform.rotation, new Quaternion(0f, rotation.y, 0f, rotation.w), Time.deltaTime * 1.5f);
        }

        if (isWalkingToPlayer && closestPlayer != null)
        {
            //transform.position = Vector3.Lerp(transform.position, closestFood.transform.position, 0.001f);
            rb.AddForce((closestPlayer.transform.position - transform.position) * movementSpeed * 0.08f);
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

    IEnumerator Attack()
    {
        int rotationTime = Random.Range(1, 3);
        int walkWait = Random.Range(1, 3);

        int stopTime = Random.Range(1, 5);

        isAttacking = true;

        isRotatingToPlayer = true;
        yield return new WaitForSeconds(rotationTime);
        isRotatingToPlayer = false;

        isWalkingToPlayer = true;
        yield return new WaitForSeconds(walkWait);
        isWalkingToPlayer = false;

        isStoping = true;
        yield return new WaitForSeconds(stopTime);
        isStoping = false;

        isAttacking = false;
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

    private void OnTriggerEnter(Collider other)
    {
        if (previousAttackTimeCount >= attackCD)
        {
            if (other.tag == "Player")
            {
                //attack player
                _ = other.GetComponent<PlayerMovement>().playerHP - 5;
                Debug.Log("attack");

                AudioSource.PlayClipAtPoint(attack, transform.position);
                previousAttackTimeCount = 0f;
            }
        }
    }

    private void Stop()
    {
        rb.velocity = rb.velocity * 0.995f;
    }

    private GameObject FindClosestPlayer()
    {
        GameObject[] players;
        players = GameObject.FindGameObjectsWithTag("Player");

        //Debug.Log(foods.Length);

        GameObject closest = null;
        float distance = Mathf.Infinity;
        foreach (GameObject player in players)
        {
            Vector3 diff = player.transform.position - transform.position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closest = player;
                distance = curDistance;
            }
            //if (player.transform.parent == player)
            //{
            //    Vector3 diff = player.transform.position - transform.position;
            //    float curDistance = diff.sqrMagnitude;
            //    if (curDistance < distance)
            //    {
            //        closest = player;
            //        distance = curDistance;
            //    }
            //}
        }

        //Debug.Log("distance: " + distance);

        if (distance <= findPlayerDistance)
        {
            hvPlayer = true;
            return closest;
        }
        else
        {
            hvPlayer = false;
            return null;
        }
    }
}
