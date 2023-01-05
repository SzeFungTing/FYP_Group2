using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Jetpack : MonoBehaviour
{
    public Image FillBar;
    public float maxFual = 4f;
    private float curFuel;
    public float thrustForce = 0.5f;
    public Rigidbody rigid;
    public Transform groundedTransform;

    public ShopManager shopManager;
   
    // Start is called before the first frame update
    void Start()
    {
        curFuel = maxFual;
    }

    // Update is called once per frame
    void Update()
    {
        if (shopManager.isBought)
        {
            FillBar.fillAmount = curFuel / maxFual;

            if (Input.GetAxis("Jump") > 0f &&curFuel > 0f)
            {
                curFuel -= Time.deltaTime;
                rigid.AddForce(rigid.transform.up * thrustForce, ForceMode.Impulse);
            }
            else if (Physics.Raycast(groundedTransform.position, Vector3.down, 0.05f, LayerMask.GetMask("Grounded"))&& curFuel < maxFual)
            {
                Debug.Log("adding");
                curFuel += Time.deltaTime;
            }
        }
        
    }
}
