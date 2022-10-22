using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunShooting : MonoBehaviour
{
    public GameObject target;
    Transform gunPos;

    // Start is called before the first frame update
    void Start()
    {
        gunPos = this.transform;
        //Debug.Log(gunPos.position);
    }

    // Update is called once per frame
    void Update()
    {
        gunPos = this.transform;
        //gunPos = this.transform;
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
        //gunPos = this.transform;
        Vector3 newGunPos = gunPos.position;
        newGunPos.z = gunPos.transform.forward + 0.4f;
        Instantiate(target, transform.TransformPoint(newGunPos), gunPos.rotation);
    }

    //https://cindyalex.pixnet.net/blog/post/143734467-unity-c%23-%E5%8F%96%E5%BE%97%E7%88%B6%E5%AD%90%E7%89%A9%E4%BB%B6
    //    ameObject.transform.GetChild(0).gameObject;
}
