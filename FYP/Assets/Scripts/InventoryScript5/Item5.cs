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
   

    [Header("Only UI")]
    public bool stackabke = true;
    public bool sellable = true;

    [Header("Both")]
    public Sprite image;


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
    Tool,
    Traget,
    ChickenGhost,
    Cube,
    Capsule,
    Cylinder,
    House,
    Material
}

public enum ActionType
{
    Dig,
    Mine
}



