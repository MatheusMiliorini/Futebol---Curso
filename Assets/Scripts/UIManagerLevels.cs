using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManagerLevels : MonoBehaviour
{
    [SerializeField] private Text moedasLevel;

    private void Start()
    {
        moedasLevel.text = ScoreManager.instance.GetMoedas().ToString();
    }
}
