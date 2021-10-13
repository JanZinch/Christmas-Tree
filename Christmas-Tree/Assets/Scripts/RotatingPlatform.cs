using System;
using System.Collections.Generic;
using UnityEngine;


public class RotatingPlatform : MonoBehaviour
{
    [SerializeField] private Vector3 _rotationSpeed = new Vector3(0.0f, 2.5f, 0.0f);

    [SerializeField] private List<Transform> _attachedObjects = null;

    private int _stage = 0;

    public event Action OnPlatformRotate = null;

    private void Awake()
    {
        _attachedObjects = new List<Transform>();
    }

    private void OnEnable()
    {
        OnPlatformRotate += delegate() { transform.Rotate(Vector3.up, _rotationSpeed.y, Space.Self); };
    }


    public void CheckScore() {

        float score = ScoreCounter.Instance.GetCount();

        if (_stage == 0 && score >= 100.0f || _stage == 1 && score >= 200.0f || _stage == 2 && score >= 300.0f) {

            Debug.Log("NEXT STAGE");
            NextStage();
        }


        //if (score > 100.0f && score < 200.0f || score >= 200.0f) NextStage();
    
    }


    private void NextStage() {

        this.transform.eulerAngles = new Vector3(0.0f, this.transform.eulerAngles.y, UnityEngine.Random.Range(0.0f, 3.5f));

        int direction = UnityEngine.Random.Range(0, 2);
        if (direction == 0)
        {
            direction = -1;
        }
        else {

            direction = 1;
        }

        _rotationSpeed = new Vector3(0.0f, UnityEngine.Random.Range(direction * 2.5f, direction * 5.0f), 0.0f);

        _stage++;
    }


    public void StartRotationAroundTree(Transform projectile) {

        //OnPlatformRotate += delegate () { projectile.RotateAround(transform.position, Vector3.up,_rotationSpeed.y); };

        _attachedObjects.Add(projectile);
        CheckScore();
    
    }

    public void StopRotationAroundTree(Transform projectile) {

        _attachedObjects.Remove(projectile);
        CheckScore();

    }



    private void FixedUpdate()
    {
        OnPlatformRotate?.Invoke();

        foreach (Transform transform in _attachedObjects) {

            transform.RotateAround(this.transform.position, Vector3.up, _rotationSpeed.y);

        }
    }

}

