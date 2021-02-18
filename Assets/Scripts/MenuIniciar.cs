using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuIniciar : MonoBehaviour
{

    private Animator animBack, animInfo;
    private bool backAberto, infoAberto;
    private AudioSource musica;
    [SerializeField] private Sprite somLigado, somDesligado;
    private Button btnSom;

    private void Awake()
    {
        animBack = GameObject.Find("BACK").GetComponent<Animator>();
        animInfo = GameObject.Find("InfoImg").GetComponent<Animator>();
        musica = GameObject.Find("AudioManager").GetComponent<AudioSource>();
        btnSom = GameObject.Find("SOM").GetComponent<Button>();

        CheckMute();
    }

    public void Jogar()
    {
        SceneManager.LoadScene("FASES");
    }

    public void AnimaMenu()
    {
        if (backAberto)
        {
            animBack.Play("MOVE_UI_BAIXO");
        }
        else
        {
            animBack.Play("MOVE_UI");
        }
        backAberto = !backAberto;
    }

    public void AnimaInfo()
    {
        if (infoAberto)
        {
            animInfo.Play("ANIMA_INFO_BAIXO");
        }
        else
        {
            animInfo.Play("ANIMA_INFO");
        }
        infoAberto = !infoAberto;
    }

    public void AlternaMusica()
    {
        musica.mute = !musica.mute;
        UpdateMuteSprite();
        PlayerPrefs.SetInt("MUTE", musica.mute ? 1 : 0);
    }

    private void UpdateMuteSprite()
    {
        if (musica.mute)
        {
            btnSom.image.sprite = somDesligado;
        }
        else
        {
            btnSom.image.sprite = somLigado;
        }
    }

    private void CheckMute()
    {
        if (PlayerPrefs.HasKey("MUTE"))
        {
            musica.mute = PlayerPrefs.GetInt("MUTE") == 1;
            UpdateMuteSprite();
        }
    }

    public void Facebook()
    {
        Application.OpenURL("https://www.facebook.com/matheus.miliorini/");
    }
}
