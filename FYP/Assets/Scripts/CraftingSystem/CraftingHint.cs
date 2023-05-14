using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingHint : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            UIScripts.instance.craftingHint.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            UIScripts.instance.craftingHint.SetActive(false);
        }
    }
}
