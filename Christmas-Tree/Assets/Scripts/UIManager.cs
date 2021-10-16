using System.Collections;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] Animator _instructionTextAnim = null;
    [SerializeField] Animator _continueTextAnim = null;
    [SerializeField] Animator _scoreLabelAnim = null;

    [SerializeField] Timer _timer = null;
    [SerializeField] public float _calculationWaitingSeconds = 0.25f;

    private string StartParam = "Start";
    private string ViewParam = "View";

    private WaitForSeconds _calculationWaiting = null;

    private void Awake()
    {
        _calculationWaiting = new WaitForSeconds(_calculationWaitingSeconds);
    }

    private void OnEnable()
    {
        if (GameManager.Instance == null) Debug.Log("GM is null");
        if (_instructionTextAnim == null) Debug.Log("TextAnim is null");

        GameManager.Instance.OnSessionStart += delegate () { _instructionTextAnim.SetTrigger(StartParam); };
        GameManager.Instance.OnSessionFinish += delegate () { _continueTextAnim.SetTrigger(StartParam); };
        GameManager.Instance.OnSessionFinish += delegate () { _scoreLabelAnim.SetTrigger(ViewParam); };

        GameManager.Instance.OnSessionFinish += delegate () { StartCoroutine(AddTimeBonus()); };

    }

    private IEnumerator AddTimeBonus() {


        while (_timer.LeftTime > 0.0f) {

            _timer.Substract(1.0f);

            ScoreCounter.Instance.Add(1.0f);

            yield return _calculationWaiting;
        
        }

        yield return null;


    }

}