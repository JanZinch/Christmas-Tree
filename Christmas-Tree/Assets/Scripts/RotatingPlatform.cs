using System;
using System.Collections.Generic;
using UnityEngine;


public class RotatingPlatform : MonoBehaviour
{
    [SerializeField] private Vector3 _rotationSpeed = new Vector3(0.0f, 2.5f, 0.0f);

    [SerializeField] private List<Transform> _attachedObjects = null;



    public event Action OnPlatformRotate = null;

    private void Awake()
    {
        _attachedObjects = new List<Transform>();
    }

    private void OnEnable()
    {
        OnPlatformRotate += delegate() { transform.Rotate(_rotationSpeed, Space.Self); };
    }


    public void StartRotationAroundTree(Transform projectile) {

        //OnPlatformRotate += delegate () { projectile.RotateAround(transform.position, Vector3.up,_rotationSpeed.y); };

        _attachedObjects.Add(projectile);
    
    }

    public void StopRotationAroundTree(Transform projectile) {

        _attachedObjects.Remove(projectile);
    
    }



    private void FixedUpdate()
    {
        OnPlatformRotate?.Invoke();

        foreach (Transform transform in _attachedObjects) {

            transform.RotateAround(this.transform.position, Vector3.up, _rotationSpeed.y);

        }
    }

}

