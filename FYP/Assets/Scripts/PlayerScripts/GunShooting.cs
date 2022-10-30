using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunShooting : MonoBehaviour
{
    public GameObject target;
    GameObject born;
    Transform gunPos;

    [SerializeField]
    float speed = 8f;

    //Animator anim;

    private Inventory2 inventory;

    [SerializeField] private UI_Inventory uiInventory;

    // Start is called before the first frame update
    void Start()
    {
        gunPos = this.transform;

        inventory = new Inventory2();
        
        //Debug.Log(gunPos.position);
    }

    // Update is called once per frame
    void Update()
    {
        gunPos = this.transform;


        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }

        //Debug.DrawLine(transform.position, transform.right );
    }

    void Shoot()
    {
        //gunPos = this.transform;
        Vector3 gunLoaclPos = gunPos.localPosition;
        gunLoaclPos.z = gunPos.localPosition.z + 0.8f;

        born = Instantiate(target, transform.TransformPoint(gunLoaclPos), ObjectRotation());
        born.GetComponent<Rigidbody>().velocity = speed * transform.forward;

        //anim = born.gameObject.GetComponentInChildren<Animator>();
        //anim.SetTrigger("isBorn");

    }


    Quaternion ObjectRotation()
    {
        //Quaternion rotation = Quaternion.LookRotation(-transform.right);
        return Quaternion.LookRotation(-transform.right);
    }





}
