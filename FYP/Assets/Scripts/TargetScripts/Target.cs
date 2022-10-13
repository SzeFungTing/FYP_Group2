using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public bool isVacuum = false;

    //private Rigidbody rb;
    private Vector3 originalScale;

    private Vector3 scaleChange = new Vector3(0.01f, 0.01f, 0.01f);

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

            //if (rb.velocity.x < 0.01f || rb.velocity.z < 0.01f)
            //{
            //    rb.velocity = Vector3.zero;
            //    rb.angularVelocity = Vector3.zero;
            //}
        }
    }
}
