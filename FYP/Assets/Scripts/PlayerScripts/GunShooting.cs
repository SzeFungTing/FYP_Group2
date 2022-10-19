using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunShooting : MonoBehaviour
{
    public GameObject target;
    Vector3 gunPos;

    // Start is called before the first frame update
    void Start()
    {
        gunPos = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetAxis("Mouse ScrollWheel") > 0f) //forward
        //{
        //    Debug.Log("forward: " + Input.GetAxis("Mouse ScrollWheel"));
        //}
        //else if(Input.GetAxis("Mouse ScrollWheel") < 0f) //backwards
        //{
        //    Debug.Log("backwards: " + Input.GetAxis("Mouse ScrollWheel"));
        //}

        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }

    }

    void Shoot()
    {
        //Instantiate(target, gunPos, gunPos);
    }
}
