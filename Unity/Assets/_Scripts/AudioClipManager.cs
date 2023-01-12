using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioClipManager : MonoBehaviour
{
    AudioSource audioSource;

    [Header("Toda la música aquí. Se playea random luego")]
    public AudioClip[] clips;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();

        int randomNumber = Random.Range(0, clips.Length);

        for (int i = 0; i < clips.Length; i++)
        {
            if (i == randomNumber) {
                audioSource.clip = clips[i];
            }
        }

        if(audioSource.clip != null)
        {
            audioSource.Play();
        }
    }
}
