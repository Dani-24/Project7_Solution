using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Script de Camara hecha para targetear la posición media entre los 2 jugadores.
    // Si solo hay un jugador le hace focus a ese.
    // Se aleja en función de la distancia entre los 2 jugadores para que no se salgan de la pantalla.

    // Odio SDL existiendo Unity

    [SerializeField]
    private GameObject[] players;

    [SerializeField]
    private float posX = 0.0f;
    [SerializeField]
    private float posZ = 0.0f;
    private float originalPosZ = 0.0f;

    [SerializeField]
    private float smoothTime = 0.25f;
    [SerializeField]
    private Vector3 velocity = Vector3.zero;

    float dist;

    [SerializeField]
    private float distanciaAlaQueSeDanDeHostias = 5.0f;

    private void Start()
    {
        posX = transform.position.x;
        originalPosZ = posZ = transform.position.z;
    }

    void Update()
    {
        // Check for 2 Players
        if(players.Length < 2)
        {
            players = GameObject.FindGameObjectsWithTag("Player");
        }

        if(players.Length > 0)
        {
            if(players.Length == 1)
            {
                posX = players[0].transform.position.x;
            }
            else if(players.Length == 2)
            {
                posX = (players[0].transform.position.x + players[1].transform.position.x) / 2;

                dist = Vector3.Distance(players[0].transform.position, players[1].transform.position);

                //Debug.Log("Distance between the 2 Players: " + dist);

                //Super Escalado de cámara que seguro que me felicita alguien si lo ve. Imagina usar exponenciales (Que seria lo suyo aquí) 
                if (dist > 25)
                {
                    posZ = -22;
                }
                else if (dist > 20)
                {
                    posZ = -19;
                }
                else if (dist > 15)
                {
                    posZ = -14;
                }
                else
                {
                    posZ = originalPosZ;
                }
            }
            
            Vector3 targetPos = new Vector3(posX, transform.position.y, posZ);
            transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref velocity, smoothTime);
        }

        // COMPROBACIONES DE LOS ATAQUES
        if (dist < distanciaAlaQueSeDanDeHostias)
        {
            // Player 1 Quick Attack
            if (players[0].GetComponentInParent<PlayerController>().quickAttacked == true && players[1].GetComponentInParent<PlayerController>().blocked == false)
            {
                players[1].GetComponent<PrefabPropierties>().HP -= players[0].GetComponentInParent<PrefabPropierties>().attackDMG;
            }

            // Player 2 Quick Attack
            if (players[1].GetComponentInParent<PlayerController>().quickAttacked == true && players[0].GetComponentInParent<PlayerController>().blocked == false)
            {
                players[0].GetComponent<PrefabPropierties>().HP -= players[1].GetComponentInParent<PrefabPropierties>().attackDMG;
            }

            // Player 1 Slow Attack
            if (players[0].GetComponentInParent<PlayerController>().slowAttacked == true && players[1].GetComponentInParent<PlayerController>().blocked == false)
            {
                players[1].GetComponent<PrefabPropierties>().HP -= players[0].GetComponentInParent<PrefabPropierties>().attackDMG * 2;
            }

            // Player 2 Slow Attack
            if (players[1].GetComponentInParent<PlayerController>().slowAttacked == true && players[0].GetComponentInParent<PlayerController>().blocked == false)
            {
                players[0].GetComponent<PrefabPropierties>().HP -= players[1].GetComponentInParent<PrefabPropierties>().attackDMG * 2;
            }

            // LOW ANIMATIONS

            // Player 1 Quick Attack
            if (players[0].GetComponentInParent<PlayerController>().lowQuickAttacked == true && players[1].GetComponentInParent<PlayerController>().jumped == false)
            {
                players[1].GetComponent<PrefabPropierties>().HP -= players[0].GetComponentInParent<PrefabPropierties>().attackDMG;
            }

            // Player 2 Quick Attack
            if (players[1].GetComponentInParent<PlayerController>().lowQuickAttacked == true && players[0].GetComponentInParent<PlayerController>().jumped == false)
            {
                players[0].GetComponent<PrefabPropierties>().HP -= players[1].GetComponentInParent<PrefabPropierties>().attackDMG;
            }

            // Player 1 Slow Attack
            if (players[0].GetComponentInParent<PlayerController>().lowSlowAttacked == true && players[1].GetComponentInParent<PlayerController>().jumped == false)
            {
                players[1].GetComponent<PrefabPropierties>().HP -= players[0].GetComponentInParent<PrefabPropierties>().attackDMG * 2;
            }

            // Player 2 Slow Attack
            if (players[1].GetComponentInParent<PlayerController>().lowSlowAttacked == true && players[0].GetComponentInParent<PlayerController>().jumped == false)
            {
                players[0].GetComponent<PrefabPropierties>().HP -= players[1].GetComponentInParent<PrefabPropierties>().attackDMG * 2;
            }
        }

        // Si la vida baja P1
        if (players[0].GetComponent<PrefabPropierties>().HP <= 0)
        {
            players[0].GetComponentInParent<PlayerController>().die = true;
            players[1].GetComponentInParent<PlayerController>().win = true;

            players[0].GetComponentInParent<PlayerController>().playing = false;
            players[1].GetComponentInParent<PlayerController>().playing = false;
        }
        // P2
        if (players[1].GetComponent<PrefabPropierties>().HP <= 0)
        {
            players[1].GetComponentInParent<PlayerController>().die = true;
            players[0].GetComponentInParent<PlayerController>().win = true;

            players[0].GetComponentInParent<PlayerController>().playing = false;
            players[1].GetComponentInParent<PlayerController>().playing = false;
        }

    }
}
