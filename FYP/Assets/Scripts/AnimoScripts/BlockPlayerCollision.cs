using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockPlayerCollision : MonoBehaviour
{
    public CharacterController characterController;
    public CapsuleCollider characterBlockerCollider;

    // Start is called before the first frame update
    void Start()
    {
        Physics.IgnoreCollision(characterController, characterBlockerCollider, true);
    }

}
