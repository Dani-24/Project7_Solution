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

    [Header("Attack Cooldowns")]
    float kickCooldownP1 = 0.30f;
    float kickCooldownP2 = 0.30f;

    [SerializeField]
    float kickCooldown = 0.5f;

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

        if(kickCooldownP1 > 0.0f)
        {
            kickCooldownP1 -= Time.deltaTime;
        }

        if (kickCooldownP2 > 0.0f)
        {
            kickCooldownP2 -= Time.deltaTime;
        }

        // COMPROBACIONES DE LOS ATAQUES
        if (dist < distanciaAlaQueSeDanDeHostias)
        {
            // Player 1 Quick Attack
            if (players[0].GetComponentInParent<PlayerController>().quickAttacked == true && players[1].GetComponentInParent<PlayerController>().blocked == false && kickCooldownP1 <= 0.0f)
            {
                players[1].GetComponent<PrefabPropierties>().HP -= players[0].GetComponentInParent<PrefabPropierties>().attackDMG;
                kickCooldownP1 = kickCooldown;
            }

            // Player 2 Quick Attack
            if (players[1].GetComponentInParent<PlayerController>().quickAttacked == true && players[0].GetComponentInParent<PlayerController>().blocked == false && kickCooldownP2 <= 0.0f)
            {
                players[0].GetComponent<PrefabPropierties>().HP -= players[1].GetComponentInParent<PrefabPropierties>().attackDMG;
                kickCooldownP2 = kickCooldown;
            }

            // Player 1 Slow Attack
            if (players[0].GetComponentInParent<PlayerController>().slowAttacked == true && players[1].GetComponentInParent<PlayerController>().blocked == false && kickCooldownP1 <= 0.0f)
            {
                players[1].GetComponent<PrefabPropierties>().HP -= players[0].GetComponentInParent<PrefabPropierties>().attackDMG * 2;
                kickCooldownP1 = kickCooldown;
            }

            // Player 2 Slow Attack
            if (players[1].GetComponentInParent<PlayerController>().slowAttacked == true && players[0].GetComponentInParent<PlayerController>().blocked == false && kickCooldownP2 <= 0.0f)
            {
                players[0].GetComponent<PrefabPropierties>().HP -= players[1].GetComponentInParent<PrefabPropierties>().attackDMG * 2;
                kickCooldownP2 = kickCooldown;
            }

            // LOW ANIMATIONS

            // Player 1 Quick Attack
            if (players[0].GetComponentInParent<PlayerController>().lowQuickAttacked == true && players[1].GetComponentInParent<PlayerController>().jumped == false && kickCooldownP1 <= 0.0f)
            {
                players[1].GetComponent<PrefabPropierties>().HP -= players[0].GetComponentInParent<PrefabPropierties>().attackDMG;
                kickCooldownP1 = kickCooldown;
            }

            // Player 2 Quick Attack
            if (players[1].GetComponentInParent<PlayerController>().lowQuickAttacked == true && players[0].GetComponentInParent<PlayerController>().jumped == false && kickCooldownP2 <= 0.0f)
            {
                players[0].GetComponent<PrefabPropierties>().HP -= players[1].GetComponentInParent<PrefabPropierties>().attackDMG;
                kickCooldownP2 = kickCooldown;
            }

            // Player 1 Slow Attack
            if (players[0].GetComponentInParent<PlayerController>().lowSlowAttacked == true && players[1].GetComponentInParent<PlayerController>().jumped == false && kickCooldownP1 <= 0.0f)
            {
                players[1].GetComponent<PrefabPropierties>().HP -= players[0].GetComponentInParent<PrefabPropierties>().attackDMG * 2;
                kickCooldownP1 = kickCooldown;
            }

            // Player 2 Slow Attack
            if (players[1].GetComponentInParent<PlayerController>().lowSlowAttacked == true && players[0].GetComponentInParent<PlayerController>().jumped == false && kickCooldownP2 <= 0.0f)
            {
                players[0].GetComponent<PrefabPropierties>().HP -= players[1].GetComponentInParent<PrefabPropierties>().attackDMG * 2;
                kickCooldownP2 = kickCooldown;
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

        // HAcer que los players se miren siempre
        if (players[0].transform.position.x < players[1].transform.position.x)
        {
            players[0].GetComponentInParent<PlayerController>().playerDir = new Vector3(1, 0, 0);
            players[1].GetComponentInParent<PlayerController>().playerDir = new Vector3(-1, 0, 0);
        }
        else
        {
            players[0].GetComponentInParent<PlayerController>().playerDir = new Vector3(-1, 0, 0);
            players[1].GetComponentInParent<PlayerController>().playerDir = new Vector3(1, 0, 0);
        }
    }
}
