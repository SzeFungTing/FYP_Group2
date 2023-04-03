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
    float originalHeight;
    [SerializeField]float height;
    float digestionTime = 3f;

    private void Awake()
    {
        material = GetComponent<Renderer>().material;
        isAte = false;
        height = transform.position.y + (objectHeight / 2) + 1;
        originalHeight = transform.position.y;
        SetHeight(height);
    }

    private void Update()
    {

        if (isAte)
        {
            digestionTime -= Time.deltaTime;
            if (digestionTime <= 0)
            {
                if (height <= transform.position.y - (objectHeight / 2))                //if end the disappear, start poo 
                {
                    
                    transform.GetComponentInParent<EatFood>().canPoo = true;
                    Destroy(transform.parent.gameObject);
                }
                else                                                            //start to disappear
                {
                    height -= Time.deltaTime;
                    SetHeight(height);
                }
            }
            
        }
        else
        {
            Debug.Log("else");
            if(originalHeight + 1 < transform.position.y || transform.position.y > originalHeight - 1)
            {
                Debug.Log("SetHeight");

                height = transform.position.y + (objectHeight / 2) + 1;
                SetHeight(height);
            }
        }
        
    }

    private void SetHeight(float height)
    {
        material.SetFloat("_CutoffHeight", height);
        material.SetFloat("_NoiseStrength", noiseStrength);
    }
}