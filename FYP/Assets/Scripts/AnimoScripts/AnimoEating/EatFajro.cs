using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EatFajro : MonoBehaviour
{
    [SerializeField]
    private GameObject[] FajroWillNotEat;

    [SerializeField]
    private GameObject[] ListOfMixedAnimoPrefab;

    //[SerializeField]
    //private Eat _eat;

    [SerializeField]
    private EatFood _eatFood;

    [SerializeField]
    private AIMovement _aIMovement;

    public float movementSpeed = 1f;
    public float rotationSpeed = 100f;
    public float findFajroDistance = 300f;

    private bool isEatingFajro = false;
    private bool hvFajro = false;
    private bool isRotatingToFajro = false;
    private bool isWalkingToFajro = false;
    private bool isStoping = false;

    private GameObject closestFajro;
    private Rigidbody rb;
    private Animo _animo;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        _animo = GetComponent<Animo>();
    }

    // Update is called once per frame
    void Update()
    {
        closestFajro = FindClosestFajro();

        if (hvFajro)
        {
            _aIMovement.enabled = false;
            _eatFood.enabled = false;
            if (!isEatingFajro)
            {
                StartCoroutine(EatingFajro());
            }
        }
        else
        {
            _aIMovement.enabled = true;
            _eatFood.enabled = true;
        }

        if (isRotatingToFajro)
        {
            Vector3 lookPos = closestFajro.transform.position - transform.position;
            Quaternion rotation = new Quaternion(0f, Quaternion.LookRotation(lookPos).y, 0f, transform.rotation.w);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 1.5f);
            //transform.LookAt(lookPos);
        }

        if (isWalkingToFajro)
        {
            //transform.position = Vector3.Lerp(transform.position, closestFajro.transform.position, 0.005f);
            rb.AddForce((closestFajro.transform.position - transform.position) * _aIMovement.movementSpeed * 0.3f);
        }

        if (isStoping)
        {
            //Debug.Log("isStoping");
            Stop();
        }
    }

    IEnumerator EatingFajro()
    {
        int rotationTime = Random.Range(1, 3);
        int eatWait = Random.Range(1, 5);
        int stopTime = Random.Range(1, 5);

        if (hvFajro)
            isEatingFajro = true;

        isRotatingToFajro = true;
        yield return new WaitForSeconds(rotationTime);
        isRotatingToFajro = false;

        isWalkingToFajro = true;
        yield return new WaitForSeconds(eatWait);
        isWalkingToFajro = false;

        isStoping = true;
        yield return new WaitForSeconds(stopTime);
        isStoping = false;

        isEatingFajro = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Fajro")
        {
            /*for (int i = 0; i < FajroWillNotEat.Length; i++)
            {
                Debug.Log("FajroWillNotEat fajro type :" + FajroWillNotEat[i].GetComponent<Fajro>().fajroType.ToString());
                Debug.Log("other fajro type : " + other.GetComponent<Fajro>().fajroType.ToString());

                if (FajroWillNotEat[i].GetComponent<Fajro>().fajroType == other.GetComponent<Fajro>().fajroType)
                {
                    return;
                }
            }*/

            if (/*other != _eat.FajroCore*/gameObject.GetComponent<Animo>().animoType1.ToString() != other.gameObject.GetComponent<Fajro>().fajroType1.ToString()
                        && gameObject.GetComponent<Animo>().animoType2.ToString() != other.gameObject.GetComponent<Fajro>().fajroType1.ToString())
            {
                Destroy(other.gameObject);

                for (int i = 0; i < ListOfMixedAnimoPrefab.Length; i++)
                {
                    if ((ListOfMixedAnimoPrefab[i].GetComponent<Animo>().animoType1.ToString() == gameObject.GetComponent<Animo>().animoType1.ToString()            /*animo prefab type1 == this animo type1*/
                        || ListOfMixedAnimoPrefab[i].GetComponent<Animo>().animoType2.ToString() == gameObject.GetComponent<Animo>().animoType1.ToString())         /*animo prefab type2 == this animo type1*/
                        && (ListOfMixedAnimoPrefab[i].GetComponent<Animo>().animoType1.ToString() == other.gameObject.GetComponent<Fajro>().fajroType1.ToString()    /*animo prefab type1 == fajro ate type*/
                        || ListOfMixedAnimoPrefab[i].GetComponent<Animo>().animoType2.ToString() == other.gameObject.GetComponent<Fajro>().fajroType1.ToString())   /*animo prefab type2 == fajro ate type*/
                        && gameObject.GetComponent<Animo>().animoType1.ToString() != other.gameObject.GetComponent<Fajro>().fajroType1.ToString()
                        && gameObject.GetComponent<Animo>().animoType2.ToString() != other.gameObject.GetComponent<Fajro>().fajroType1.ToString())
                    {
                        Instantiate(ListOfMixedAnimoPrefab[i], transform.position/* + Vector3.up*/, transform.rotation);
                    }
                }

                Destroy(gameObject);
            }
            
        }
    }

    private GameObject FindClosestFajro()
    {
        GameObject[] fajros;
        fajros = GameObject.FindGameObjectsWithTag("Fajro");
        GameObject closest = null;
        float distance = Mathf.Infinity;
        foreach (GameObject fajro in fajros)
        {
            if (/*fajro.transform.parent != _eatFood.FajroCore*/ 
                _animo.animoType1.ToString() != fajro.GetComponent<Fajro>().fajroType1.ToString() &&
                _animo.animoType2.ToString() != fajro.GetComponent<Fajro>().fajroType1.ToString() &&
                _animo.animoType1.ToString() != fajro.GetComponent<Fajro>().fajroType2.ToString() &&
                _animo.animoType2.ToString() != fajro.GetComponent<Fajro>().fajroType2.ToString()
                )
            {
                Vector3 diff = fajro.transform.position - transform.position;
                float curDistance = diff.sqrMagnitude;
                if (curDistance < distance)
                {
                    closest = fajro;
                    distance = curDistance;
                }
            }
        }

        if (distance <= findFajroDistance)
        {
            hvFajro = true;
            return closest;
        }
        else
        {
            hvFajro = false;
            return null;
        }
    }

    public bool GetHvFajro()
    {
        //Debug.Log(hvFajro);
        return hvFajro;
    }

    private void Stop()
    {
        rb.velocity = rb.velocity * 0.995f;
    }
}
