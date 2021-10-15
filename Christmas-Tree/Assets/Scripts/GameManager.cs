using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance = null;
    public SessionState GameSessionState { get; private set; } = SessionState.STARTED;

    private const int GameSceneIndex = 0;

    public event Action OnSessionFinish = null;
    public event Action OnSessionStart = null;

    private void Awake()
    {
        if (Instance != null) throw new Exception("There can be only one GameManager object.");
        Instance = this;
    }

    void Start()
    {

    }


    private void FinishGameSession() {

        GameSessionState = SessionState.FINISHED;
        OnSessionFinish?.Invoke();    
    }

    private void OnEnable()
    {
        Thrower.OnInventoryEmpty += FinishGameSession;
    }


    

    private void OnMouseDown()
    {

        if (GameSessionState == SessionState.STARTED) {

            GameSessionState = SessionState.PLAYED;
            OnSessionStart?.Invoke();
        
        }
        else if (GameSessionState == SessionState.FINISHED) {

            SceneManager.LoadScene(GameSceneIndex);       
        }
    }



}


public enum SessionState : byte { 

    STARTED, PLAYED, PAUSED, FINISHED

}