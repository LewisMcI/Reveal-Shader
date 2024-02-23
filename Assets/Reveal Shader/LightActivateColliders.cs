using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightActivateColliders : MonoBehaviour
{
    List<Collider> activeColliders = new List<Collider>();
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("EnableOnVisible"))
        {
            Debug.Log("Activated Object");
            other.isTrigger = false;
            activeColliders.Add(other);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (activeColliders.Contains(other))
        {
            other.isTrigger = true;
            activeColliders.Remove(other);
        }
    }
}
