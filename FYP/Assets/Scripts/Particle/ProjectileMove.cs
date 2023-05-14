
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMove : MonoBehaviour
{
    public float speed;
    public float fireRate;
    public GameObject muzzzlePrefab;
    public GameObject hitPrefab;

    // Start is called before the first frame update
    void Start()
    {
        //if (muzzzlePrefab)
        //{
        //    var muzzleVFX = Instantiate(muzzzlePrefab, transform.position, Quaternion.identity);
        //    muzzleVFX.transform.forward = gameObject.transform.forward;
        //}
    }

    // Update is called once per frame
    void Update()
    {
        if (speed != 0)
        {
            transform.position += transform.forward * (speed * Time.deltaTime);
        }
        else
        {
            Debug.Log("No Speed");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log("collision.name:" + collision.gameObject.name);

        speed = 0;

        ContactPoint contactPoint = collision.contacts[0];
        Quaternion ret = Quaternion.FromToRotation(Vector3.up, contactPoint.normal);
        Vector3 pos = contactPoint.point;

        //if (hitPrefab)
        //{
        //    var hitVFX = Instantiate(hitPrefab, pos, ret);
        //}
        if (collision.gameObject.TryGetComponent<DemonAI>(out DemonAI demonAI))
        {
            //Debug.Log("demonAI.hp--");
            demonAI.hp--;
        }

        Destroy(gameObject);
    }
}

