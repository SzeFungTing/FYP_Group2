using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animo : MonoBehaviour
{
    public enum AnimoType {Bear, Cat, Chicken, Deer, Demon, Dog, Dragon, Peacock, Pig, Rabbit, Null};
    //public enum AnimoType2 {Chicken, Cat, Null};
    public AnimoType animoType1;
    public AnimoType animoType2;

    [SerializeField] private float originalXRotation = 0f;
    [SerializeField] private float originalZRotation = 0f;
    [SerializeField] private float rotateSpeed = 1;
    [SerializeField] private AudioClip[] wonderingSounds;
    [SerializeField] private float killTime = 6000f;

    private float currentYRotation;
    private float currentWRotation;
    private bool isPlayingSound = false;
    private bool hvPlayed = true;
    private int randomSound = 0;
    private float killTimeCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (killTimeCount >= killTime)
        {
            Destroy(gameObject);
        }

        currentYRotation = transform.rotation.y;
        currentWRotation = transform.rotation.w;
        
        if (transform.rotation.x != originalXRotation || transform.rotation.z != originalZRotation)
        {
            Quaternion targetRotation = new Quaternion(originalXRotation, currentYRotation, originalZRotation, currentWRotation);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);
        }

        if (!isPlayingSound && !IsWonderingSoundsEmpty())
        {
            StartCoroutine(PlayWonderingSound());
        }

        if (!hvPlayed)
        {
            randomSound = Random.Range(0, wonderingSounds.Length);
            AudioSource.PlayClipAtPoint(wonderingSounds[randomSound], transform.position);
            hvPlayed = true;
        }

        if (gameObject.tag != "Pet")
        {
            killTimeCount += Time.deltaTime;
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

    private bool IsWonderingSoundsEmpty()
    {
        if (wonderingSounds == null || wonderingSounds.Length == 0)
        {
            return true;
        }
            
        for (int i = 0; i < wonderingSounds.Length; i++)
        {
            if (wonderingSounds[i] != null)
            {
                return false;
            }
        }

        return true;
    }
}
