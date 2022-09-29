using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField]
    private Transform gunPoint;

    [SerializeField]
    private int distanceOfRaycast = 10;

    [SerializeField]
    private float speed = 1.0f;

    private RaycastHit _hit;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = new Ray(gunPoint.position, gunPoint.forward);
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * distanceOfRaycast, Color.white);

        if (Physics.Raycast(ray, out _hit, distanceOfRaycast))
        {
            if (_hit.transform.CompareTag("Target"))
            {
                if (Input.GetMouseButton(1))
                {
                    AttractTarget(_hit);
                    Debug.Log("hit");
                }
                else if (Input.GetMouseButtonUp(1))
                {
                    _hit.rigidbody.useGravity = true;
                }
            }
        }
    }

    private void AttractTarget(RaycastHit _hit)
    {
        if (Vector3.Distance(_hit.transform.position, gunPoint.position) > 0.3f)
        {
            _hit.rigidbody.useGravity = false;
            float distance = speed * Time.deltaTime;
            _hit.transform.position = Vector3.MoveTowards(_hit.transform.position, gunPoint.position, distance);
        }
        else
        {
            Destroy(_hit.transform.gameObject);
            //_hit.transform.gameObject.SetActive(false);
        }
    }
}
