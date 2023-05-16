using System.Collections.Generic;
using UnityEngine;

public class LerpColor : MonoBehaviour
{
    public Renderer hexagon;
    public GameObject all;
    public List<GameObject> list;
    public List<GameObject> P_list;
    public GameObject Light;
    public GameObject PortalTrigger;
    public ParticleSystem Portal;

    public Wall wall;


    int index = 0;

    [SerializeField] private Transform playerCameraTransform;

    [SerializeField] int puzzleNum = 3;

    int colorIdx = 0;
    float emitTime = 0;
    float emitInterval = 0.5f;
    float emissiveIntensity = 1.5f;

    bool isDone;
    bool toReset;

    void Start()
    {

        foreach (Transform all in transform)                        //close all puzzle light
        {
            all.GetComponent<Renderer>().materials[1].SetColor("_EmissionColor", Color.black);
        }
        Light.SetActive(false);
        PortalTrigger.SetActive(false);
    }

    private void Update()
    {
        if (wall && wall.istriggered || (!wall && !isDone))       //start animation
        {
            RandomV();
            
        }

        float interactDistance = 10f;


        if (isDone)
        {
            Debug.DrawLine(Camera.main.transform.position, Camera.main.ScreenPointToRay(new Vector2(Screen.width / 2, Screen.height / 2)).GetPoint(50), Color.green);

            //Debug.Log(raycastHit.transform.gameObject);
            //Debug.DrawRay(playerCameraTransform.position, playerCameraTransform.TransformDirection(Vector3.forward) * 1000, Color.white);




            if (index != puzzleNum)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    if (Physics.Raycast(/*playerCameraTransform.position, playerCameraTransform.forward*/Camera.main.ScreenPointToRay(new Vector2(Screen.width / 2, Screen.height / 2)), out RaycastHit raycastHit, interactDistance))
                    {

                        if (P_list.Contains(raycastHit.transform.gameObject))       //player select same position
                        {
                            return;
                        }
                        else if (raycastHit.transform.gameObject == list[index] && !P_list.Contains(raycastHit.transform.gameObject))       //corret
                        {
                            Debug.Log("raycastHit.transform.gameObject1: " + raycastHit.transform.gameObject);
                            Debug.Log("list[index]1: " + list[index]);
                            raycastHit.transform.GetComponent<Renderer>().materials[1].SetColor("_EmissionColor", emissiveIntensity * new Color(12 / 255f, 28 / 255f, 191 / 255f));
                            P_list.Add(raycastHit.transform.gameObject);
                            index++;
                            emitTime = Time.time;
                        }
                        else if (raycastHit.transform.gameObject != list[index])        //incorret
                        {
                            Debug.Log("raycastHit.transform.gameObject2: " + raycastHit.transform.gameObject);
                            Debug.Log("list[index]2: " + list[index]);
                            foreach (Transform all in transform)
                            {
                                all.GetComponent<Renderer>().materials[1].SetColor("_EmissionColor", emissiveIntensity * Color.red);
                            }
                            emitTime = Time.time;
                            toReset = true;


                        }

                    }
                }
              
            }
            else
            {
                if (Time.time > emitTime + emitInterval + 2)        //end puzzle
                {
                    foreach (Transform all in transform)
                    {
                        all.GetComponent<Renderer>().materials[1].SetColor("_EmissionColor", emissiveIntensity * new Color(12 / 255f, 28 / 255f, 191 / 255f));
                        all.GetComponent<MeshCollider>().enabled = false;
                        all.GetComponent<Rigidbody>().isKinematic = false;
                        all.GetComponent<Rigidbody>().useGravity = true;
                        PortalTrigger.SetActive(true);
                        Portal.Play();

                        Destroy(this.gameObject, 2f);
                    }
                }

            }




        }

        if (toReset && Time.time > emitTime + emitInterval + 2)     //reset
        {
            foreach (Transform all in transform)
            {
                all.GetComponent<Renderer>().materials[1].SetColor("_EmissionColor", emissiveIntensity * Color.black);
            }
            list = new List<GameObject>();
            P_list = new List<GameObject>();
            colorIdx = 0;
            index = 0;
            isDone = false;
            Light.SetActive(false);
            RandomV();

            toReset = false;
            emitTime = Time.time;
        }


    }

    private void RandomV()
    {
        

        if (colorIdx < puzzleNum)
        {
            if (Time.time > emitTime + emitInterval)
            {
                var num = Random.Range(0, all.transform.childCount);

                if (list.Contains(all.transform.GetChild(num).gameObject))
                {
                    //list.Remove(all.transform.GetChild(num).gameObject);
                    //Debug.Log("hi");
                    num = Random.Range(0, all.transform.childCount);

                }
                else if (colorIdx < puzzleNum)
                {

                    all.transform.GetChild(num).GetComponent<Renderer>().materials[1].SetColor("_EmissionColor", emissiveIntensity * new Color(12 / 255f, 28 / 255f, 191 / 255f));
                    list.Add(all.transform.GetChild(num).gameObject);

                    emitTime = Time.time;
                    colorIdx++;

                }

            }
        }
        if (colorIdx == puzzleNum && Time.time > emitTime + emitInterval && !isDone)
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

        if (wall && isDone)
        {
            Destroy(wall);
        }
    }

   
}