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

    [SerializeField]
    private Vector3 scaleChange = new Vector3(-0.01f, -0.01f, -0.01f);

    private RaycastHit _hit;

    Animator anim;

    CapsuleCollider _collider;

    GameObject grandChild;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    //void Update()
    //{
    //    Ray ray = new Ray(gunPoint.position, gunPoint.forward);
    //    Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * distanceOfRaycast, Color.white);

<<<<<<< Updated upstream
    //    if (Physics.Raycast(ray, out _hit, distanceOfRaycast))
    //    {
    //        if (_hit.transform.CompareTag("Target"))
    //        {
    //            if (Input.GetMouseButton(1))
    //            {
    //                _hit.transform.gameObject.GetComponent<Target>().isVacuum = true;
    //                VacuumTarget(_hit);
    //            }
    //            else if (Input.GetMouseButtonUp(1))
    //            {
    //                _hit.transform.gameObject.GetComponent<Target>().isVacuum = false;
    //                anim.SetBool("isSucking", false);
    //                anim.SetBool("isReleasing", true);
    //            }
    //            if (anim.GetCurrentAnimatorStateInfo(0).IsName("ChickenNormal"))
    //            {
    //                anim.speed = 1;
    //            }
    //        }
    //    }
    //}
=======
        if (Physics.Raycast(ray, out _hit, distanceOfRaycast))
        {
            if (_hit.transform.CompareTag("Target"))
            {
                anim = _hit.transform.gameObject.GetComponentInChildren<Animator>();
                if (_hit.transform.gameObject.layer == 7)
                {
                    _collider = _hit.transform.GetChild(1).gameObject.GetComponentInChildren<CapsuleCollider>();
                }
                if (Input.GetMouseButton(1))
                {
                    _hit.transform.gameObject.GetComponent<Target>().isVacuum = true;
                    VacuumTarget(_hit);
                }
                else if (Input.GetMouseButtonUp(1))
                {
                    _hit.transform.gameObject.GetComponent<Target>().isVacuum = false;
                    anim.SetBool("isSucking", false);
                    anim.SetBool("isReleasing", true);
                }
                if (anim.GetCurrentAnimatorStateInfo(0).IsName("ChickenNormal"))
                {
                    anim.speed = 1;
                }
            }
        }
    }
>>>>>>> Stashed changes

    

    //public void VacuumTarget(RaycastHit _hit)
    //{
    //    if (Vector3.Distance(_hit.transform.position, gunPoint.position) > 0.3f)
    //    {
    //        //old version (Vector3.MoveTowards)
    //        //float distance = speed * Time.deltaTime;
    //        //_hit.transform.position = Vector3.MoveTowards(_hit.transform.position, gunPoint.position, distance);

<<<<<<< Updated upstream
    //        //new version (change velocity)
    //        Vector3 direction = (gunPoint.position - _hit.transform.position).normalized;
    //        _hit.rigidbody.velocity = direction * speed;

    //        anim = _hit.transform.gameObject.GetComponentInChildren<Animator>();
    //        anim.speed = 2;
    //        anim.SetBool("isSucking", true);
    //        anim.SetBool("isReleasing", false);
    //    }
    //    else
    //    {
    //        Destroy(_hit.transform.gameObject);
    //        //_hit.transform.gameObject.SetActive(false);
    //    }
    //    if (Vector3.Distance(_hit.transform.position, gunPoint.position) < 1.0f)
    //    {
    //        _hit.transform.localScale += scaleChange;
    //    }
    //}
=======
            anim.speed = 2;
            anim.SetBool("isSucking", true);
            anim.SetBool("isReleasing", false);
            _hit.transform.GetChild(1).gameObject.GetComponentInChildren<CapsuleCollider>().enabled = false;
        }
        else
        {
            Destroy(_hit.transform.gameObject);
            _hit.transform.GetChild(1).gameObject.GetComponentInChildren<CapsuleCollider>().enabled = true;
            //_hit.transform.gameObject.SetActive(false);
        }
        if (Vector3.Distance(_hit.transform.position, gunPoint.position) < 1.0f)
        {
            _hit.transform.localScale += scaleChange;
        }
    }
>>>>>>> Stashed changes
}
