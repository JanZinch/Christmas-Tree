using System.Collections;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Animator _instructionTextAnim = null;
    [SerializeField] private Animator _continueTextAnim = null;
    [SerializeField] private Animator _scoreLabelAnim = null;
    [SerializeField] private Animator _bestScoreLabelAnim = null;
    [SerializeField] private Animator _newRecordLabelAnim = null;
    
    [Space]
    [SerializeField] private Timer _timer = null;
    [SerializeField] public float _calculationWaitingSeconds = 0.25f;

    private const string StartParam = "Start";
    private const string ViewParam = "View";

    private WaitForSeconds _calculationWaiting = null;

    public bool CalculationsPerformed { get; private set; } = false;

    private void Awake()
    {
        _calculationWaiting = new WaitForSeconds(_calculationWaitingSeconds);
    }

    private void OnEnable()
    {
        //if (GameManager.Instance == null) Debug.Log("GM is null");
        //if (_instructionTextAnim == null) Debug.Log("TextAnim is null");

        GameManager.Instance.OnSessionStart += delegate () { _instructionTextAnim.SetTrigger(StartParam); };
        GameManager.Instance.OnSessionFinish += delegate () { _scoreLabelAnim.SetTrigger(ViewParam); };
        GameManager.Instance.OnSessionFinish += delegate () { StartCoroutine(AddTimeBonus()); };
    }

    private IEnumerator AddTimeBonus() {
        
        while (_timer.LeftTime > 0.0f) {

            _timer.Substract(1.0f);
            ScoreCounter.Instance.Add(1.0f);
            yield return _calculationWaiting;        
        }

        _continueTextAnim.SetTrigger(StartParam);
        
        if (DataManager.BestScore < ScoreCounter.Instance.GetCount())
        {
            DataManager.BestScore = ScoreCounter.Instance.GetCount();
            _newRecordLabelAnim.SetTrigger(StartParam);
        }
        else
        {
            ScoreCounter.Instance.ViewBestResult(StartParam);
            _bestScoreLabelAnim.SetTrigger(StartParam);
        }
        
        
        
        CalculationsPerformed = true;

        yield return null;


    }

}