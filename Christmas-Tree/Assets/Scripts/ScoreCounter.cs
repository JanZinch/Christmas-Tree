using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreCounter : MonoBehaviour
{
    public static ScoreCounter Instance { get; private set; }

    [SerializeField] private TextMeshProUGUI _counterText = null;
    [SerializeField] private TextMeshProUGUI _differenceText = null;

    [SerializeField] private Animator _counterTextAnim = null;
    [SerializeField] private Animator _differenceTextAnim = null;

    private float _count = 0.0f;

    private const string ViewParam = "View";
    private const string FinishParam = "Finish";

    private void Awake()
    {
        if (Instance != null) throw new Exception("There can be only one ScoreCounter object.");
        Instance = this;

        GameManager.OnDestroyScene += delegate () { Instance = null; };
    }

    private void OnEnable()
    {
        GameManager.Instance.OnSessionFinish += delegate() { _counterTextAnim.SetTrigger(FinishParam); };
    }

    private void UpdateText()
    {
        _counterText.text = _count.ToString();

        // _differenceTextAnim.SetBool(ViewParam, true);

        
        _differenceTextAnim.SetTrigger(ViewParam);

    }

   


    public void Add(float count) {

        _count += count;
        _differenceText.text = "+" + count;

        UpdateText();
    }

    public void Substract(float count) {

        _count -= count;
        _differenceText.text = "-" + count;
        UpdateText();
    }

    public float GetCount() {

        return _count;
    }


   
}