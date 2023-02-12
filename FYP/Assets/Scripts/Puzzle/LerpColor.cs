using System.Collections.Generic;
using UnityEngine;

public class LerpColor : MonoBehaviour
{
    public Renderer hexagon;
    public GameObject all;
    public List<GameObject> list;
    public List<GameObject> P_list;
    public GameObject Light;

    int index = 0;

    [SerializeField] private Transform playerCameraTransform;

    int colorIdx = 0;
    float emitTime = 0;
    float emitInterval = 0.5f;
    float emissiveIntensity = 1.5f;

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
        RandomV();

        float interactDistance = 10f;
      

        if (isDone)
        {
            if (Physics.Raycast(playerCameraTransform.position, playerCameraTransform.forward, out RaycastHit raycastHit, interactDistance))
            {
                Debug.Log(raycastHit.transform.gameObject);
                Debug.DrawRay(playerCameraTransform.position, playerCameraTransform.TransformDirection(Vector3.forward) * 1000, Color.white);

                if (index != 9)
                {
                    if (Input.GetKeyDown(KeyCode.K))
                    {

                            if (raycastHit.transform.gameObject == list[index] && !P_list.Contains(raycastHit.transform.gameObject))
                            {
                                Debug.Log("t1: " + raycastHit.transform.gameObject);
                                Debug.Log("g1: " + list[index]);
                                raycastHit.transform.GetComponent<Renderer>().materials[1].SetColor("_EmissionColor", emissiveIntensity * new Color(12 / 255f, 28 / 255f, 191 / 255f));
                                P_list.Add(raycastHit.transform.gameObject);
                                index++;
                                emitTime = Time.time;
                            }
                            else if (raycastHit.transform.gameObject != list[index])
                            {
                                Debug.Log("t2: " + raycastHit.transform.gameObject);
                                Debug.Log("g2: " + list[index]);
                                foreach (Transform all in transform)
                                {
                                    all.GetComponent<Renderer>().materials[1].SetColor("_EmissionColor", emissiveIntensity * Color.red);

                                }
                                RandomV();
                            }
                        

                    }
                }
                else
                {
                    if (Time.time > emitTime + emitInterval+2 )
                    {
                        foreach (Transform all in transform)
                        {
                            all.GetComponent<Renderer>().materials[1].SetColor("_EmissionColor", emissiveIntensity * new Color(12 / 255f, 28 / 255f, 191 / 255f));

                        }
                    }
                   
                }
               
            }
        }
       

    }

    private void RandomV()
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


                all.transform.GetChild(num).GetComponent<Renderer>().materials[1].SetColor("_EmissionColor", emissiveIntensity * Color.red);
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
    }
}   