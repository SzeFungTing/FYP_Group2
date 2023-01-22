using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumeItem : MonoBehaviour
{
    CraftingTable craftingTable;
    WorldItem destroyItem;

    public bool canDestroy;

    private void Start()
    {
        craftingTable = GetComponentInParent<CraftingTable>();
    }

    private void OnTriggerEnter(Collider other)
    {
        //craftingTable.ConsumeMaterial();
        //if (canDestroy)
        //{
        //    Debug.Log(other.TryGetComponent(out WorldItem worldItem2));
        //    if(other.TryGetComponent(out WorldItem worldItem) && worldItem.item.type == ItemType.Material)
        //    {
        //        Debug.Log("Destroy: " + worldItem.gameObject);
        //        Destroy(worldItem.gameObject);

        //    }
        //    canDestroy = false;
        //}

        //if (other.TryGetComponent(out WorldItem worldItem) && worldItem.item.type == ItemType.Material)
        //{
        //    Debug.Log("get destroyItem");
        //    destroyItem = worldItem;
        //    craftingTable.ConsumeMaterial(destroyItem);

        //}

        //if (canDestroy)
        //{
        //    Debug.Log("Destroy destroyItem");
        //    Destroy(destroyItem.gameObject);
        //    canDestroy = false;
        //}
    }
}
