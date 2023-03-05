using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fajro : MonoBehaviour
{
    public enum FajroType { Bear, Cat, Chicken, Deer, Demon, Dog, Dragon, Peacock, Pig, Rabbit };
    public FajroType fajroType1;
    public FajroType fajroType2;

    public float destoryTime = 300f;

    float destoryCountDown;
    

    // Start is called before the first frame update
    void Start()
    {
        destoryCountDown = destoryTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (destoryCountDown > 0)
        {
            destoryCountDown -= Time.deltaTime;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
