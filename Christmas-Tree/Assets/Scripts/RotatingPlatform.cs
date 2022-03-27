using System;
using System.Collections;
using UnityEngine;


public class RotatingPlatform : MonoBehaviour
{
    [SerializeField] private Vector3 _startRotationSpeed = new Vector3(0.0f, 2.5f, 0.0f);
    [SerializeField] private Vector3 _rotationSpeed = new Vector3(0.0f, 2.5f, 0.0f);
    [SerializeField] private float _maxSpeedIncrement = 1.5f;
    [SerializeField] private float _minSpeedIncrement = 0.75f;
    
    private int _stage = 0;

    public event Action OnPlatformRotate = null;

    private Stage _first = new Stage(1, 100.0f);
    private Stage _second = new Stage(2, 200.0f);
    private Stage _third = new Stage(3, 300.0f);


    private struct Stage {

        public int Number { get; private set; }
        public float ActivationScore { get; private set; }

        public Stage(int number, float activationScore) {

            Number = number;
            ActivationScore = activationScore;        
        }

        public bool TransitionAllowed(int currentStageNumber, float currentScore) {

            return Number > currentStageNumber && currentScore >= ActivationScore;       
        }

    }


    private void Awake()
    {
        _rotationSpeed = _startRotationSpeed;
        Debug.Log("First stage. Spped: " + _rotationSpeed);
    }

    private void OnEnable()
    {
        OnPlatformRotate += delegate() { transform.Rotate(Vector3.up, _rotationSpeed.y, Space.Self); };
        GameManager.Instance.OnSessionFinish += delegate { StopAllCoroutines(); FinishSatge(); };
    }


    public void CheckScore() {

        float score = ScoreCounter.Instance.GetCount();

        if (_first.TransitionAllowed(_stage, score) || _second.TransitionAllowed(_stage, score) || _third.TransitionAllowed(_stage, score))
        {
            NextStage();
        }
    }

    private IEnumerator Tilt(Vector3 targetEulerAngles, Action callback = null)
    {
        float time = 0.0f;
        Vector3 startEulerAngles = this.transform.eulerAngles;
        WaitForSeconds wait = new WaitForSeconds(0.1f);

        while (!SafeEquals(this.transform.eulerAngles.x, targetEulerAngles.x))
        {            
            time += 0.00001f;

            float angleX = Mathf.LerpAngle(startEulerAngles.x, targetEulerAngles.x, Time.time);
            float angleZ = Mathf.LerpAngle(startEulerAngles.z, targetEulerAngles.z, Time.time);

            this.transform.eulerAngles = new Vector3(angleX, transform.eulerAngles.y, angleZ);
            //yield return wait;

            yield return null;
        }

        Debug.Log("Rotation complete!");

        callback?.Invoke();

    }


    private void NextStage() {

        Vector3 targetEulerAngles = new Vector3(UnityEngine.Random.Range(0.0f, 3.5f), this.transform.eulerAngles.y, UnityEngine.Random.Range(0.0f, 3.5f));
        
        int direction = UnityEngine.Random.Range(0, 2);
        if (direction == 0)
        {
            direction = -1;
        }
        else {

            direction = 1;
        }

        Vector3 _targetSpeed = new Vector3(0.0f, UnityEngine.Random.Range(Mathf.Abs(_rotationSpeed.y) + _minSpeedIncrement, Mathf.Abs(_rotationSpeed.y) + _maxSpeedIncrement), 0.0f);
        _targetSpeed *= direction;

        Debug.Log("Next stage. Spped: " + _targetSpeed);

        StartCoroutine(Tilt(targetEulerAngles, delegate () { StartCoroutine(AccelerateRotation(_targetSpeed)); }));
        
        _stage++;
    }

    private void FinishSatge()
    {
        Vector3 _targetSpeed = _startRotationSpeed;
        _targetSpeed.y *= _rotationSpeed.normalized.y;
        //StartCoroutine(AccelerateRotation(_targetSpeed));

        Vector3 targetEulerAngles = new Vector3(0.0f, this.transform.eulerAngles.y, 0.0f);
        //StartCoroutine(Tilt(targetEulerAngles));

        StartCoroutine(Tilt(targetEulerAngles, delegate () { StartCoroutine(AccelerateRotation(_targetSpeed)); }));
    }


    private bool SafeEquals(float a, float b) {

        const float e = 0.1f;
        return Mathf.Abs(a - b) < e;
    }

    private IEnumerator AccelerateRotation(Vector3 _targetSpeed) {

        float Acceleration = 0.1f;

        if (_rotationSpeed.y > _targetSpeed.y) {

            Acceleration *= -1;
        }
                
        WaitForSeconds wait = new WaitForSeconds(0.1f);

        while (!SafeEquals(_rotationSpeed.y, _targetSpeed.y)) {

            _rotationSpeed.y += Acceleration;
            yield return wait;      
        }

        yield return null;
    
    }
    
    public void StartRotationAroundTree(Transform projectile) {
        
        projectile.parent = this.transform;
        CheckScore();
    }

    public void StopRotationAroundTree(Transform projectile) {

        projectile.parent = null;
        CheckScore();
    }
    
    private void FixedUpdate()
    {
        OnPlatformRotate?.Invoke();
    }

}

