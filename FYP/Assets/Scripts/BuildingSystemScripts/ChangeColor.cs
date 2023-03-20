using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColor : MonoBehaviour
{
    public Material redMat;
    public Material[] redMat2;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("gameObject.GetComponent<Renderer>().materials[0]: " + gameObject.GetComponent<Renderer>().materials[0]);
        //GetComponent<Renderer>().materials[0] = redMat;
        //GetComponent<Renderer>().sharedMaterial = redMat2[0];
        //GetComponent<Renderer>().materials[0] = redMat2[0];
        GetComponent<Renderer>().materials[1].color = Color.red;

        //GetComponent<Renderer>().material = redMat;

        //GetComponent<Renderer>().materials[0].SetColor("_Color", Color.red);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
