﻿using System.Collections;
using UnityEngine;
using TMPro;


public class Timer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _timerUIText = null;
    
    [SerializeField] private float _leftTime = 0.0f;
    private float _gameTime = 0.0f;


    private string ConvertTimeFormat(float time)
    {
        float minutes = 0.0f, seconds = time;

        while (seconds > 59)
        {
            seconds -= 60;
            minutes++;
        }

        return string.Format("{0:D2}:{1:D2}", (int)minutes, (int)seconds);
    }


    void Update()
    {


        _timerUIText.text = ConvertTimeFormat(_leftTime);
        _gameTime += Time.deltaTime;



        if (_gameTime >= 1) {

            _leftTime--;
            _gameTime = 0;       
        }


    }
}
