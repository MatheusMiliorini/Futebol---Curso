using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CompraBola : MonoBehaviour
{
    public int idBolaBtn;
    public Text btnText;
    private Animator falido;

    public void CompraBolaBtn()
    {
        foreach (var bola in BolasShop.instance.bolasList)
        {
            if (bola.bolasId == idBolaBtn)
            {
                if (!bola.comprou)
                {
                    if (TemDinheiro(bola.bolasPreco))
                    {
                        bola.comprou = true;
                        BolasShop.instance.UpdateSprite(idBolaBtn);
                        ScoreManager.instance.PerdeMoedas(bola.bolasPreco);
                        UpdateCompraBnt();
                        // Atualiza a UI com a quantidade de dinheiro
                        GameObject.Find("Moedas").GetComponent<Text>().text = ScoreManager.instance.GetMoedas().ToString();
                    }
                    else
                    {
                        if (!falido)
                            falido = GameObject.FindGameObjectWithTag("falido").GetComponent<Animator>();
                        falido.Play("MOVE_UI_FALIDO");
                        StartCoroutine(EscondeFalido());
                    }
                }
                else
                {
                    UpdateCompraBnt();
                }
            }
        }
    }

    IEnumerator EscondeFalido()
    {
        yield return new WaitForSeconds(1);
        falido.Play("MOVE_UI_FALIDO_BAIXO");
    }

    bool TemDinheiro(int valor)
    {
        return ScoreManager.instance.GetMoedas() >= valor;
    }

    void UpdateCompraBnt()
    {
        btnText.text = "Usando";
        PlayerPrefs.SetInt("BOLA_ATUAL", idBolaBtn);

        foreach (var btn in BolasShop.instance.compraBtnList)
        {
            CompraBola cbScript = btn.GetComponent<CompraBola>();
            foreach (var bola in BolasShop.instance.bolasList)
            {
                if (bola.bolasId == idBolaBtn)
                {
                    BolasShop.instance.SalvaBolasLojaText(bola.bolasId, "Usando");
                    OndeEstou.instance.bolaEmUso = bola.bolasId;
                    PlayerPrefs.SetInt("BOLA_USO", bola.bolasId);
                }

                if (cbScript.idBolaBtn == bola.bolasId && bola.comprou && bola.bolasId != idBolaBtn)
                {
                    cbScript.btnText.text = "Us ar"; // Espaço por causa da fonte
                    BolasShop.instance.SalvaBolasLojaText(bola.bolasId, "Us ar");
                }
            }
        }
    }
}
