using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreCounter : MonoBehaviour
{
    public static ScoreCounter Instance { get; private set; } = null;

    [SerializeField] private TextMeshProUGUI _counterText = null;
    [SerializeField] private TextMeshProUGUI _differenceText = null;
    [SerializeField] private TextMeshProUGUI _bestScoreText = null;
    
    [SerializeField] private Animator _counterTextAnim = null;
    [SerializeField] private Animator _differenceTextAnim = null;

    private float _count = 0.0f;

    private const string ViewParam = "View";
    private const string FinishParam = "Finish";

    public float Score { get { return _count; } private set { _count = value; } }
    
    private void Awake()
    {
        if (Instance != null) throw new Exception("There can be only one ScoreCounter object.");
        Instance = this;

        _bestScoreText.gameObject.SetActive(false);
        GameManager.OnDestroyScene += delegate () { Instance = null; };
    }

    private void OnEnable()
    {
        GameManager.Instance.OnSessionFinish += delegate() { 
            
            _counterTextAnim.SetTrigger(FinishParam); 
            _differenceText.gameObject.SetActive(false); 
        };
    }

    private void UpdateText()
    {
        _counterText.text = _count.ToString();
        _differenceTextAnim.SetTrigger(ViewParam);
    }

    public void ViewBestResult(string animStartParam)
    {
        _bestScoreText.text = DataManager.BestScore.ToString();
        _bestScoreText.gameObject.SetActive(true);
        _bestScoreText.TryGetComponent<Animator>(out Animator bestScoreAnim);
        bestScoreAnim.SetTrigger(animStartParam);
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