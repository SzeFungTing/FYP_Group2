using UnityEngine;

public class LerpColor : MonoBehaviour
{
    public Renderer hexagon;
    public GameObject all;
    int count = 0;
    int num = 0;

    void Start()
    {
        foreach (Transform all in transform){
            all.GetComponent<Renderer>().materials[1].SetColor("_EmissionColor", Color.black);
        }
             
       
    }

    private void Update()
    {
        while(count==10){
            num=Random.Range(0,1);
            count++;
        }
        
        if (num == 1)
            {

            foreach (Transform all in transform)
            {
                all.GetComponent<Renderer>().materials[1].SetColor("_EmissionColor", Color.red);


                //hexagon.GetComponent<Renderer>().materials[1].SetColor("_EmissionColor", Color.red);
            }

        }
        
    }
}