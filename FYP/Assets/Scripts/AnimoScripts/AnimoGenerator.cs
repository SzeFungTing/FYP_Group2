using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimoGenerator : MonoBehaviour
{
    [SerializeField] private Rigidbody animoPrefab;

    public float generationInterval = 0.5f;
    public float speed = 0.5f;

    private float previousGenerationTime = 0.0f;
    private bool isGenerating;
    private int currentNum;
    private float currentTime;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        currentTime = Time.time;
    }

    public void Generate(int targetNum)
    {
        //isGenerating = true;
        //currentNum = 0;
        ////currentTime = 100.0f;

        //while (isGenerating)
        //{
        //    if (currentTime > previousGenerationTime + generationInterval)
        //    {
        //        Rigidbody rb = Instantiate(animoPrefab, transform.position, transform.rotation);
        //        rb.velocity = transform.forward * speed;
        //        previousGenerationTime = Time.time;
        //        currentNum++;
        //    }
        //    if (currentNum == targetNum)
        //    {
        //        isGenerating = false;
        //    }

        //    //currentTime = Time.time;
        //}

        //previousGenerationTime = 0.0f;

        for (int i = 0; i < targetNum; i++)
        {
            Rigidbody rb = Instantiate(animoPrefab, transform.position + Vector3.up * i, transform.rotation);
            rb.velocity = transform.up * speed;
        }
    }
}
