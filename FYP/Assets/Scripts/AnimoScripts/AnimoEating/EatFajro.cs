using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EatFajro : MonoBehaviour
{
    [SerializeField]
    private GameObject[] FajroWillNotEat;

    [SerializeField]
    private GameObject[] ListOfMixedAnimoPrefab;

    [SerializeField]
    private Eat _eat;

    [SerializeField]
    private AIMovement _aIMovement;

    public float movementSpeed = 1f;
    public float rotationSpeed = 100f;
    public float findFajroDistance = 300f;

    private bool isEatingFajro = false;
    private bool hvFajro = false;
    private bool isRotatingToFajro = false;
    private bool isWalkingToFajro = false;

    private GameObject closestFajro;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        closestFajro = FindClosestFajro();

        if (hvFajro)
        {
            _aIMovement.enabled = false;
            if (!isEatingFajro)
                StartCoroutine(EatingFajro());
        }
        else
        {
            _aIMovement.enabled = true;
        }

        if (isRotatingToFajro)
        {
            Vector3 lookPos = closestFajro.transform.position - transform.position;
            Quaternion rotation = Quaternion.LookRotation(lookPos);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 1.5f);
            //transform.LookAt(lookPos);
        }

        if (isWalkingToFajro)
        {
            transform.position = Vector3.Lerp(transform.position, closestFajro.transform.position, 0.005f);
        }
    }

    IEnumerator EatingFajro()
    {
        int rotationTime = Random.Range(1, 3);
        int eatWait = Random.Range(1, 5);

        if (hvFajro)
            isEatingFajro = true;

        isRotatingToFajro = true;
        yield return new WaitForSeconds(rotationTime);
        isRotatingToFajro = false;

        isWalkingToFajro = true;
        yield return new WaitForSeconds(eatWait);
        isWalkingToFajro = false;

        isEatingFajro = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Fajro")
        {
            //for (int i = 0; i < FajroWillNotEat.Length; i++)
            //{
            //    Debug.Log("FajroWillNotEat fajro type :" + FajroWillNotEat[i].GetComponent<Fajro>().fajroType.ToString());
            //    Debug.Log("other fajro type : " + other.GetComponent<Fajro>().fajroType.ToString());

            //    if (FajroWillNotEat[i].GetComponent<Fajro>().fajroType == other.GetComponent<Fajro>().fajroType)
            //    {
            //        return;
            //    }
            //}

            Destroy(other.gameObject);

            for (int i = 0; i < ListOfMixedAnimoPrefab.Length; i++)
            {
                if ((ListOfMixedAnimoPrefab[i].GetComponent<Animo>().animoType1.ToString() == gameObject.GetComponent<Animo>().animoType1.ToString()            /*animo prefab type1 == this animo type1*/
                    || ListOfMixedAnimoPrefab[i].GetComponent<Animo>().animoType2.ToString() == gameObject.GetComponent<Animo>().animoType1.ToString())         /*animo prefab type2 == this animo type1*/
                    && (ListOfMixedAnimoPrefab[i].GetComponent<Animo>().animoType1.ToString() == other.gameObject.GetComponent<Fajro>().fajroType.ToString()    /*animo prefab type1 == fajro ate type*/
                    || ListOfMixedAnimoPrefab[i].GetComponent<Animo>().animoType2.ToString() == other.gameObject.GetComponent<Fajro>().fajroType.ToString()))   /*animo prefab type2 == fajro ate type*/
                {
                    Instantiate(ListOfMixedAnimoPrefab[i], transform.position, transform.rotation);
                }
            }

            Destroy(gameObject);
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
            if (fajro != _eat.FajroCore)
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
}
