using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitMaze : MonoBehaviour
{
    public GameObject PuzzleStartPoint;
    public GameObject FPSController;

    void OnParticleCollision(GameObject other)
    {
        if (other.transform.tag == "Player")
        {
            FPSController.transform.position = PuzzleStartPoint.transform.position;
        }
    }
}