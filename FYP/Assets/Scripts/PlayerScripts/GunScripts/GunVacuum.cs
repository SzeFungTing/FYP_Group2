using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunVacuum : MonoBehaviour
{
    [SerializeField]
    private Transform gunPoint;

    [SerializeField]
    private float speed = 1.0f;

    [SerializeField]
    private Vector3 scaleChange = new Vector3(-0.01f, -0.01f, -0.01f);

    public GameObject gun;

    Animator anim;
    Animator gunAnim;

    CapsuleCollider _collider;

    private void Start()
    {
        gunAnim = gun.GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            gunAnim.SetBool("isInhale", true);
            gunAnim.speed = 1f;
        }
        else if (Input.GetMouseButtonUp(1))
        {
            gunAnim.SetBool("isInhale", false);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.transform.CompareTag("Target"))
        {
            anim = other.transform.gameObject.GetComponentInChildren<Animator>();
            if (other.transform.gameObject.layer == 7)
            {
                _collider = other.transform.GetChild(0).gameObject.GetComponentInChildren<CapsuleCollider>();
            }
            if (Input.GetMouseButton(1))
            {
                other.transform.gameObject.GetComponent<Target>().isVacuum = true;
                VacuumTarget(other);
            }
            else /*if (Input.GetMouseButtonUp(1))*/
            {
                other.transform.GetChild(1).gameObject.GetComponentInChildren<CapsuleCollider>().enabled = true;
                other.transform.gameObject.GetComponent<Target>().isVacuum = false;

                anim.ResetTrigger("Suck");
                anim.SetTrigger("Release");
            }
            if (anim.GetCurrentAnimatorStateInfo(0).IsName("ChickenNormal"))
            {
                anim.speed = 1;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.CompareTag("Target"))
        {
            anim = other.transform.gameObject.GetComponentInChildren<Animator>();
            other.transform.GetChild(1).gameObject.GetComponentInChildren<CapsuleCollider>().enabled = true;
            other.transform.gameObject.GetComponent<Target>().isVacuum = false;
            anim.ResetTrigger("Suck");
            anim.SetTrigger("Release");
        }
    }

    public void VacuumTarget(Collider other)
    {
        if (Vector3.Distance(other.transform.position, gunPoint.position) > 0.3f)
        {
            //old version (Vector3.MoveTowards)
            //float distance = speed * Time.deltaTime;
            //_hit.transform.position = Vector3.MoveTowards(_hit.transform.position, gunPoint.position, distance);

            //new version (change velocity)
            Vector3 direction = (gunPoint.position - other.transform.position).normalized;
            other.attachedRigidbody.velocity = direction * speed;

            other.transform.GetChild(1).gameObject.GetComponentInChildren<CapsuleCollider>().enabled = false;
            anim = other.transform.gameObject.GetComponentInChildren<Animator>();
            anim.speed = 1;
            anim.SetTrigger("Suck");
            anim.ResetTrigger("Release");

            
        }

        else
        {
            

            Destroy(other.transform.gameObject);
            //collision.transform.gameObject.SetActive(false);
        }
        if (Vector3.Distance(other.transform.position, gunPoint.position) < 1.0f)
        {
            other.transform.localScale += scaleChange;
        }
    }
}
