using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody = null;

    private bool _isAttached = false;

    public Rigidbody Rigidbody { get { return _rigidbody; } private set { _rigidbody = value; } }

    private void OnTriggerEnter(Collider other)
    {
        if (_isAttached) return;

        if (other.transform.parent.TryGetComponent<ChristmasTree>(out ChristmasTree christmasTree))
        {
            christmasTree.Platform.AddToThree(this.transform);

            this.Rigidbody.isKinematic = true;

            _isAttached = true;
        }
        else { 
        

        
        }



    }


}
