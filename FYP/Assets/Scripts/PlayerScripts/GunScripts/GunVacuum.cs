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

    Animator anim;

    private void OnTriggerStay(Collider other)
    {
        if (other.transform.CompareTag("Target"))
        {
            if (Input.GetMouseButton(1))
            {
                other.transform.gameObject.GetComponent<Target>().isVacuum = true;
                VacuumTarget(other);
            }
            else /*if (Input.GetMouseButtonUp(1))*/
            {
                other.transform.gameObject.GetComponent<Target>().isVacuum = false;
                anim.SetBool("isSucking", false);
                anim.SetBool("isReleasing", true);
            }
            if (anim.GetCurrentAnimatorStateInfo(0).IsName("ChickenNormal"))
            {
                anim.speed = 1;
            }
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

            anim = other.transform.gameObject.GetComponentInChildren<Animator>();
            anim.speed = 2;
            anim.SetBool("isSucking", true);
            anim.SetBool("isReleasing", false);
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
