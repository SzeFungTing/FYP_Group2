using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimoGenerator : MonoBehaviour
{
    [SerializeField] private Rigidbody animoPrefab;

    public float generationInterval = 0.5f;
    public float speed = 0.5f;

    public void Generate(int targetNum)
    {
        for (int i = 0; i < targetNum; i++)
        {
            Rigidbody rb = Instantiate(animoPrefab, transform.position + Vector3.up * i * 1.2f, transform.rotation);
            rb.velocity = transform.up * speed;
        }
    }
}
