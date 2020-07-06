﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour
{

    Text scoreText;
    GameSession gameSession;

    // Start is called before the first frame update
    void Start()
    {
        scoreText = GetComponent<Text>();
        gameSession = FindObjectOfType<GameSession>();        
    }

    // Update is called once per frame
    void Update()
    {
        // setting the text property of the scoreText variable
        scoreText.text = gameSession.GetScore().ToString();
    }
}
