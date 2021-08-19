using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thrower : MonoBehaviour
{
    [SerializeField] private Rigidbody _projectile = null;

    [SerializeField] private Vector3 _forceMultiplier = Vector3.one;

    [SerializeField] private TrajectoryDrawer _drawer = null;


    private Vector3 _startMousePoint = default;
    private Vector3 _currentMousePoint = default;


    private void OnMouseDown()
    {
        _startMousePoint = _currentMousePoint = Input.mousePosition;       
        _drawer.UpdateTrajectory(_projectile, _projectile.transform.position, MakeForceFromScreen());
    }

    private void OnMouseDrag()
    {
        _currentMousePoint = Input.mousePosition;
        _drawer.UpdateTrajectory(_projectile, _projectile.transform.position, MakeForceFromScreen());
    }


    private void OnMouseUp()
    {
        Vector3 force = MakeForceFromScreen();

        _drawer.UpdateTrajectory(_projectile, _projectile.transform.position, force);
        _projectile.AddForce(force);
    }


    private Vector3 MakeForceFromScreen() {

        Vector3 differency = _startMousePoint - _currentMousePoint;
        
        differency.x *= _forceMultiplier.x;
        differency.y *= _forceMultiplier.y;
        differency.z = differency.y;

        return differency;    
    }


    private void Start() {

        //_drawer.UpdateTrajectory(_projectile, _projectile.transform.position, _direction);

    }

    private void Update()
    {
        

        //if (Input.GetKeyDown(KeyCode.F)){

            
        //    _projectile.AddForce(_direction);
        //}
    }



}
