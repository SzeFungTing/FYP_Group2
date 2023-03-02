using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterMaze : MonoBehaviour
{
    public GameObject MazeStartPoint;
    public GameObject FPSController;

    void OnTriggerEnter(Collider collision)
    {
        if (collision.transform.tag == "Player")
        {
            FPSController.transform.position = MazeStartPoint.transform.position;
        }
    }
}
