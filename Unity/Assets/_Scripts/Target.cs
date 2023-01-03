using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        var attack = other.GetComponent<Attack>();
        if (attack != null)
        {
            Debug.Log(other.name);
            GetComponentInParent<PlayerController>().OnHit(other.transform);
        }
    }
}
