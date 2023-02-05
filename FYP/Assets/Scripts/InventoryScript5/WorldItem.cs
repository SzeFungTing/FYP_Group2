using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldItem : MonoBehaviour
{
    public Item5 item;
    /*[HideInInspector]*/public bool canConsume;

    private void Start()
    {
        canConsume = false;
    }
}
