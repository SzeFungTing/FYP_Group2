using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PenetrateWall : MonoBehaviour
{
    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.transform.TryGetComponent<BounceBack>(out BounceBack bounceBack))
        {
            Physics.IgnoreCollision(transform.GetComponent<CharacterController>(), hit.collider, true);

        }
    }
}
