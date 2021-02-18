using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    public static UIManager instance;
    private Text pontosUI, bolasUI;
    private GameObject losePainel;
    private GameObject winPainel;
    private GameObject pausePainel;
    private Button pauseBtn, pauseBtnReturn;
    private Button menuLose, novamenteLose;
    private Button menuWin, novamenteWin, avancar;
    public int moedasNumAntes, moedasNumDepois;

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

        SceneManager.sceneLoaded += Carrega;

        PegaDados();
    }

    void Carrega(Scene cena, LoadSceneMode modo)
    {
        PegaDados();
    }

    void PegaDados()
    {
        if (OndeEstou.instance.fase >= OndeEstou.instance.primeiraFase)
        {
            pontosUI = GameObject.Find("PontosUI").GetComponent<Text>();
            bolasUI = GameObject.Find("BolasUI").GetComponent<Text>();
            losePainel = GameObject.Find("LosePainel");
            winPainel = GameObject.Find("WinPainel");

            pausePainel = GameObject.Find("PausePainel");
            pauseBtn = GameObject.Find("PAUSE").GetComponent<Button>();
            pauseBtnReturn = GameObject.Find("PauseR").GetComponent<Button>();

            menuLose = GameObject.Find("MenuLose").GetComponent<Button>();
            novamenteLose = GameObject.Find("NovamenteLose").GetComponent<Button>();

            menuWin = GameObject.Find("MenuWin").GetComponent<Button>();
            novamenteWin = GameObject.Find("NovamenteWin").GetComponent<Button>();
            avancar = GameObject.Find("Avancar").GetComponent<Button>();

            // Pause
            pauseBtn.onClick.AddListener(Pause);
            pauseBtnReturn.onClick.AddListener(Retorno);
            // Lose
            novamenteLose.onClick.AddListener(JogarNovamente);
            menuLose.onClick.AddListener(Levels);
            // Win
            novamenteWin.onClick.AddListener(JogarNovamente);
            menuWin.onClick.AddListener(Levels);
            avancar.onClick.AddListener(ProximaFase);
            // Moedas
            moedasNumAntes = PlayerPrefs.GetInt(ScoreManager.moedasKey);
        }
    }

    public void UpdateUI()
    {
        pontosUI.text = ScoreManager.instance.moedas.ToString();
        bolasUI.text = GameManager.instance.bolasNum.ToString();
        moedasNumDepois = ScoreManager.instance.moedas;
    }

    public void StartUI()
    {
        StartCoroutine(DesabilitaPaineis());
    }

    IEnumerator DesabilitaPaineis()
    {
        yield return new WaitForSeconds(0.001f);
        losePainel.SetActive(false);
        winPainel.SetActive(false);
        pausePainel.SetActive(false);
    }

    public void GameOverUI()
    {
        losePainel.SetActive(true);
    }

    public void WinGameUI()
    {
        winPainel.SetActive(true);
    }

    void Pause()
    {
        pausePainel.SetActive(true);
        pausePainel.GetComponent<Animator>().Play("MOVE_UI_PAUSE");
        Time.timeScale = 0;
    }

    void Retorno()
    {
        pausePainel.GetComponent<Animator>().Play("MOVE_UI_PAUSE_RETORNO");
        Time.timeScale = 1;
        StartCoroutine(EsperaRetorno());
    }

    IEnumerator EsperaRetorno()
    {
        yield return new WaitForSeconds(0.8f);
        pausePainel.SetActive(false);
    }

    void JogarNovamente()
    {
        if (!GameManager.instance.win)
        {
            // Se não pagou, perde as moedinhas :)
            if (!UnityAds.instance.adsBtnAcionado)
            {
                int resultado = moedasNumDepois - moedasNumAntes;
                ScoreManager.instance.PerdeMoedas(resultado);
            }
            else
            {
                UnityAds.instance.adsBtnAcionado = false;
            }
        }
        SceneManager.LoadScene(OndeEstou.instance.fase);
    }

    void Levels()
    {
        if (!GameManager.instance.win)
        {
            int resultado = moedasNumDepois - moedasNumAntes;
            ScoreManager.instance.PerdeMoedas(resultado);
        }
        SceneManager.LoadScene("FASES");
    }

    void ProximaFase()
    {
        if (GameManager.instance.win)
        {
            int aux = OndeEstou.instance.fase + 1;
            if (aux < SceneManager.sceneCountInBuildSettings)
            {
                SceneManager.LoadScene(aux);
            }
            // Vai para a lista de fases
            else
            {
                SceneManager.LoadScene("FASES");
            }
        }
    }
}
