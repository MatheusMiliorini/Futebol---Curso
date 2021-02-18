using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    // Bola
    [SerializeField]
    private GameObject[] bola;
    public int bolasNum = 2;
    public int bolasEmCena = 0;
    private Transform pos;
    public bool win;
    public int tiro;
    public bool jogoComecou;
    private bool adsShown;

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

        pos = GameObject.Find("StartPos").transform;
    }

    void Carrega(Scene cena, LoadSceneMode modo)
    {
        if (OndeEstou.instance.fase >= OndeEstou.instance.primeiraFase)
        {
            pos = GameObject.Find("StartPos").transform;
            StartGame();
        }
    }

    private void Start()
    {
        StartGame();
    }

    private void Update()
    {
        ScoreManager.instance.UpdateScore();
        UIManager.instance.UpdateUI();
        NasceBolas();
        if (bolasNum <= 0 && !win && jogoComecou)
        {
            GameOver();
        }

        if (win)
        {
            WinGame();
        }
    }

    void NasceBolas()
    {
        // Fases onde a câmera move
        if (OndeEstou.instance.fase >= 6)
        {
            // Limite esquerdo da câmera
            Transform e = GameObject.Find("E").transform;
            float offset = 0.5f;
            // Instancia apenas após a câmera voltar
            if (bolasNum > 0 && bolasEmCena == 0 && Camera.main.transform.position.x <= (e.position.x + offset))
            {
                Instantiate(bola[OndeEstou.instance.bolaEmUso], new Vector2(pos.position.x, pos.position.y), Quaternion.identity);
                bolasEmCena += 1;
                tiro = 0;
            }
        }
        else
        {
            if (bolasNum > 0 && bolasEmCena == 0)
            {
                Instantiate(bola[OndeEstou.instance.bolaEmUso], new Vector2(pos.position.x, pos.position.y), Quaternion.identity);
                bolasEmCena += 1;
                tiro = 0;
            }
        }
    }

    void GameOver()
    {
        UIManager.instance.GameOverUI();
        jogoComecou = false;
        if (!adsShown && ShouldShowAd())
        {
            UnityAds.instance.ShowAds();
            adsShown = true;
            PlayerPrefs.SetInt("AD", 0);
        }
    }

    private bool ShouldShowAd()
    {
        if (PlayerPrefs.HasKey("AD"))
        {
            if (PlayerPrefs.GetInt("AD") == 3)
            {
                return true;
            }
            else
            {
                PlayerPrefs.SetInt("AD", PlayerPrefs.GetInt("AD") + 1);
            }
        }
        else
        {
            PlayerPrefs.SetInt("AD", 1);
        }

        return false;
    }

    void WinGame()
    {
        UIManager.instance.WinGameUI();
        jogoComecou = false;
    }

    void StartGame()
    {
        win = false;
        jogoComecou = true;
        bolasNum = 2;
        bolasEmCena = 0;
        UIManager.instance.StartUI();
        adsShown = false;
    }
}
