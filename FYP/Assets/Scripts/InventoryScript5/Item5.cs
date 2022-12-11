using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "Scriptable object/Item")]
public class Item5 : ScriptableObject
{
    [Header("Only gameplay")]
    //public TileBase tile;
    public ItemType type;
    public int maxStackSize = 10;
    public GameObject objectPrefab;
    //public ActionType actionType;
    //public Vector2Int range = new Vector2Int(5, 4);

    [Header("Only UI")]
    public bool stackabke = true;

    [Header("Both")]
    public Sprite image;

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
}

public enum ActionType
{
    Dig,
    Mine
}

