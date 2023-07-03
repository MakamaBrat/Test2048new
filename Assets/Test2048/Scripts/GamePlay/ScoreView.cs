using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreView : MonoBehaviour
{
    [SerializeField] private MainValues MainValues;
    [SerializeField] private TextMeshProUGUI textMeshProUGUI;

    private void OnEnable()
    {
        MainValues.OnScoreChanged += setTextValue;
    }

    private void OnDestroy()
    {
        MainValues.OnScoreChanged -= setTextValue;
    }

    public void setTextValue(int score)
    {
        textMeshProUGUI.text ="Score:" + score.ToString();
    }
}
