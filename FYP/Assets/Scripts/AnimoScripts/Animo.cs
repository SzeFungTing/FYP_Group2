using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animo : MonoBehaviour
{
    public enum AnimoType {Chicken, Cat, Null};
    //public enum AnimoType2 {Chicken, Cat, Null};
    public AnimoType animoType1;
    public AnimoType animoType2;

    [SerializeField] private float originalXRotation = 0f;
    [SerializeField] private float originalZRotation = 0f;
    [SerializeField] private float rotateSpeed = 1;
    [SerializeField] private AudioClip[] wonderingSounds;

    private float currentYRotation;
    private float currentWRotation;
    private bool isPlayingSound = false;
    private bool hvPlayed = true;
    private int randomSound = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        currentYRotation = transform.rotation.y;
        currentWRotation = transform.rotation.w;
        
        if (transform.rotation.x != originalXRotation || transform.rotation.z != originalZRotation)
        {
            Quaternion targetRotation = new Quaternion(originalXRotation, currentYRotation, originalZRotation, currentWRotation);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);
        }

        if (!isPlayingSound)
        {
            StartCoroutine(PlayWonderingSound());
        }

        if (!hvPlayed)
        {
            randomSound = Random.Range(0, wonderingSounds.Length);
            AudioSource.PlayClipAtPoint(wonderingSounds[randomSound], transform.position);
            hvPlayed = true;
        }
    }

    IEnumerator PlayWonderingSound()
    {
        int waitForNextTime = Random.Range(5, 16);

        isPlayingSound = true;

        yield return new WaitForSeconds(waitForNextTime);
        hvPlayed = false;

        isPlayingSound = false;
    }
}
