using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; } = null;
    public SessionState GameSessionState { get; private set; } = SessionState.STARTED;

    private const int GameSceneIndex = 0;

    public event Action OnSessionFinish = null;
    public event Action OnSessionStart = null;
    public static event Action OnDestroyScene = null;

    private void Awake()
    {
        if (Instance != null) throw new Exception("There can be only one GameManager object.");
        Instance = this;

        GameSessionState = SessionState.STARTED;

        //OnDestroyScene = null;
    }

    void Start()
    {
        Debug.Log(GameSessionState);
    }


    private void FinishGameSession() {

        Debug.Log("CALL!");

        if (GameSessionState != SessionState.FINISHED) {

            GameSessionState = SessionState.FINISHED;
            OnSessionFinish?.Invoke();
        }           
    }

    private void OnEnable()
    {
        Thrower.OnInventoryEmpty += FinishGameSession;
        Timer.OnTimeIsUp += FinishGameSession;
    }


    

    private void OnMouseDown()
    {

        if (GameSessionState == SessionState.STARTED) {

            GameSessionState = SessionState.PLAYED;
            OnSessionStart?.Invoke();
        
        }
        else if (GameSessionState == SessionState.FINISHED) {

            Debug.Log("New!");

            Instance = null;


            //OnDestroyScene += delegate () { SceneManager.LoadScene(GameSceneIndex); };

            OnDestroyScene();
            
            

            SceneManager.LoadScene(GameSceneIndex);
            //Application.LoadLevel(GameSceneIndex);

        }
    }



}


public enum SessionState : byte { 

    STARTED, PLAYED, PAUSED, FINISHED

}