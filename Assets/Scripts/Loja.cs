using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Loja : MonoBehaviour
{
    public void VaiPraLoja()
    {
        SceneManager.LoadScene("LOJA_BOLAS");
    }
}
