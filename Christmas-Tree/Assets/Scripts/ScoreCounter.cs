using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreCounter : MonoBehaviour
{
    public static ScoreCounter Instance { get; private set; }

    [SerializeField] private TextMeshProUGUI _text = null;
    private float _count = 0.0f;

    private void Awake()
    {
        if (Instance != null) throw new Exception("There can be only one ScoreCounter object.");
        Instance = this;
    }

    private void UpdateText()
    {
        _text.text = _count.ToString();
    }

    public void Add(float count) {

        _count += count;
        UpdateText();
    }

    public void Substract(float count) {

        _count -= count;
        UpdateText();
    }

    public float GetCount() {

        return _count;
    }


   
}