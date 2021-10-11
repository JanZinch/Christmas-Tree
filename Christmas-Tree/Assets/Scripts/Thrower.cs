using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thrower : MonoBehaviour
{
    [SerializeField] private Projectile _projectilePrefab = null;
    [SerializeField] private Transform _startPoint = null;
    [SerializeField] private Vector3 _forceMultiplier = Vector3.one;
    [SerializeField] private float _recharge = default;

    [SerializeField] private TrajectoryDrawer _drawer = null;

    private Vector3 _startMousePoint = default;
    private Vector3 _currentMousePoint = default;
    private Projectile _projectile = null;

    private WaitForSeconds _rechargeWaiting = null;

    private void OnMouseDown()
    {
        if (_projectile == null) return;

        _startMousePoint = _currentMousePoint = Input.mousePosition;
        _drawer.UpdateTrajectory(_projectile.Rigidbody, _projectile.transform.position, MakeForceFromScreen());
    }

    private void OnMouseDrag()
    {
        if (_projectile == null) return;

        _currentMousePoint = Input.mousePosition;
        _drawer.UpdateTrajectory(_projectile.Rigidbody, _projectile.transform.position, MakeForceFromScreen());
    }


    private void OnMouseUp()
    {
        if (_projectile == null) return;

        Vector3 force = MakeForceFromScreen();

        _drawer.UpdateTrajectory(_projectile.Rigidbody, _projectile.transform.position, force);
        _projectile.Rigidbody.isKinematic = false;
        _projectile.Rigidbody.AddForce(force);

        StartCoroutine(SetProjectileToScene(_rechargeWaiting));

        _drawer.RemoveTrajectory();

        _projectile = null;
    }


    private Vector3 MakeForceFromScreen() {

        Vector3 differency = _startMousePoint - _currentMousePoint;

        differency.x *= _forceMultiplier.x;
        differency.y *= _forceMultiplier.y;
        differency.z = differency.y;


        if (differency.y < 0.0f) {

            differency.y = Mathf.Abs(differency.y);
            differency.z = Mathf.Abs(differency.z);
            differency.x *= -1;
        }

        
        

        Debug.Log("DIF: " + differency);

        return differency;
    }


    private void SetProjectileToScene() {

        _projectile = Inventory.GetRandomProjectile(_startPoint.position, Quaternion.identity);

        //_projectile = PoolsManager.GetObject((int) Decoration.STAR, _startPoint.position, Quaternion.identity).GetComponent<Projectile>();
        _projectile.Rigidbody.isKinematic = true;


    
    }

    private IEnumerator SetProjectileToScene(WaitForSeconds wait)
    {
        yield return wait;

        _projectile = Inventory.GetRandomProjectile(_startPoint.position, Quaternion.identity);

        //_projectile = PoolsManager.GetObject((int) Decoration.CANDY_CANE, _startPoint.position, Quaternion.identity).GetComponent<Projectile>();
        _projectile.Rigidbody.isKinematic = true;

        
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
