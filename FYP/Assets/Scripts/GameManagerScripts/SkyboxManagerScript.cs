using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyboxManagerScript : MonoBehaviour
{
    public float skyboxSpeed = 1.5f;
    public Material skyMaterial;
    public float T=0;
    // Start is called before the first frame update
    void Start()
    {
    
    }

    // Update is called once per frame
    void Update()
    {
        RenderSettings.skybox.SetFloat("_Rotation", Time.time * skyboxSpeed);
        T -= Time.deltaTime;
        if (T<=0)
            RenderSettings.skybox = skyMaterial;

    }
}
