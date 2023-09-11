using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TMP_Text _scoreText;
    private int _scoreSize = -1;
    public static UIManager Instance;
    public Action Score;
    public int Score_Text
    {
        get { return _scoreSize; }
        set
        {
            _scoreSize += value;
            _scoreText.text=_scoreSize.ToString();        
        }
    }
    void Start()
    {
        Instance = this;
        Score += ScoreTextWrite;
    }

    private void ScoreTextWrite()
    {
        Score_Text=1;
    }
}
