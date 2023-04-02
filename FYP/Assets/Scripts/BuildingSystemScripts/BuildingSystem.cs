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

    private PlaceableObject objectToPlace = null;
    private bool canBuild = false;

    Item5 previousReceivedItem = null;
    [SerializeField] List<Material> tempMaterialsColor;
    public Material RedMat;


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
        if(receivedItem)
        {
            if (receivedItem.type == ItemType.BuildingBlock && (canBuild != true || previousReceivedItem != receivedItem))           //if yes, display the object
            {

                if (objectToPlace && objectToPlace.GetComponent<ObjectDrag>() && previousReceivedItem && previousReceivedItem != receivedItem)
                {
                    //Debug.Log("destroy(objectToPlace.gameObject)");
                    Destroy(objectToPlace.gameObject);

                }

     

                InitalizeWithObject(receivedItem.objectPrefab);
                previousReceivedItem = receivedItem;

               
                canBuild = true;
            }
            else if (receivedItem.type == ItemType.BuildingBlock && (canBuild != true || previousReceivedItem == receivedItem))         //display can or cannot place
            {
                //Debug.Log("check CanBePlaced");

                CanBePlaced(objectToPlace);
            }
            else if (receivedItem.type != ItemType.BuildingBlock)               //if switch to other item, close the building object display
            {
                canBuild = false;
                if (objectToPlace && objectToPlace.GetComponent<ObjectDrag>())
                    Destroy(objectToPlace.gameObject);
            }
        }
        else                                                                    //if switch to no item, close the building object display
        {
            canBuild = false;
            if (objectToPlace && objectToPlace.GetComponent<ObjectDrag>())
                Destroy(objectToPlace.gameObject);
        }
        previousReceivedItem = receivedItem;



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
                //Destroy(objectToPlace.gameObject);
                //canBuild = false;
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
        //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Ray ray = Camera.main.ScreenPointToRay(new Vector2(Screen.width / 2, Screen.height / 2));
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

    //[SerializeField] Material[] tempMaterialsColor2;
    public void InitalizeWithObject(GameObject prefab)
    {
        //Debug.Log("spawn");

        tempMaterialsColor = new List<Material>();
        //tempMaterialsColor2 = null;

        Vector3 position = SnapCoordinateToGrid(Vector3.zero);

        GameObject obj = Instantiate(prefab, position, Quaternion.identity);
        objectToPlace = obj.GetComponent<PlaceableObject>();
        obj.AddComponent<ObjectDrag>();
        obj.layer = LayerMask.NameToLayer("Ignore Raycast");

        foreach(Transform transform in objectToPlace.transform)
        {
            //Debug.Log("1(transform): " + transform);

            if (transform.TryGetComponent<Renderer>(out Renderer renderer))
            {
                foreach(Material r1 in renderer.materials)
                {
                    //Debug.Log("2(renderer): " + r1);
                    tempMaterialsColor.Add(r1);
                }
                   
                //foreach(Material r in renderer.materials)
                //tempMaterialsColor.Add(r);
                //tempMaterialsColor2 = renderer.materials;
            }
            else
            {
                foreach (Transform tChild in transform)
                {
                    //Debug.Log("3(tChild): " + tChild);

                    if (tChild.TryGetComponent<Renderer>(out Renderer renderer2))
                    {
                        foreach (Material r2 in renderer2.materials)
                        {
                            //Debug.Log("4(renderer2): " + r2);
                            tempMaterialsColor.Add(r2);
                        }

                        //foreach (Material r2 in renderer2.materials)
                        //tempMaterialsColor2 = renderer2.materials;

                        


                    }
                }
            }

        }
    }

    private bool CanBePlaced(PlaceableObject placeableObject)
    {
        //Debug.Log("CanBePlaced");

        BoundsInt area = new BoundsInt();
        area.position = gridLayout.WorldToCell(objectToPlace.GetStartPosition());
        area.size = placeableObject.Size;
        area.size = new Vector3Int(area.size.x + 1, area.size.y + 1, area.size.z);

        TileBase[] baseArray = GetTilesBlock(area, MainTilemap);

        

        foreach (var b in baseArray)
        {
            if(b == null || b!= whiteTile)
            {



                foreach (Transform transform in objectToPlace.transform)                        //check gameobjcet child
                {
                    //Debug.Log("1(CanBePlaced)");

                    if (transform.TryGetComponent<Renderer>(out Renderer renderer))
                    {
                        Material[] arrMat = new Material[renderer.materials.Length];        //crate a redMat array
                        for (int i = 0; i < renderer.materials.Length; i++)                 //change gameobjcet child
                        {

                            //Debug.Log("2(CanBePlaced)");
                            //Debug.Log("renderer.materials[i]: " + renderer.materials[i]);
                            arrMat[i] = RedMat;
                            //renderer.materials[i].color = Color.red;
                            //renderer.materials[i].SetColor("_Color", Color.red);
                        }

                        renderer.materials = arrMat;                                             //gameobject mat array change to redMat array
                    }
                    else
                    {
                        foreach (Transform tChild in transform)                                 //change gameobjcet child child
                        {
                            //Debug.Log("3(CanBePlaced)");

                            if (tChild.TryGetComponent<Renderer>(out Renderer renderer2))
                            {
                                Material[] arrMat = new Material[renderer2.materials.Length];              //crate a redMat array
                                for (int i = 0; i < renderer2.materials.Length; i++)                 //change gameobjcet child child
                                {
                                    //Debug.Log("4(CanBePlaced)");
                                    //renderer2.materials[i] = RedMat;
                                    arrMat[i] = RedMat;
                                }
                                renderer2.materials = arrMat;

                            }
                        }
                    }
                }


                //Debug.Log("CanBePlaced: cannot");

                return false;
            }
        }

        int index = 0;
        foreach (Transform transform in objectToPlace.transform)
        {
            //Debug.Log("5(CanBePlaced(true))");

            if (transform.TryGetComponent<Renderer>(out Renderer renderer))
            {
                List<Material> copyTempMaterialsColor = new List<Material>();
                for (int i = 0; i < renderer.materials.Length; i++)
                {
                    copyTempMaterialsColor.Add(tempMaterialsColor[index]);
                    index++;

                    //foreach (Material m in copyTempMaterialsColor)
                    //{
                    //    Debug.Log("m: " + m);

                    //}
                }
                Material[] tempMaterialsColorArray = copyTempMaterialsColor.ToArray();

                for (int i = 0; i < renderer.materials.Length; i++)
                {
                    //Debug.Log("6(CanBePlaced(true))");
                    //Debug.Log("renderer.materials[i]: " + renderer.materials[i]);
                    //Debug.Log("tempMaterialsColorArray[index]: " + tempMaterialsColorArray[index]);


                    renderer.materials = tempMaterialsColorArray;
                }
            }
            else
            {
                foreach (Transform tChild in transform)
                {
                    //Debug.Log("7(CanBePlaced(true))");



                    if (tChild.TryGetComponent<Renderer>(out Renderer renderer2))
                    {
                        List<Material> copyTempMaterialsColor = new List<Material>();
                        //Debug.Log("renderer2.materials.Length: " + renderer2.materials.Length);

                        for (int i = 0; i < renderer2.materials.Length; i++)
                        {
                            copyTempMaterialsColor.Add(tempMaterialsColor[index]);
                            index++;

                            //foreach (Material m in copyTempMaterialsColor)
                            //{
                            //    Debug.Log("m: " + m);

                            //}
                        }

                        Material[] tempMaterialsColorArray = copyTempMaterialsColor.ToArray();
                        for (int i = 0; i < renderer2.materials.Length; i++)
                        {
                            //Debug.Log("8(CanBePlaced(true))");
                            //Debug.Log("renderer.materials[i]: " + renderer2.materials[i]);
                            //Debug.Log("tempMaterialsColorArray[index]: " + tempMaterialsColorArray[index]);


                            //renderer2.materials[i] = tempMaterialsColorArray[index];
                            renderer2.materials = tempMaterialsColorArray;
                            //index++;
                        }
                    }
                }
            }
        }


        //Debug.Log("CanBePlaced: can");

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

    public void ChangeColor()
    {

    }

    public PlaceableObject GetPlaceableObject()
    {
        return objectToPlace;
    }

    public void SetCanBuild(bool action)
    {
        canBuild = action;
    }
}
