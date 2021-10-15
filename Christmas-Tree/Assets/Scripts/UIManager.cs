using System.Collections;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] Animator _instructionTextAnim = null;
    [SerializeField] Animator _continueTextAnim = null;

    private string StartParam = "Start";

    
    private void OnEnable()
    {
        if (GameManager.Instance == null) Debug.Log("GM is null");
        if (_instructionTextAnim == null) Debug.Log("TextAnim is null");

        GameManager.Instance.OnSessionStart += delegate () { _instructionTextAnim.SetTrigger(StartParam); };
        GameManager.Instance.OnSessionFinish += delegate () { _continueTextAnim.SetTrigger(StartParam); };

    }

}