using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceBack : MonoBehaviour
{
    [SerializeField] float bounceForce;

    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.transform.TryGetComponent<Animo>(out Animo animo))
    //    {
    //        Debug.Log("BounceBack");
    //        Rigidbody otherRB = collision.rigidbody;

    //        otherRB.AddExplosionForce(bounceForce, collision.contacts[0].point, 5);
    //    }
    //}

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.TryGetComponent<Animo>(out Animo animo))
        {
            Debug.Log("BounceBack");
            Rigidbody otherRB = other.GetComponent<Rigidbody>();
            //Vector3 collisionPoint = other.ClosestPoint(other.transform.position);
            Vector3 collisionPoint = other.gameObject.GetComponent<Collider>().ClosestPointOnBounds(transform.position);

            otherRB.AddExplosionForce(bounceForce, collisionPoint, 5);
        }
    }
}
