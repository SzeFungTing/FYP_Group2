using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crystal : MonoBehaviour
{
    public float time = 600;
    [SerializeField]float timecount=0;
    public GameObject prefab;
    float minX, minZ;
    [SerializeField]int maxNumber = 5;
    [SerializeField]float range = 50f;

    void Start()
    {
        minX = transform.position.x - range;
        minZ = transform.position.z - range;
        //timecount = time;
    }
    void Update()
    {
        timecount -= Time.deltaTime;
        if (timecount <= 0.0f)
        {
            int randomSpawnNumber = Random.Range(1, maxNumber);
            for (int i=0;i< randomSpawnNumber; i++)
            {
                float x = Random.Range(minX,minX+ range * 2);
                float z = Random.Range(minZ,minZ+ range * 2);
                GameObject newGameObject = Instantiate(prefab,new Vector3(x,transform.position.y,z), Quaternion.identity);
            }

            timecount = time;
        }

            
    }
}
