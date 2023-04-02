using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceBack : MonoBehaviour
{
    [SerializeField] float bounceForce;
    public GameObject cube;



    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.TryGetComponent<Animo>(out Animo animo))
        {
            Debug.Log("BounceBack");
            Rigidbody otherRB = collision.rigidbody;

            //1
            //otherRB.AddExplosionForce(bounceForce, collision.contacts[0].point, 5);
            //Debug.Log("collision.contacts[0].point: " + collision.contacts[0].point);


            //2
            //var lastVelocity = otherRB.velocity;
            //var speed = lastVelocity.magnitude;
            //var direction = Vector2.Reflect(lastVelocity.normalized, collision.contacts[0].normal);

            //otherRB.velocity = direction * Mathf.Max(speed, 2f);

            //3
            Vector3 direction = collision.transform.position - transform.position;
            Vector3 planeNormal = transform.GetChild(0).position - transform.position;
            //Debug.Log("Vector3.Dot(direction, planeNormal: " + Vector3.Dot(direction, planeNormal));
            //if (Vector3.Angle(direction, planeNormal) > 90)             //go out
            if(Vector3.Dot(direction, planeNormal) < 0)
            {
                Debug.Log(transform.name + " out");
                Debug.Log("collision.collider: " + collision.collider );

                Physics.IgnoreCollision(GetComponent<Collider>(), collision.collider, false);
            }
            else                                                        //go inside
            {
                Debug.Log(transform.name + " in");
                Debug.Log("collision.collider: " + collision.collider);

                Physics.IgnoreCollision(GetComponent<Collider>(), collision.collider, true);
            }
        }
        else
        {
            //Debug.Log("else");
            //Debug.Log("collision.collider: " + collision.collider);

            Physics.IgnoreCollision(GetComponent<Collider>(), collision.collider, true);
            //Physics.IgnoreCollision(GetComponent<Collider>(), collision.transform.GetComponent<CharacterController>(), true);
        }
    }



    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.transform.TryGetComponent<Animo>(out Animo animo))
    //    {
    //        Vector3 direction = other.transform.position - transform.position;
    //        Vector3 planeNormal = transform.GetChild(0).position - transform.position;
    //        //Debug.Log("direction: " + direction);
    //        if (Vector3.Angle(direction, planeNormal) > 90)             //go out
    //        {
    //            Debug.Log(transform.name + " out");
    //            Debug.Log("BounceBack");
    //            Rigidbody otherRB = other.GetComponent<Rigidbody>();
    //            //Vector3 collisionPoint = other.ClosestPoint(other.transform.position);
    //            Vector3 collisionPoint = other.ClosestPointOnBounds(transform.position);
    //            Debug.Log("collisionPoint: " + collisionPoint);
    //            cube.transform.position = collisionPoint;

    //            //1
    //            //otherRB.AddExplosionForce(bounceForce, collisionPoint, 5);

    //            //2
    //            //float speed = otherRB.velocity.magnitude;
    //            //Debug.Log("speed: " + speed);

    //            //Debug.Log("otherRB.velocity.normalized: " + otherRB.velocity.normalized);
    //            //Vector3 d = Vector3.Reflect(otherRB.velocity.normalized, collisionPoint);
    //            //Debug.Log("d: " + d);

    //            //Debug.Log("otherRB.velocity: " + otherRB.velocity);
    //            //otherRB.velocity = d * Mathf.Max(speed, 0f);
    //            //Debug.Log("otherRB.velocity: " + otherRB.velocity);

    //            //3
    //            Physics.IgnoreCollision(GetComponent<Collider>(), other, false);



    //        }
    //        else                                                        //go inside
    //        {
    //            Debug.Log(transform.name + " in");

    //            //3
    //            Physics.IgnoreCollision(GetComponent<Collider>(), other, true);


    //        }


    //    }
    //    else
    //    {
    //        Debug.Log("else");

    //        //3
    //        Physics.IgnoreCollision(GetComponent<Collider>(), other, true);
    //    }
    //}
}
