using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class DissolveObject : MonoBehaviour
{
    [SerializeField] private float noiseStrength = 0.25f;
    [SerializeField] private float objectHeight = 1.0f;

    private Material material;

    public bool isAte;
    [SerializeField]float height;
    float digestionTime = 3f;

    private void Awake()
    {
        material = GetComponent<Renderer>().material;
        isAte = false;
        height = transform.position.y + (objectHeight / 2) + 1;
        SetHeight(height);
    }

    private void Update()
    {

        if (isAte)
        {
            digestionTime -= Time.deltaTime;
            if (digestionTime <= 0)
            {
                if (height <= transform.position.y - (objectHeight / 2))
                {
                    
                    transform.parent.GetComponent<Eat>().canPoo = true;
                    Destroy(gameObject);
                }
                else
                {
                    height -= Time.deltaTime;
                    SetHeight(height);
                }
            }
            
        }
        
    }

    private void SetHeight(float height)
    {
        material.SetFloat("_CutoffHeight", height);
        material.SetFloat("_NoiseStrength", noiseStrength);
    }
}