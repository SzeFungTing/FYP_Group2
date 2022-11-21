using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public bool isVacuum = false;

    //private Rigidbody rb;
    private Vector3 originalScale;

    private Vector3 scaleChange = new Vector3(0.01f, 0.01f, 0.01f);

    Quaternion startRotation;
    float time;

    // Start is called before the first frame update
    void Start()
    {
        originalScale = GetComponent<Transform>().localScale;
        //rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isVacuum)
        {
            if (gameObject.transform.localScale.y < originalScale.y)
            {
                gameObject.transform.localScale += scaleChange;
            }
            else if (gameObject.transform.localScale.y > originalScale.y)
            {
                gameObject.transform.localScale += -scaleChange;
            }

            //gameObject.transform.rotation = Quaternion.Slerp(startRotation, Quaternion.identity, time);
        }

        if (gameObject.transform.position.y < -2)   //delete target when fall out of map
        {
            Destroy(gameObject);
        }

        if (gameObject.transform.position.y > 80)   //delete target when fly too high
        {
            Destroy(gameObject);
        }
    }
}
