using System;
using UnityEngine;
using TMPro;


public class Timer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _timerUIText = null;
    
    [SerializeField] private float _leftTime = 0.0f;
    private float _gameTime = 0.0f;

    public static event Action OnTimeIsUp = null;

    private bool _timeIsUp = false;

    public float LeftTime { get { return _leftTime; } private set { _leftTime = value; } }


    private void Start()
    {
        _timerUIText.text = ConvertTimeFormat(_leftTime);
    }


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

    public void Substract(float seconds) {

        _leftTime -= seconds;
        _timerUIText.text = ConvertTimeFormat(_leftTime);
    }


    void Update()
    {
        if (GameManager.Instance == null) return;

        if (!_timeIsUp && GameManager.Instance.GameSessionState == SessionState.PLAYED) {

            if (_leftTime <= 0)
            {
                _timeIsUp = true;
                OnTimeIsUp?.Invoke();
            }

            _timerUIText.text = ConvertTimeFormat(_leftTime);
            _gameTime += Time.deltaTime;



            if (_gameTime >= Time.timeScale)
            {

                _leftTime--;
                _gameTime = 0;
            }




        }


        


    }
}
