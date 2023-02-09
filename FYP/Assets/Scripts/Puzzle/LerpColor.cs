using System.Collections.Generic;
using UnityEngine;

public class LerpColor : MonoBehaviour
{
    public Renderer hexagon;
    public GameObject all;
    public List<GameObject> list;
    public GameObject Light;

    [SerializeField] private Transform playerCameraTransform;

    int colorIdx = 0;
    float emitTime = 0;
    float emitInterval = 0.5f;

    bool isDone;

    void Start()
    {
        foreach (Transform all in transform)
        {
            all.GetComponent<Renderer>().materials[1].SetColor("_EmissionColor", Color.black);
        }
        Light.SetActive(false);

    }

    private void Update()
    {
        if (colorIdx < 10)
        {
            if (Time.time > emitTime + emitInterval)
            {
                var num = Random.Range(0, all.transform.childCount);

                if (list.Contains(all.transform.GetChild(num).gameObject))
                {
                    num = Random.Range(0, all.transform.childCount);

                }


                all.transform.GetChild(num).GetComponent<Renderer>().materials[1].SetColor("_EmissionColor", Color.red);
                list.Add(all.transform.GetChild(num).gameObject);

                emitTime = Time.time;
                colorIdx++;

            }
        }
        if (colorIdx == 10 && Time.time > emitTime + emitInterval && !isDone)
        {
            foreach (Transform all in transform)
            {
                all.GetComponent<Renderer>().materials[1].SetColor("_EmissionColor", Color.black);

            }
            if (Time.time > emitTime + emitInterval + 1)
            {
                Light.SetActive(true);
                isDone = true;
            }
                
        }

        float interactDistance = 10f;

        if (isDone)
        {
            if (Physics.Raycast(playerCameraTransform.position, playerCameraTransform.forward, out RaycastHit raycastHit, interactDistance))
            {
                Debug.Log(raycastHit.transform.gameObject);
               
                if (Input.GetKeyDown(KeyCode.K))
                {
                    raycastHit.transform.GetComponent<Renderer>().materials[1].SetColor("_EmissionColor", Color.blue);
                }
            }
        }
       

    }
}   