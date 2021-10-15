using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreCounter : MonoBehaviour
{
    public static ScoreCounter Instance { get; private set; }

    [SerializeField] private TextMeshProUGUI _counterText = null;
    [SerializeField] private TextMeshProUGUI _differenceText = null;
    [SerializeField] private Animator _differenceTextAnim = null;

    private float _count = 0.0f;

    private const string ViewParam = "View";

    private void Awake()
    {
        if (Instance != null) throw new Exception("There can be only one ScoreCounter object.");
        Instance = this;
    }

    private void UpdateText()
    {
        _counterText.text = _count.ToString();

        _differenceTextAnim.SetBool(ViewParam, true);

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