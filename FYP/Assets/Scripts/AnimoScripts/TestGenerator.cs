using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestGenerator : MonoBehaviour
{
    [SerializeField] private Rigidbody[] animoPrefab;
    [SerializeField] private AudioClip generateSound;

    public float generationInterval = 0.5f;
    public float speed = 0.5f;

    public void TestGenerate()
    {
        for (int i = 0; i < animoPrefab.Length; i++)
        {
            Rigidbody rb = Instantiate(animoPrefab[i], transform.position + Vector3.up * i * 1.3f, transform.rotation);
            AudioSource.PlayClipAtPoint(generateSound, transform.position + Vector3.up * i * 1.3f);
            rb.velocity = transform.up * speed;
        }
    }
}
