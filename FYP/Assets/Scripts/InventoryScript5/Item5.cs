using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "Scriptable object/Item")]
public class Item5 : ScriptableObject
{
    [Header("Only gameplay")]
    
    public ItemType type;
    public int maxStackSize = 10;
    public float price;
    public GameObject objectPrefab;
    public int id;
   

    [Header("Only UI")]
    public bool stackabke = true;
    public bool sellable = true;

    [Header("Both")]
    public Sprite image;

    [Header("Shop")]
    public string itemDisplayName;
    public float originalPrice;
    public int limit;


    //public ItemType GetItemType()
    //{
    //    return type;
    //}

    //public bool IsBuildingBlock()
    //{
    //    if (type == ItemType.BuildingBlock)
    //        return true;
    //    return false;
    //}
}

public enum ItemType
{
    BuildingBlock,
    Food,
    Material,
    Ghost,
    FajroCore,

    Traget,
    Cube,
    Capsule,
    Cylinder,
    House,
    Tool,

}

public enum ActionType
{
    Dig,
    Mine
}



