using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStart : MonoBehaviour
{
    [SerializeField]
    Vector3[] StartPositions;
    [SerializeField]
    Vector3[] StartRotations;

    public static int nPLayers;

    static Action<PlayerStart> OnPlayerAdded;
    static Action<PlayerStart> OnPlayerAddedAnswer;

    private void OnEnable()
    {
        OnPlayerAdded += ListenToAdded;
        OnPlayerAddedAnswer += ListenToAnswer;
    }
    private void OnDisable()
    {
        OnPlayerAdded -= ListenToAdded;
        OnPlayerAddedAnswer -= ListenToAnswer;
    }

   

    private void ListenToAdded(PlayerStart other)
    {
        if (other != this)
        {
            GetComponentInChildren<PlayerController>().SetOtherPlayer(other.transform);
            GetComponentInChildren<MovementController>().SetOtherPlayer(other.transform);
            OnPlayerAddedAnswer?.Invoke(this);
        }
    }
    private void ListenToAnswer(PlayerStart other)
    {
        if (other != this)
        {
            GetComponentInChildren<PlayerController>().SetOtherPlayer(other.transform);
            GetComponentInChildren<MovementController>().SetOtherPlayer(other.transform);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        transform.position = StartPositions[nPLayers];
        transform.eulerAngles = StartRotations[nPLayers];
        nPLayers++;
        OnPlayerAdded?.Invoke(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
