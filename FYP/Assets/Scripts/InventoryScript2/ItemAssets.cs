using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAssets : MonoBehaviour
{
    public static ItemAssets Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    public Transform pfItemWorld;

    public Sprite tragetSprite;
    public Sprite chickenGhostSprite;
    public Sprite cubeSprite;

    public GameObject tragetObject;
    public GameObject chickenGhostObject;
    public GameObject cubeObject;
}
