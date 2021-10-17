using System;
using System.Collections;
using UnityEngine;


public class RotatingPlatform : MonoBehaviour
{
    [SerializeField] private Vector3 _startRotationSpeed = new Vector3(0.0f, 2.5f, 0.0f);
    [SerializeField] private Vector3 _rotationSpeed = new Vector3(0.0f, 2.5f, 0.0f);
    [SerializeField] private float _maxSpeedIncrement = 1.5f;
    [SerializeField] private float _minSpeedIncrement = 0.5f;


    //[SerializeField] private List<Transform> _attachedObjects = null;

    private int _stage = 0;

    public event Action OnPlatformRotate = null;

    private void Awake()
    {
        _rotationSpeed = _startRotationSpeed;

        Debug.Log("First stage. Spped: " + _rotationSpeed);
        //_attachedObjects = new List<Transform>();
    }

    private void OnEnable()
    {
        OnPlatformRotate += delegate() { transform.Rotate(Vector3.up, _rotationSpeed.y, Space.Self); };

        GameManager.Instance.OnSessionFinish += delegate { StopAllCoroutines(); FinishSatge(); };
    }


    public void CheckScore() {

        float score = ScoreCounter.Instance.GetCount();

        if (_stage == 0 && score >= 100.0f || _stage == 1 && score >= 200.0f || _stage == 2 && score >= 300.0f)
        {

            
            NextStage();
        }
        else if (_stage == 3) { 
        

        
        }


        //if (score > 100.0f && score < 200.0f || score >= 200.0f) NextStage();
    
    }


    private void NextStage() {

        this.transform.eulerAngles = new Vector3(UnityEngine.Random.Range(0.0f, 3.5f), this.transform.eulerAngles.y, UnityEngine.Random.Range(0.0f, 3.5f));

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

        StartCoroutine(AccelerateRotation(_targetSpeed));

        _stage++;
    }

    private void FinishSatge()
    {
        Vector3 _targetSpeed = _startRotationSpeed;
        _targetSpeed.y *= _rotationSpeed.normalized.y;
        StartCoroutine(AccelerateRotation(_targetSpeed));
    }


    private bool SafeEquals(float a, float b) {

        const float e = 0.1f;

        Debug.Log(Mathf.Abs(a - b));

        return Mathf.Abs(a - b) < e;
    
    }

    private IEnumerator AccelerateRotation(Vector3 _targetSpeed) {

        float Acceleration = 0.1f;

        if (_rotationSpeed.y > _targetSpeed.y) {

            Acceleration *= -1;
        }
                
        WaitForSeconds wait = new WaitForSeconds(0.1f);

        Debug.Log("Target: " + _targetSpeed);


        while (!SafeEquals(_rotationSpeed.y, _targetSpeed.y)) {

            Debug.Log("Current: " + _rotationSpeed);

            _rotationSpeed.y += Acceleration;

            yield return wait;
        
        }


        Debug.Log("Complete!");

        yield return null;
    
    }



   


    public void StartRotationAroundTree(Transform projectile) {

        //OnPlatformRotate += delegate () { projectile.RotateAround(transform.position, Vector3.up,_rotationSpeed.y); };

        //_attachedObjects.Add(projectile);
        projectile.parent = this.transform;
        
        CheckScore();
    
    }

    public void StopRotationAroundTree(Transform projectile) {

        //_attachedObjects.Remove(projectile);
        projectile.parent = null;
        CheckScore();

    }



    private void FixedUpdate()
    {
        OnPlatformRotate?.Invoke();

        //foreach (Transform transform in _attachedObjects) {

          //  transform.RotateAround(this.transform.position, Vector3.up, _rotationSpeed.y);

        //}
    }

}

