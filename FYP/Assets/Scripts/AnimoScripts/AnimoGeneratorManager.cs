using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimoGeneratorManager : MonoBehaviour
{
    int animoCount = 0;
    bool canGenerate = true;
    float countDown = 0.0f;

    [SerializeField] int maxNum = 30;
    [SerializeField] AnimoGenerator ag;
    [SerializeField] float cooldown = 300.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (countDown > 0)
        {
            countDown -= Time.deltaTime;
        }

        if (countDown <= 0)
        {
            canGenerate = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"));
        {
            if(canGenerate)
            {
                animoCount = GameObject.FindGameObjectsWithTag("Target").Length;

                if (animoCount <= maxNum)
                {
                    int generateNum = Random.Range(1, 6);
                    ag.Generate(generateNum);
                }

                canGenerate = false;
                countDown = cooldown;
            }
        }
        
        
    }
}
