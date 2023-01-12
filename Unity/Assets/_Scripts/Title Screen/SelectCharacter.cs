using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectCharacter : MonoBehaviour
{
    public static bool start;
    private bool startFinished = false;

    [Header("Cosas de la Scene")]
    public GameObject canvas;
    public GameObject cameraGO;

    public TextMeshProUGUI textSelectCharacters;

    [SerializeField]
    float charactersScale1, charactersScale2, charactersScale3, charactersScale4;

    [Header("Personajes a usar en el juego -> FBX tal cual")]
    public GameObject character1, character2, character3, character4;

    private GameObject go1, go2, go3, go4;

    [Header("Select Buttons")]
    public GameObject button1,button2, button3, button4;

    bool allCharactersSelected = false;

    AudioSource audioS;

    [Header("Select Audio")]
    [SerializeField]
    AudioClip amogusC, thorC, gokuC, dogeC;

    public void OnStart()
    {
        audioS = GetComponent<AudioSource>();

        PlayerManager.playerType1 = null;
        PlayerManager.playerType2 = null;

        go1 = Instantiate(character1);
        go1.transform.parent = transform;

        go2 = Instantiate(character2);
        go2.transform.parent = transform;

        go3 = Instantiate(character3);
        go3.transform.parent = transform;

        go4 = Instantiate(character4);
        go4.transform.parent = transform;

        go1.transform.localScale = new Vector3(charactersScale1, charactersScale1, charactersScale1);
        go2.transform.localScale = new Vector3(charactersScale2, charactersScale2, charactersScale2);
        go3.transform.localScale = new Vector3(charactersScale3, charactersScale3, charactersScale3);
        go4.transform.localScale = new Vector3(charactersScale4, charactersScale4, charactersScale4);

        start = false;
        startFinished = true;
    }

    void Update()
    {
        if (start)
        {
            cameraGO.GetComponent<Animator>().SetBool("TitleAnim", false);

            if (cameraGO.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("End"))
            {
                OnStart();
            }
        }
        
        if(startFinished)
        {
            textSelectCharacters.color = new Color(255, 255, 255, 255);

            Vector3 pos = canvas.transform.position;

            Vector3 XSeparation = new Vector3(30, 0, 0);
            Vector3 YPos = new Vector3(0, -40, 0);

            go1.transform.position = pos - 2.3f * XSeparation + YPos;
            go2.transform.position = pos - 0.6f * XSeparation + YPos;
            go3.transform.position = pos + 0.6f * XSeparation + YPos;
            go4.transform.position = pos + 2.3f * XSeparation + YPos;

            go1.transform.Rotate(new Vector3(0, 0.4f, 0));
            go2.transform.Rotate(new Vector3(0, 0.4f, 0));
            go3.transform.Rotate(new Vector3(0, 0.4f, 0));
            go4.transform.Rotate(new Vector3(0, 0.4f, 0));

            button1.SetActive(true);
            button2.SetActive(true);
            button3.SetActive(true);
            button4.SetActive(true);
        }

        if (allCharactersSelected)
        {
            SceneManager.LoadScene("BattleScene", LoadSceneMode.Single);
        }
    }

    public void SelectCh1()
    {
        if(PlayerManager.playerType1 == null)
        {
            PlayerManager.playerType1 = character1;
            audioS.clip = amogusC;
            audioS.Play();
        }
        else
        {
            PlayerManager.playerType2 = character1;
            audioS.clip = amogusC;
            audioS.Play();
            allCharactersSelected = true;
        }
    }

    public void SelectCh2()
    {
        if (PlayerManager.playerType1 == null)
        {
            PlayerManager.playerType1 = character2;
            audioS.clip = thorC;
            audioS.Play();
        }
        else
        {
            PlayerManager.playerType2 = character2;
            audioS.clip = thorC;
            audioS.Play();
            allCharactersSelected = true;
        }
    }

    public void SelectCh3()
    {
        if (PlayerManager.playerType1 == null)
        {
            PlayerManager.playerType1 = character3;
            audioS.clip = gokuC;
            audioS.Play();
        }
        else
        {
            PlayerManager.playerType2 = character3;
            audioS.clip = gokuC;
            audioS.Play();
            allCharactersSelected = true;
        }
    }

    public void SelectCh4()
    {
        if (PlayerManager.playerType1 == null)
        {
            PlayerManager.playerType1 = character4;
            audioS.clip = dogeC;
            audioS.Play();
        }
        else
        {
            PlayerManager.playerType2 = character4;
            audioS.clip = dogeC;
            audioS.Play();
            allCharactersSelected = true;
        }
    }
}
