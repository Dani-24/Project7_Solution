using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public GameObject playerControllerPrefab;

    public static GameObject playerType1;
    public static GameObject playerType2;

    [Header("Players Prefabs for testing without using Title Scene")]
    public bool useTestingGameObjects = false;

    public GameObject playerType1Testing;
    public GameObject playerType2Testing;

    [Header("Players Start Positions")]
    public float player1SpawnPos = -5.0f;
    public float player2SpawnPos = 5.0f;

    private void Awake()
    {
        if (useTestingGameObjects == true)
        {
            playerType1 = playerType1Testing;
            playerType2 = playerType2Testing;
        }
    }

    void Start()
    {
        Vector3 spawnPos = transform.position;

        spawnPos.Set(transform.position.x + player1SpawnPos, transform.position.y, transform.position.z);

        GameObject go;
        go = Instantiate(playerControllerPrefab, spawnPos, Quaternion.identity) as GameObject;
        go.transform.parent = transform;
        go.GetComponent<SetCharacter>().character = playerType1;

        spawnPos.Set(transform.position.x + player2SpawnPos, transform.position.y, transform.position.z);

        GameObject go2;
        go2 = Instantiate(playerControllerPrefab, spawnPos, Quaternion.identity) as GameObject;
        go2.transform.parent = transform;
        go2.GetComponent<SetCharacter>().character = playerType2;
    }
}
