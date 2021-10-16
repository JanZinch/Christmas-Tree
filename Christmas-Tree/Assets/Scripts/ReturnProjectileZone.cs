using System.Collections;
using UnityEngine;

public class ReturnProjectileZone : MonoBehaviour
{
    [SerializeField] private Thrower _thrower = null;


    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Projectile>(out Projectile projectile)) {

            _thrower.TakeBackProjectile(projectile);

            Debug.Log("COLLISISON!");
        
        }


    }


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}