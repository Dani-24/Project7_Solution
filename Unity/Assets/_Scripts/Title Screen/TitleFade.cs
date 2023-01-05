using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.TextCore.Text;

public class TitleFade : MonoBehaviour
{
    [Header("Fade In del Texto + Press Any Key para pasar a la siguiente Scene")]

    public float timeUntilFadeIn = 500;

    private TextMeshProUGUI text;

    private Color alpha;

    public float fadeSpeed = 1.0f;


    public TextMeshProUGUI titleText;

    private bool onTitle = true;

    public GameObject selectCharacterGameObject;

    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();

        alpha = new Color(255,255,255,0);
        text.color = alpha;
    }

    void Update()
    {
        if (onTitle)
        {
            if (timeUntilFadeIn > 0)
            {
                timeUntilFadeIn -= Time.deltaTime;
            }
            else
            {
                if (alpha.a < 1.0f)
                {
                    alpha.a += 0.5f * fadeSpeed * Time.deltaTime;
                    text.color = alpha;
                }
            }
        }
        else
        {
            alpha.a -= 0.5f * fadeSpeed * Time.deltaTime;
            text.color = alpha;
            titleText.color = alpha;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (onTitle)
            {
                Application.Quit();
            }
            else
            {
                ChangeScene("TitleScene");
            }
        }
        else if (Input.anyKey && onTitle == true)
        {
            onTitle = false;
            SelectCharacters();
        }

    }

    private void SelectCharacters()
    {
        SelectCharacter.start = true;
    }

    private void ChangeScene(string scene)
    {
        SceneManager.LoadScene(scene, LoadSceneMode.Single);

        //ChangeScene("BattleScene");
    }

}