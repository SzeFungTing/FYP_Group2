using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnProjectile : MonoBehaviour
{
    public GameObject firePoint;
    public List<GameObject> vfx = new List<GameObject>();
    public RotateToMouse rotateToMouse;

    private GameObject effectToSpawn;

    float droneStayTime = 2;
    float currentDroneStayTime = 0;

    float timeToFire = 0;

    // Start is called before the first frame update
    void Start()
    {
        effectToSpawn = vfx[0];

        firePoint.transform.parent.gameObject.SetActive(false);
        currentDroneStayTime = droneStayTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Q) && Time.time >= timeToFire)
        {
            firePoint.transform.parent.gameObject.SetActive(true);
            currentDroneStayTime = droneStayTime;
            timeToFire = Time.time + 1 / effectToSpawn.GetComponent<ProjectileMove>().fireRate;
            SpawnVFX();
        }
        currentDroneStayTime -= Time.deltaTime;
        if (currentDroneStayTime <= 0)
        {
            firePoint.transform.parent.gameObject.SetActive(false);
            currentDroneStayTime = droneStayTime;
        }
    }

    void SpawnVFX()
    {
        GameObject vfx;

        if (firePoint != null)
        {
            vfx = Instantiate(effectToSpawn, firePoint.transform.position, Quaternion.identity);
            if (rotateToMouse != null)
            {
                vfx.transform.localRotation = rotateToMouse.GetRotation();
            }
        }
        else
        {
            Debug.Log("No Fire Point");
        }
    }
}