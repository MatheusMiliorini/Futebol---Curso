using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    // Músicas
    public AudioClip[] clips;
    public AudioSource musicaBG;
    // Sons Fx
    public AudioClip[] clipsFx;
    public AudioSource sonsFx;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Update()
    {
        // Dentro do Update para tocar quando uma parar
        if (!musicaBG.isPlaying)
        {
            musicaBG.clip = GetRandom();
            musicaBG.Play();
        }
    }

    AudioClip GetRandom()
    {
        return clips[Random.Range(0, clips.Length)];
    }

    public void SonsFXToca(int index)
    {
        sonsFx.clip = clipsFx[index];
        sonsFx.Play();
    }
}
