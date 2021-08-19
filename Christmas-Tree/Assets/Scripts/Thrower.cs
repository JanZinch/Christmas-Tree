using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thrower : MonoBehaviour
{
    [SerializeField] private Rigidbody _projectilePrefab = null;
    [SerializeField] private Transform _startPoint = null;
    [SerializeField] private Vector3 _forceMultiplier = Vector3.one;
    [SerializeField] private float _recharge = default;

    [SerializeField] private TrajectoryDrawer _drawer = null;

    private Vector3 _startMousePoint = default;
    private Vector3 _currentMousePoint = default;
    private Rigidbody _projectile = null;

    private WaitForSeconds _rechargeWaiting = null;

    private void OnMouseDown()
    {
        if (_projectile == null) return;

        _startMousePoint = _currentMousePoint = Input.mousePosition;
        _drawer.UpdateTrajectory(_projectile, _projectile.transform.position, MakeForceFromScreen());
    }

    private void OnMouseDrag()
    {
        if (_projectile == null) return;

        _currentMousePoint = Input.mousePosition;
        _drawer.UpdateTrajectory(_projectile, _projectile.transform.position, MakeForceFromScreen());
    }


    private void OnMouseUp()
    {
        if (_projectile == null) return;

        Vector3 force = MakeForceFromScreen();

        _drawer.UpdateTrajectory(_projectile, _projectile.transform.position, force);
        _projectile.AddForce(force);

        StartCoroutine(SetProjectileToScene(_rechargeWaiting));

        _drawer.RemoveTrajectory();
    }


    private Vector3 MakeForceFromScreen() {

        Vector3 differency = _startMousePoint - _currentMousePoint;

        differency.x *= _forceMultiplier.x;
        differency.y *= _forceMultiplier.y;
        differency.z = differency.y;

        return differency;
    }


    private void SetProjectileToScene() {

        _projectile = Instantiate<Rigidbody>(_projectilePrefab, _startPoint.position, Quaternion.identity);    
    }

    private IEnumerator SetProjectileToScene(WaitForSeconds wait)
    {
        yield return wait;

        _projectile = Instantiate<Rigidbody>(_projectilePrefab, _startPoint.position, Quaternion.identity);
    }

    private void Awake()
    {
        _rechargeWaiting = new WaitForSeconds(_recharge);
    }

    private void Start() {

        SetProjectileToScene();        
    }

    private void Update()
    {
        
    }



}
