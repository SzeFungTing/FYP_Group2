using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationScript : MonoBehaviour
{
    //Animator anim;

    //Vector3 normalSize = new Vector3(1, 1, 1);
    Vector3 enlargeSclae = new Vector3(0.01f, 0.01f, 0.01f);

    // Start is called before the first frame update
    void Start()
    {
        //anim = gameObject.GetComponentInChildren<Animator>();
        //anim.SetTrigger("isBorn");


        transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);


        //anim = this.gameObject.GetComponentInChildren<Animator>();
        //anim.SetTrigger("isBorn");
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.localScale.x <= 0.35f)
        {

            transform.localScale += enlargeSclae;
        }
    }
}
