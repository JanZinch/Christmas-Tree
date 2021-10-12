using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody = null;

    private bool _isAttached = false;
    private bool _isFailed = false;
    public bool IsMassive { get; set; } = false;

    public Rigidbody Rigidbody { get { return _rigidbody; } private set { _rigidbody = value; } }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.TryGetComponent<Projectile>(out Projectile otherProjectile))
        {
            if (this._isFailed || otherProjectile._isFailed) return;


            if (otherProjectile._isAttached)
            {




            }


            this.Rigidbody.isKinematic = false;
            this._isFailed = true;

            otherProjectile.Rigidbody.isKinematic = false;

            otherProjectile._isAttached = false;
            otherProjectile._isFailed = true;

        }
        else if (!_isAttached && collision.transform.parent != null && collision.transform.parent.TryGetComponent<RotatingPlatform>(out RotatingPlatform rotatingPlatform)) {
            
            rotatingPlatform?.StartRotationAroundTree(this.transform);

            if (this.IsMassive) this.Rigidbody.isKinematic = true;



        }

    }


    private void OnTriggerEnter(Collider other)
    {

        if (!_isAttached && !_isFailed && other.transform.parent.TryGetComponent<ChristmasTree>(out ChristmasTree christmasTree))
        {
            christmasTree.Platform.StartRotationAroundTree(this.transform);

            this.Rigidbody.isKinematic = true;

            _isAttached = true;
        }
        




    }


}
