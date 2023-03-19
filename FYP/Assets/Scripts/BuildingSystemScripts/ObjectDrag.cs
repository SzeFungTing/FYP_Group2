using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDrag : MonoBehaviour
{
    private Vector3 offset;

    private void Start()
    {
        //offset = transform.position/* - BuildingSystem.GetMouseWorldPosition()*/;
    }

    private void Update()
    {
        //Vector3 pos = Camera.main.ScreenPointToRay(new Vector2(Screen.width / 2, Screen.height / 2)).GetPoint(10) + offset;

        Vector3 pos = BuildingSystem.GetMouseWorldPosition()/* + offset*/;
        transform.position = BuildingSystem.current.SnapCoordinateToGrid(pos);
    }

    //private void OnMouseDown()
    //{
    //    offset = transform.position - BuildingSystem.GetMouseWorldPosition();
    //}

    //private void OnMouseDrag()
    //{
    //    Vector3 pos = BuildingSystem.GetMouseWorldPosition() + offset;
    //    transform.position = BuildingSystem.current.SnapCoordinateToGrid(pos);
    //}
}
