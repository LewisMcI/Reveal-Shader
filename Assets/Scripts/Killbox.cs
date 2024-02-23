using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Killbox : MonoBehaviour
{
    [SerializeField] Vector3 worldPos;
    private void OnTriggerEnter(Collider other)
    {
        if (other.isTrigger)
            return;

        other.transform.position = worldPos;
    }
}
