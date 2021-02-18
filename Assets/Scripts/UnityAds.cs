using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UnityAds : MonoBehaviour, IUnityAdsListener
{

    public static UnityAds instance;
    private string gameId = "4015757";
    private Button adsBtn;
    public bool adsBtnAcionado;

    private void Awake()
    {
        if (!instance)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        Advertisement.AddListener(this);
        Advertisement.Initialize(gameId);

        SceneManager.sceneLoaded += PegaBtn;
    }

    void PegaBtn(Scene cena, LoadSceneMode modo)
    {
        if (cena.buildIndex >= OndeEstou.instance.primeiraFase)
        {
            adsBtn = GameObject.Find("AdsBtn").GetComponent<Button>();
            adsBtn.onClick.AddListener(() =>
            {
                if (Advertisement.IsReady("rewardedVideo"))
                {
                    Advertisement.Show("rewardedVideo");
                }
            });
        }
    }

    public void ShowAds()
    {
        if (Advertisement.IsReady("video"))
        {
            Advertisement.Show("video");
        }
    }

    public void OnUnityAdsDidFinish(string surfacingId, ShowResult showResult)
    {
        if (showResult == ShowResult.Finished)
        {
            ScoreManager.instance.ColetaMoedas(1000);
            adsBtnAcionado = true;
        }
    }

    public void OnUnityAdsReady(string placementId)
    {
        //throw new System.NotImplementedException();
    }

    public void OnUnityAdsDidError(string message)
    {
        //throw new System.NotImplementedException();
    }

    public void OnUnityAdsDidStart(string placementId)
    {
        //throw new System.NotImplementedException();
    }
}
