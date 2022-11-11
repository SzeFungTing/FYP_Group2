using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu(fileName = "New Item", menuName = "Item/Create New Item2")]
[Serializable]
public class Item2 
{
    //public GameObject plane;
    //public static Item2 Instance { get; private set; }

    public enum ItemType
    {
        Traget,
        ChickenGhost,
        Cube,
    }

    public ItemType itemType;
    public int amount = 1;

    public Sprite GetSprite()
    {
        //Debug.Log("call Item2 GetSprite()");
        switch (itemType)
        {
            default:
            case ItemType.Traget:
                return ItemAssets.Instance.tragetSprite;
            case ItemType.ChickenGhost:
                return ItemAssets.Instance.chickenGhostSprite;
            case ItemType.Cube:
                return ItemAssets.Instance.cubeSprite;
        }
    }

    public GameObject GetGameObject()
    {
        switch (itemType)
        {
            default:
            case ItemType.Traget:
                return ItemAssets.Instance.tragetObject;
            case ItemType.ChickenGhost:
                return ItemAssets.Instance.chickenGhostObject;
            case ItemType.Cube:
                return ItemAssets.Instance.cubeObject;
        }
    }

    public Item2.ItemType GetItemType(Sprite sprite)
    {
        Debug.Log("call Item2 GetItemType()");
        //switch (sprite)
        //{
        //    default:
        //    case ItemAssets.Instance.tragetSprite:
        //        return ItemType.Traget;
        //    case ItemType.ChickenGhost:
        //        return ItemType.ChickenGhost;
        //    case ItemType.Cube:
        //        return ItemType.Cube;
        //}
        Item2.ItemType type = ItemType.Traget;

        if (string.Compare(sprite.name, ItemAssets.Instance.tragetSprite.name) == 0)
        {
            type = ItemType.Traget;

        }
        else if (string.Compare(sprite.name, ItemAssets.Instance.chickenGhostSprite.name) == 0)
        {
            type = ItemType.ChickenGhost;

        }
        else if (string.Compare(sprite.name, ItemAssets.Instance.cubeSprite.name) == 0)
        {
            type = ItemType.Cube;

        }
        Debug.Log("call Item2 GetItemType(): " + type);
        return type;
    }

    //public /*Item2.ItemType*/void GetItemType(Sprite sprite)
    //{


    //    if (string.Compare(sprite.name, ItemAssets.Instance.tragetSprite.name) == 0)
    //    {

    //        new Item2 { itemType = ItemType.Traget, amount = 1 };
    //    }
    //    else if (string.Compare(sprite.name, ItemAssets.Instance.chickenGhostSprite.name) == 0)
    //    {

    //        new Item2 { itemType = ItemType.ChickenGhost, amount = 1 };
    //    }
    //    else if (string.Compare(sprite.name, ItemAssets.Instance.cubeSprite.name) == 0)
    //    {

    //        new Item2 { itemType = ItemType.Cube, amount = 1 };
    //    }

    //}



    public bool IsStackable()
    {
        switch (itemType)
        {
            default:
            case ItemType.Traget:
            case ItemType.ChickenGhost:
            case ItemType.Cube:
                return true;
            
                //return false;
        }
    }





    //public /*Item2.ItemType*/void GetItemType(Sprite sprite)
    //{
    //    //switch (sprite.GetInstanceID())
    //    //{
    //    //    default:
    //    //    case ItemAssets.Instance.tragetSprite.GetInstanceID():
    //    //        return ItemType.Traget;
    //    //    case ItemAssets.Instance.chickenGhostSprite.GetInstanceID():
    //    //        return ItemType.ChickenGhost; ;
    //    //    case ItemAssets.Instance.chickenGhostSprite.GetInstanceID():
    //    //        return ItemType.Cube;
    //    //}

    //    //Debug.Log("ItemAssets.Instance.chickenGhostSprite.GetInstanceID(): " + ItemAssets.Instance.chickenGhostSprite.GetInstanceID()); + 
    //    //    "ItemAssets.Instance.tragetSprite" + ItemAssets.Instance.tragetSprite.GetInstanceID() +
    //    //    "ItemAssets.Instance.cubeSprite.GetInstanceID()" + ItemAssets.Instance.cubeSprite.GetInstanceID());
    //    //Item2.ItemType type = ItemType.Traget;

    //    //if (sprite.name == ItemAssets.Instance.tragetSprite.name)
    //    //    type = ItemType.Traget;
    //    //else if (sprite.name == ItemAssets.Instance.chickenGhostSprite.name)
    //    //    type = ItemType.ChickenGhost;
    //    //else if (sprite.name == ItemAssets.Instance.cubeSprite.name)
    //    //    type = ItemType.Cube;
    //    //return type;
    //    //Debug.Log("string.Compare(sprite.name, ItemAssets.Instance.tragetSprite.name)" + string.Compare(sprite.name, ItemAssets.Instance.tragetSprite.name) +
    //    //    "string.Compare(sprite.name, ItemAssets.Instance.chickenGhostSprite.name)" + string.Compare(sprite.name, ItemAssets.Instance.chickenGhostSprite.name) +
    //    //    "string.Compare(sprite.name, ItemAssets.Instance.cubeSprite.name)" + string.Compare(sprite.name, ItemAssets.Instance.cubeSprite.name));


    //    if (string.Compare(sprite.name, ItemAssets.Instance.tragetSprite.name) == 0)
    //    {
    //        //type = ItemType.Traget;
    //        new Item2 { itemType = ItemType.Traget, amount = 1 };
    //    }
    //    else if (string.Compare(sprite.name, ItemAssets.Instance.chickenGhostSprite.name) == 0)
    //    {
    //        //type = ItemType.ChickenGhost;
    //        new Item2 { itemType = ItemType.ChickenGhost, amount = 1 };
    //    }
    //    else if (string.Compare(sprite.name, ItemAssets.Instance.cubeSprite.name) == 0)
    //    {
    //        //type = ItemType.Cube;
    //        new Item2 { itemType = ItemType.Cube, amount = 1 };
    //    }
    //    //type = ItemType.Cube;
    //    //return type;
    //}



    //public Item2/*.ItemType*/ GetItemType(Item _item)
    //{
    //    //Debug.Log(_item.itemName + "_item.itemName == \"Traget\"" + 
    //    //    _item.itemName == "Traget");
    //    //switch (_item.itemName)
    //    //{
    //    //    default:
    //    //    case "Traget":
    //    //        return ItemType.Traget;
    //    //    case "ChickenGhost":
    //    //        return ItemType.ChickenGhost;
    //    //    case "Cube":
    //    //        return ItemType.Cube;
    //    //}

    //    //switch (_item.id)
    //    //{
    //    //    default:
    //    //    case 1:
    //    //        return ItemType.Traget;
    //    //    case 2:
    //    //        return ItemType.ChickenGhost;
    //    //    case 3:
    //    //        return ItemType.Cube;
    //    //}

    // switch (_item.id)
    //    {
    //        default:
    //        case 1:
    //            return new Item2 { itemType = ItemType.Traget, amount = 1};
    //        case 2:
    //            return new Item2 { itemType = ItemType.ChickenGhost, amount = 1 };
    //        case 3:
    //            return new Item2 { itemType = ItemType.Cube, amount = 1 };
    //    }
    //}
}
