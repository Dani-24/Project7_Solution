using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    // Start is called before the first frame update
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
