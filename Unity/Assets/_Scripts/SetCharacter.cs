using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetCharacter : MonoBehaviour
{
    public GameObject character;

    void Start()
    {
        GameObject go;
        go = Instantiate(character, transform.position, Quaternion.identity) as GameObject;
        go.transform.parent = transform;
        go.tag = "Player";
    }
}
