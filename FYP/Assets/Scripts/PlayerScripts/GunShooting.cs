using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GunShooting : MonoBehaviour
{
    //public GameObject target;

    Transform gunPos;

    //[SerializeField]
    //float speed = 8f;

    Vector3 gunLoaclPos;





    private Inventory2 inventory;
    [SerializeField] private HotBarScript hotBarScript;

    // Start is called before the first frame update
    void Start()
    {
        gunPos = this.transform;
    }

    // Update is called once per frame
    void Update()
    {
        gunPos = this.transform;


        if (Input.GetButtonDown("Fire1") && (hotBarScript.GetComponent<Image>().sprite!=null))
        {
            Shoot();
        }
    }

    public void SetInventory(Inventory2 inventory)
    {
        this.inventory = inventory;
    }



    public void Shoot()
    {
        gunLoaclPos = gunPos.localPosition;
        gunLoaclPos.z = gunPos.localPosition.z + 0.8f;


        Item2 item = hotBarScript.GetHotbarItem();


        inventory.RemoveItem(new Item2 { itemType = item.itemType, amount = 1 });
        ItemWorld.DropItem(transform.TransformPoint(gunLoaclPos), new Item2 { itemType = item.itemType, amount = 1 }, transform);
    }


    Quaternion ObjectRotation()
    {
        return Quaternion.LookRotation(-transform.right);
    }

    public Vector3 GetGunPos()
    {
        return transform.TransformPoint(gunLoaclPos);
    }
}
