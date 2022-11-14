using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimoGenerator : MonoBehaviour
{
    [SerializeField] private Rigidbody animoPrefab;

    public float speed = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Generate(int num)
    {
        for (int i = 0; i < num; i++)
        {
            Rigidbody rb = Instantiate(animoPrefab, transform.position, transform.rotation);
            rb.velocity = transform.forward * speed;
        }
    }
}
