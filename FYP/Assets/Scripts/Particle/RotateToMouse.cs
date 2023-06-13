using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateToMouse : MonoBehaviour
{
    public Camera cam;
    public float maximunLenght;

    private Ray rayMouse;
    private Vector3 pos;
    private Vector3 direction;
    private Quaternion rotation;

    [SerializeField]private int offset = 0;

    // Update is called once per frame
    void Update()
    {
        if (cam != null)
        {
            //Debug.Log(" Update RotateToMouse");
            //RaycastHit hit;
            //var mousePos = Input.mousePosition;
            //rayMouse = cam.ScreenPointToRay(mousePos);
            ////if (Physics.Raycast(rayMouse.origin, rayMouse.direction, out hit, maximunLenght))
            //if (Physics.Raycast(Camera.main.ScreenPointToRay(new Vector2(Screen.width / 2, Screen.height / 2)), out RaycastHit raycastHit, maximunLenght))
            //{

            //    RotateToMouseDirection(gameObject, /*hit*/raycastHit.point);
            //}
            //else
            //{
            //    var pos = rayMouse.GetPoint(maximunLenght);
            //    RotateToMouseDirection(gameObject, pos);
            //}

            //2
            Ray rayMouse2 = cam.ScreenPointToRay(new Vector2(Screen.width / 2, Screen.height / 2));
            Vector3 tempPos = rayMouse2.GetPoint(maximunLenght);
            tempPos.x -= offset;
            RotateToMouseDirection(gameObject, tempPos);
        }
        else
        {
            Debug.Log("No Camera");
        }
    }

    void RotateToMouseDirection(GameObject obj, Vector3 destination)
    {
        direction = destination - obj.transform.position;
        rotation = Quaternion.LookRotation(direction);
        //obj.transform.localRotation = Quaternion.Lerp(obj.transform.rotation, rotation, 1);
    }

    public Quaternion GetRotation()
    {
        return rotation;
    }
}
