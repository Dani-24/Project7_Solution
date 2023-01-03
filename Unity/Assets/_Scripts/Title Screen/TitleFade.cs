using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class TitleFade : MonoBehaviour
{
    [Header("Fade In del Texto + Press Any Key para pasar a la siguiente Scene")]

    public float timeUntilFadeIn = 500;

    private TextMeshProUGUI text;

    private Color alpha;

    public float fadeSpeed = 1.0f;

    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();

        alpha = new Color(255,255,255,0);
        text.color = alpha;
        
    }

    void Update()
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

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();

            // Esto hay que quitarlo si se hace una build. En el Editor cierra el juego
            //UnityEditor.EditorApplication.isPlaying = false;
        }
        else if (Input.anyKey)
        {
            SceneManager.LoadScene("BattleScene", LoadSceneMode.Single);
        }
    }
}
