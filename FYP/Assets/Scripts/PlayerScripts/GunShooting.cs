using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GunShooting : MonoBehaviour
{
    //public GameObject target;

    //Transform gunPos;

    [SerializeField] float speed = 8f;

    Vector3 gunLoaclPos;
    public GameObject shootingPoint;

    //public Animator Anim_Gun;



    //private Inventory2 inventory;
    //[SerializeField] private HotBarScript hotBarScript;

    // Start is called before the first frame update
    void Start()
    {
        //gunPos = this.transform;
        //Anim_Gun = transform.GetComponentInParent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //gunPos = this.transform;


        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    //public void SetInventory(Inventory2 inventory)
    //{
    //    this.inventory = inventory;
    //}



    public void Shoot()
    {
        //shooting offset
        //gunLoaclPos = this.transform.localPosition;
        //gunLoaclPos.z = this.transform.localPosition.z + 0.8f;

        //inventory system

        if (InventoryManager5.instance)
        {
            Item5 receivedItem = InventoryManager5.instance.GetSelectedItem(false);
            //if (receivedItem != null)
            //    Debug.Log("use item: " + receivedItem);
            //else
            //    Debug.Log("no item use ");

            if (receivedItem && receivedItem.type != ItemType.BuildingBlock)
            {
                //Debug.Log("spawn");
                //if(Anim_Gun.GetCurrentAnimatorStateInfo(0).IsName("Arm1_shoot") && Anim_Gun.GetCurrentAnimatorStateInfo(0).normalizedTime > 1)
                {
                    receivedItem = InventoryManager5.instance.GetSelectedItem(true);

                    GameObject born = Instantiate(receivedItem.objectPrefab, shootingPoint.transform.position/*GetGunPos()*/, ObjectRotation());
                    //Debug.Log("born: " + born);
                    born.GetComponent<Rigidbody>().velocity = speed * shootingPoint.transform.forward;
                }

            }
        }
        
        
    }


    Quaternion ObjectRotation()
    {
        return Quaternion.LookRotation(-transform.right);
    }

    //public Vector3 GetGunPos()
    //{
    //    return transform.TransformPoint(gunLoaclPos);
    //}
}
