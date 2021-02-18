using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BolasShop : MonoBehaviour
{
    public static BolasShop instance;
    public List<Bolas> bolasList = new List<Bolas>();
    public GameObject baseItemGO;
    public Transform conteudo; // Onde ficam as bolas
    private List<BaseItem> baseItens = new List<BaseItem>();
    public List<GameObject> compraBtnList = new List<GameObject>();

    private void Awake()
    {
        if (!instance)
        {
            instance = this;
        }
    }

    private void Start()
    {
        //PlayerPrefs.DeleteAll();
        FillList();
    }

    void FillList()
    {
        foreach (Bolas b in bolasList)
        {
            GameObject itemBola = Instantiate(baseItemGO);
            itemBola.transform.SetParent(conteudo, false);

            // BaseItem é o que contém os GOs
            BaseItem baseItem = itemBola.GetComponent<BaseItem>();
            baseItem.bolaId = b.bolasId;
            baseItem.bolaPreco.text = b.bolasPreco.ToString();
            baseItem.btnCompra.GetComponent<CompraBola>().idBolaBtn = b.bolasId;

            // Bola comprada no PlayerPrefs
            if (PlayerPrefs.GetInt("BTN_" + b.bolasId) == 1)
            {
                b.comprou = true;
            }

            // Bola em uso
            if (PlayerPrefs.HasKey("BTNS_" + b.bolasId) && b.comprou)
            {
                baseItem.btnCompra.GetComponent<CompraBola>().btnText.text = PlayerPrefs.GetString("BTNS_" + b.bolasId);
            }

            if (b.comprou)
            {
                baseItem.bolaSprite.sprite = Resources.Load<Sprite>("Bolas/" + b.nomeSprite);
                baseItem.bolaPreco.text = "Comprado!";

                // Primeira vez que acessa a loja, se não tier setado o BTNS_ID é porque nunca clicou em nada
                if (!PlayerPrefs.HasKey("BTNS_" + b.bolasId))
                {
                    baseItem.btnCompra.GetComponent<CompraBola>().btnText.text = "Usando";
                }
            }
            else
            {
                baseItem.bolaSprite.sprite = Resources.Load<Sprite>("Bolas/" + b.nomeSprite + "_cinza");
            }

            baseItens.Add(baseItem);
            compraBtnList.Add(baseItem.btnCompra);
        }
    }

    public void UpdateSprite(int bolaId)
    {
        foreach (var bi in baseItens)
        {
            if (bi.bolaId == bolaId)
            {
                foreach (var bola in bolasList)
                {
                    if (bola.bolasId == bolaId)
                    {
                        if (bola.comprou)
                        {
                            bi.bolaSprite.sprite = Resources.Load<Sprite>("Bolas/" + bola.nomeSprite);
                            bi.bolaPreco.text = "Comprado!";
                            SalvaBolasLojaInfo(bi.bolaId);
                        }
                        else
                        {
                            bi.bolaSprite.sprite = Resources.Load<Sprite>("Bolas/" + bola.nomeSprite + "_cinza");
                        }
                    }
                }
            }
        }
    }

    void SalvaBolasLojaInfo(int idBola)
    {
        for (int i = 0; i < bolasList.Count; i++)
        {
            BaseItem baseItem = baseItens[i];
            if (baseItem.bolaId == idBola)
            {
                PlayerPrefs.SetInt("BTN_" + idBola, bolasList[i].comprou ? 1 : 0);
            }
        }
    }

    public void SalvaBolasLojaText(int idBola, string s)
    {
        for (int i = 0; i < bolasList.Count; i++)
        {
            BaseItem baseItem = baseItens[i];
            if (baseItem.bolaId == idBola)
            {
                PlayerPrefs.SetString("BTNS_" + idBola, s);
            }
        }
    }
}
