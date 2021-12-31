using System.Collections;
using UnityEngine;

public class ReturnProjectileZone : MonoBehaviour
{
    [SerializeField] private Thrower _thrower = null;
    [SerializeField] private float _returningSpeed = 10.0f; 

    private void OnTriggerEnter(Collider other)
    {
        if (GameManager.Instance.GameSessionState != SessionState.FINISHED && other.TryGetComponent<Projectile>(out Projectile projectile) 
        && !projectile.IsFailed) {

            _thrower.TakeBackProjectile(projectile, _returningSpeed);

            Debug.Log("COLLISISON!");
        
        }


    }


}