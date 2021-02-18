using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    [System.Serializable]
    public class Level
    {
        public string levelText;
        public bool habilitado;
        public int desbloqueado;
        public bool txtAtivo;
    }

    public GameObject botao;
    public Transform localBtn;
    public List<Level> levelList;

    void CriaLista()
    {
        foreach (var level in levelList)
        {
            // Cria o botão
            GameObject btn = Instantiate(botao);
            btn.transform.SetParent(localBtn, false);
            // Altera o botão
            BotaoLevel btnL = btn.GetComponent<BotaoLevel>();
            btnL.levelTxtBtn.text = level.levelText;
            // Verifica se o level está habilitado
            if (PlayerPrefs.GetInt("FASE_" + level.levelText) == 1)
            {
                level.desbloqueado = 1;
                level.habilitado = true;
                level.txtAtivo = true;
            }
            // Desbloqueia
            btnL.desbloqueadoBtn = level.desbloqueado;
            Button btnLBtn = btnL.GetComponent<Button>();
            // Botão clicável
            btnLBtn.interactable = level.habilitado;
            // Mostra o TXT do botão
            btnL.GetComponentInChildren<Text>().enabled = level.txtAtivo;
            // Clique para entrar na fase
            btnLBtn.onClick.AddListener(() => ClickLevel("FASE_" + level.levelText));
        }
    }

    void ClickLevel(string level)
    {
        SceneManager.LoadScene(level);
    }

    private void Awake()
    {
        Destroy(GameObject.Find("UIManager(Clone)"));
        Destroy(GameObject.Find("GameManager(Clone)"));
    }

    // Start is called before the first frame update
    void Start()
    {
        CriaLista();
    }
}
