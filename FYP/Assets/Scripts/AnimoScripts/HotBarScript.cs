using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotBarScript : MonoBehaviour
{
    public GameObject selecter;
    Transform selecterPos;

    Transform hotBar1;
    Transform hotBar2;
    Transform hotBar3;
    Transform hotBar4;

    int currentPos = 1;
    // Start is called before the first frame update
    void Start()
    {
        hotBar1 = this.gameObject.transform.GetChild(0);
        hotBar2 = this.gameObject.transform.GetChild(1);
        hotBar3 = this.gameObject.transform.GetChild(2);
        hotBar4 = this.gameObject.transform.GetChild(3);
        

        selecterPos = selecter.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        



        if (Input.GetAxis("Mouse ScrollWheel") > 0f ) // forward, right
        {
            currentPos += 1;
            SelecterPos();
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0f) // backwards, left
        {
            currentPos -= 1;
            SelecterPos();
        }
    }

   
    void SelecterPos()
    {
        if (currentPos >= 5)
        {
            currentPos = 1;
        }
        else if (currentPos <= 0)
        {
            currentPos = 4;
        }

        if (currentPos == 1)
            selecterPos.position = hotBar1.position;
        else if (currentPos == 2)
            selecterPos.position = hotBar2.position;
        else if (currentPos == 3)
            selecterPos.position = hotBar3.position;
        else if (currentPos == 4)
            selecterPos.position = hotBar4.position;
    }
}
