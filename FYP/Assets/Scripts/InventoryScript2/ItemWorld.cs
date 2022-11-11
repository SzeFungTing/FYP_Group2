using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemWorld : MonoBehaviour
{
    public Sprite itemSprite;
    public Item itemObject;

    //[SerializeField]
    //static float speed = 8f;

    //public GameObject

    public Item2 item;

    //public GunShooting gunShooting;
    //public static GameObject gunGameObject;
    //public Vector3 gunPoint;

    //public static ItemWorld Instance { get; private set; }

    public static ItemWorld SpawnItemWorld(Vector3 position, Item2 item/*, Quaternion quaternion*/ )
    {
        Debug.Log("GetGameobject: " + item.GetGameObject());
        //Transform transform = Instantiate(item.GetGameObject().transform, position, Quaternion.identity);
        GameObject transform = Instantiate<GameObject>(item.GetGameObject(), position, Quaternion.identity);

        ItemWorld itemWorld = transform.GetComponent<ItemWorld>();

        itemWorld.SetItem(item);

        return itemWorld;
    }

    public static ItemWorld DropItem(Vector3 gunPos, Item2 item, Transform gun)
    {
        Debug.Log("Drop itemType: " + item.itemType + "Drop item.amount: " + item.amount);
        ItemWorld itemWorld = SpawnItemWorld(gunPos, item);
        itemWorld.GetComponent<Rigidbody>().velocity = 8f * gun.forward;
        return itemWorld;
    }



    //private SpriteRenderer spriteRenderer;

    //private void Start()
    //{
    //    //spriteRenderer = GetComponent<SpriteRenderer>();
    //    //SpawnItemWorld();
    //    //SetItem(m_Items, m_Amount);

    //}

    //private void Update()
    //{
    //    //if (item == null)
    //    //    SetItem(itemObject);
    //}

    public void SetItem(Item2 item)
    {
        //item.amount = 1;
        this.item = item;
    }

    public Item2 GetItem()
    {
        //Debug.Log("get item:" + item.GetType());
        //Debug.Log(item.itemType);
        return item;
    }

    public Sprite GetSprite()
    {
        return itemSprite;
    }




    //Debug.Log(sprite.name + "spriteID: " + sprite.GetInstanceID() /*+ "ItemAssets.Instance.tragetSprite" + ItemAssets.Instance.tragetSprite.GetInstanceID()*/);
    //Debug.Log("ItemAssets.Instance.chickenGhostSprite.GetInstanceID(): " + ItemAssets.Instance.chickenGhostSprite.GetInstanceID() +
    //   "ItemAssets.Instance.tragetSprite" + ItemAssets.Instance.tragetSprite.GetInstanceID() +
    //   "ItemAssets.Instance.cubeSprite.GetInstanceID()" + ItemAssets.Instance.cubeSprite.GetInstanceID());

    //this.item = item;
    //itemSprite = item.GetSprite();

    //Debug.Log(item.GetItemType());
    //Debug.Log("ItemAssets.Instance.chickenGhostSprite.name: " + ItemAssets.Instance.chickenGhostSprite.name);
    //Debug.Log(_item.itemName);
    //Debug.Log(_item.itemName + "_item.itemName == \"Traget\"" +
    //   _item.itemName == "Traget");
    //var itemtype = item.GetItemType(_item);

    //Debug.Log("string.Compare(sprite.name, ItemAssets.Instance.tragetSprite.name)" + (string.Compare(itemSprite.name, ItemAssets.Instance.tragetSprite.name)==0) +
    //    "string.Compare(sprite.name, ItemAssets.Instance.chickenGhostSprite.name)" + (string.Compare(itemSprite.name, ItemAssets.Instance.chickenGhostSprite.name)==0) +
    //    "string.Compare(sprite.name, ItemAssets.Instance.cubeSprite.name)" + (string.Compare(itemSprite.name, ItemAssets.Instance.cubeSprite.name)==0));
    //Debug.Log("_item.id" + _item.id);

    //Debug.Log("item.GetItemType(_item): " + item.GetItemType(item1));
    //Debug.Log("itemSprite.name: " + itemSprite.name);
    //Debug.Log(" ItemAssets.Instance.tragetSprite.name: " + ItemAssets.Instance.tragetSprite.name);
    //Debug.Log(" sprite.name == ItemAssets.Instance.tragetSprite.name: " + itemSprite.name == ItemAssets.Instance.tragetSprite.name);
    //Debug.Log(/*"sprite.name == ItemAssets.Instance.tragetSprite.name: " + sprite.name ==*/ ItemAssets.Instance.tragetSprite.name);
    //Debug.Log(/*"sprite.name == ItemAssets.Instance.tragetSprite.name: " +*/ sprite.name /*== ItemAssets.Instance.tragetSprite.name*/);


    //this.item = new Item2 { itemType = item.GetItemType(sprite), amount = 1 };
    //this.item = item.GetItemType(sprite);
    //item.GetItemType(sprite);
}
