using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimoGenerator : MonoBehaviour
{
    [SerializeField] private Rigidbody[] animoPrefab;
    [SerializeField] private AudioClip generateSound;

    public float speed = 0.5f;

    public void Generate(int targetNum)
    {
        for (int i = 0; i < targetNum; i++)
        {
            int ranNum = Random.Range(0, animoPrefab.Length);
            Rigidbody rb = Instantiate(animoPrefab[ranNum], transform.position + Vector3.up * i * 3.5f, transform.rotation);
            AudioSource.PlayClipAtPoint(generateSound, transform.position + Vector3.up * i * 3.5f);
            rb.velocity = transform.up * speed;
        }
    }
}
