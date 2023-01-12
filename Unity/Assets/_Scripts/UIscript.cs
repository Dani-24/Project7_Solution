using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class UIscript : MonoBehaviour
{
    Canvas canvas;

    public TextMeshProUGUI textPlayer1, textPlayer2;
    public GameObject player1Image, player2Image;
    public Slider playerSlider1, playerSlider2;

    private GameObject[] players; 

    void Start()
    {
        canvas = GetComponent<Canvas>();

        canvas.planeDistance = 0.31f;

        textPlayer1.text = "Player 1: " + PlayerManager.playerType1.GetComponent<PrefabPropierties>().characterName;
        player1Image.GetComponent<Image>().sprite = PlayerManager.playerType1.GetComponent<PrefabPropierties>().characterPhoto;

        playerSlider1.maxValue = 100;
        playerSlider1.minValue = 0;

        textPlayer2.text = "Player 2: " + PlayerManager.playerType2.GetComponent<PrefabPropierties>().characterName;
        player2Image.GetComponent<Image>().sprite = PlayerManager.playerType2.GetComponent<PrefabPropierties>().characterPhoto;

        player2Image.transform.localScale = new Vector3(-player2Image.transform.localScale.x, player2Image.transform.localScale.y, player2Image.transform.localScale.z);

        playerSlider2.maxValue = 100;
        playerSlider2.minValue = 0;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("TitleScene", LoadSceneMode.Single);
        }

        // Update HP bar
        players = GameObject.FindGameObjectsWithTag("Player");

        if (players.Length > 0)
        {
            playerSlider1.value = players[0].GetComponent<PrefabPropierties>().HP;
            playerSlider2.value = players[1].GetComponent<PrefabPropierties>().HP;
        }
    }
}
