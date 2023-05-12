using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crystal : MonoBehaviour
{
    public float time = 600;
    public float timecount=600;
    public GameObject prefab;
    float minX, minZ;

    void Start()
    {
        minX = transform.position.x - 50f;
        minZ = transform.position.z - 50f;
    }
    void Update()
    {
        timecount -= Time.deltaTime;
        if (timecount <= 0.0f)
        {
            for(int i=0;i<5; i++)
            {
                float x = Random.Range(minX,minX+60);
                float z = Random.Range(minZ,minZ+60);
                GameObject newGameObject = Instantiate(prefab,new Vector3(x,transform.position.y,z), Quaternion.identity);
            }

            timecount = 600;
        }

            
    }
}
