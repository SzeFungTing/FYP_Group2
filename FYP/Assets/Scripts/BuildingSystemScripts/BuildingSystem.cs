using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BuildingSystem : MonoBehaviour
{
    public static BuildingSystem current;

    public GridLayout gridLayout;
    private Grid grid;
    [SerializeField] private Tilemap MainTilemap;
    [SerializeField] private TileBase whiteTile;


    public GameObject prefab1;
    public GameObject prefab2;

    private PlaceableObject objectToPlace;
    private bool canBuild = false;

    #region Unity methods

    private void Awake()
    {
        current = this;
        grid = gridLayout.gameObject.GetComponent<Grid>();
    }

    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.N))
        //{
        //    InitalizeWithObject(prefab1);
        //    canBuild = true;
        //}
        //else if (Input.GetKeyDown(KeyCode.M))
        //{
        //    InitalizeWithObject(prefab2);
        //    canBuild = true;
        //}

        Item5 receivedItem = InventoryManager5.instance.GetSelectedItem(false);
        if (receivedItem)                                                       //check the item is building object?
        {
            if (receivedItem.type == ItemType.BuildingBlock && !canBuild)           //if yes, display the object
            {
                InitalizeWithObject(receivedItem.objectPrefab);
                canBuild = true;
            }
            else if (receivedItem.type != ItemType.BuildingBlock)               //if switch to other item, close the building object display
            {
                canBuild = false;
                if(objectToPlace && objectToPlace.GetComponent<ObjectDrag>())
                    Destroy(objectToPlace.gameObject);
            }
        }
        else                                                                    //if switch to no item, close the building object display
        {
            canBuild = false;
            if (objectToPlace && objectToPlace.GetComponent<ObjectDrag>())
                Destroy(objectToPlace.gameObject);
        }





        //if (!objectToPlace)
        //{
        //    return;
        //}

        if (Input.GetKeyDown(KeyCode.R))        //rotate building
        {
            objectToPlace.Rotate();
        }
        else if (Input.GetButtonDown("Fire1") && canBuild)          //building building
        {
            if (CanBePlaced(objectToPlace))
            {
                objectToPlace.Place();
                Vector3Int start = gridLayout.WorldToCell(objectToPlace.GetStartPosition());
                TakeArae(start, objectToPlace.Size);
                receivedItem = InventoryManager5.instance.GetSelectedItem(true);
                canBuild = false;
            }
            else
            {
                Destroy(objectToPlace.gameObject);
                canBuild = false;
            }
        }
        //else if (Input.GetKeyDown(KeyCode.Escape))
        //{
        //    Destroy(objectToPlace.gameObject);
        //}

      


        if (Input.GetMouseButtonDown(1))        //delet building
        {
            RaycastHit raycastHit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out raycastHit, 100f))
            {
                if ((raycastHit.transform != null) && (raycastHit.transform.gameObject.GetComponent<PlaceableObject>() != null) && !raycastHit.transform.gameObject.GetComponent<ObjectDrag>())
                {
                    PlaceableObject deletObject = raycastHit.transform.gameObject.GetComponent<PlaceableObject>();
                    Vector3Int start = gridLayout.WorldToCell(deletObject.GetStartPosition());
                    DeletArae(start, deletObject.Size);
                    Destroy(deletObject.gameObject);
                }
            }
        }

    }

    #endregion


    #region Utils

    public static Vector3 GetMouseWorldPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit raycastHit))
        {
            return raycastHit.point;
        }
        else
        {
            return Vector3.zero;
        }
    }

    public Vector3 SnapCoordinateToGrid(Vector3 position)
    {
        Vector3Int cellPos = gridLayout.WorldToCell(position);
        position = grid.GetCellCenterWorld(cellPos);
        return position;
    }

    private static TileBase[] GetTilesBlock(BoundsInt area, Tilemap tilemap)
    {
        TileBase[] array = new TileBase[area.size.x * area.size.y * area.size.z];
        int counter = 0;

        foreach(var v in area.allPositionsWithin)
        {
            Vector3Int pos = new Vector3Int(v.x, v.y, 0);
            array[counter] = tilemap.GetTile(pos);
            counter++;
        }

        return array;
    }

    #endregion


    #region Building Placement

    public void InitalizeWithObject(GameObject prefab)
    {
        Vector3 position = SnapCoordinateToGrid(Vector3.zero);

        GameObject obj = Instantiate(prefab, position, Quaternion.identity);
        objectToPlace = obj.GetComponent<PlaceableObject>();
        obj.AddComponent<ObjectDrag>();
        obj.layer = LayerMask.NameToLayer("Ignore Raycast"); 
    }

    private bool CanBePlaced(PlaceableObject placeableObject)
    {
        BoundsInt area = new BoundsInt();
        area.position = gridLayout.WorldToCell(objectToPlace.GetStartPosition());
        area.size = placeableObject.Size;
        area.size = new Vector3Int(area.size.x + 1, area.size.y + 1, area.size.z);

        TileBase[] baseArray = GetTilesBlock(area, MainTilemap);

        foreach(var b in baseArray)
        {
            if(b == null || b!= whiteTile)
            {
                return false;
            }
        }

        return true;
    }

    public void TakeArae(Vector3Int start, Vector3Int size)
    {
        MainTilemap.BoxFill(start, null, start.x, start.y, 
                            start.x + size.x, start.y + size.y);
    }

    public void DeletArae(Vector3Int start, Vector3Int size)
    {
        MainTilemap.BoxFill(start, whiteTile, start.x, start.y,
                            start.x + size.x, start.y + size.y);
    }

    #endregion

    public PlaceableObject GetPlaceableObject()
    {
        return objectToPlace;
    }

    public void SetCanBuild(bool action)
    {
        canBuild = action;
    }
}
