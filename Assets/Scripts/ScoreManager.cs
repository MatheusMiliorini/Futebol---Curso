using UnityEngine;

public class ScoreManager : MonoBehaviour
{

    public static ScoreManager instance;
    public int moedas;
    public static string moedasKey = "MOEDAS";

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

        GameStartScoreM();
    }

    public void GameStartScoreM()
    {
        if (PlayerPrefs.HasKey(moedasKey))
        {
            moedas = PlayerPrefs.GetInt(moedasKey);
        }
        else
        {
            moedas = 100;
            PlayerPrefs.SetInt(moedasKey, moedas);
        }
    }

    public void UpdateScore()
    {
        moedas = PlayerPrefs.GetInt(moedasKey);
    }

    public void ColetaMoedas(int coin)
    {
        moedas += coin;
        SalvaMoedas(moedas);
    }

    public void PerdeMoedas(int coin)
    {
        moedas -= coin;
        SalvaMoedas(moedas);
    }

    public void SalvaMoedas(int coin)
    {
        PlayerPrefs.SetInt(moedasKey, coin);
    }

    public int GetMoedas()
    {
        return PlayerPrefs.GetInt(moedasKey);
    }
}
