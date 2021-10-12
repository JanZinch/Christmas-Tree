using System;
using UnityEngine;


public class RotatingPlatform : MonoBehaviour
{
    [SerializeField] private Vector3 _rotationSpeed = new Vector3(0.0f, 2.5f, 0.0f);

    public event Action OnPlatformRotate = null;

    private void Awake()
    {
        
    }

    private void OnEnable()
    {
        OnPlatformRotate += delegate() { transform.Rotate(_rotationSpeed, Space.Self); };
    }


    public void StartRotationAroundTree(Transform projectile) {

        OnPlatformRotate += delegate () { projectile.RotateAround(transform.position, Vector3.up,_rotationSpeed.y); };
    }

    private void FixedUpdate()
    {
        OnPlatformRotate?.Invoke();

    }

}

