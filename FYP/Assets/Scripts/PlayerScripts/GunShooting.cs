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





    //private Inventory2 inventory;
    //[SerializeField] private HotBarScript hotBarScript;

    // Start is called before the first frame update
    void Start()
    {
        //gunPos = this.transform;
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
        Item5 receivedItem = InventoryManager5.instance.GetSelectedItem(true);
        //if (receivedItem != null)
        //    Debug.Log("use item: " + receivedItem);
        //else
        //    Debug.Log("no item use ");

        if (receivedItem)
        {
            //Debug.Log("spawn");
            GameObject born = Instantiate(receivedItem.objectPrefab, shootingPoint.transform.position/*GetGunPos()*/, ObjectRotation());
            born.GetComponent<Rigidbody>().velocity = speed * transform.forward;
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
