using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OndeEstou : MonoBehaviour
{
    public int fase = -1;
    public int primeiraFase = 3;
    public int bolaEmUso;

    //private float hortoSize = 5;
    //[SerializeField] private float aspect = 1.66f;

    [SerializeField] private GameObject uiManager, gameManager;
    public static OndeEstou instance;

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

        SceneManager.sceneLoaded += VerificaFase;
        bolaEmUso = PlayerPrefs.GetInt("BOLA_USO");
    }

    void VerificaFase(Scene cena, LoadSceneMode modo)
    {
        fase = SceneManager.GetActiveScene().buildIndex;

        if (fase >= primeiraFase)
        {
            Instantiate(uiManager);
            Instantiate(gameManager);
            // Ajusta a tela
            //Camera.main.projectionMatrix = Matrix4x4.Ortho(-hortoSize * aspect,
            //                                               hortoSize * aspect,
            //                                               -hortoSize,
            //                                               hortoSize,
            //                                               Camera.main.nearClipPlane,
            //                                               Camera.main.farClipPlane);
        }
    }
}
