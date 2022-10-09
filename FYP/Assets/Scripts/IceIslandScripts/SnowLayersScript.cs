using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowLayersScript : MonoBehaviour
{
    [SerializeField]
    private Material[] materials;

    // Start is called before the first frame update
    private void Awake()
    {
        for (int i = 0; i < materials.Length; ++i)
        {
            materials[i].SetFloat("SnowAmount", 0.0f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        float snowAmount = (Time.time / 50.0f) % 1.2f;

        for (int i = 0; i < materials.Length; ++i)
        {
            materials[i].SetFloat("SnowAmount", snowAmount);
        }
    }
}
