using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody = null;

    [SerializeField] private float _price = default;

    private bool _isAttached = false;
    private bool _isFailed = false;
    public bool IsMassive { get; set; } = false;

    public Rigidbody Rigidbody { get { return _rigidbody; } private set { _rigidbody = value; } }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.TryGetComponent<Projectile>(out Projectile otherProjectile))
        {
            if (this._isFailed || otherProjectile._isFailed) return;


            if (this._isAttached)
            {
                ScoreCounter.Instance.Substract(this._price);
                Debug.Log("THIS");
            }
            else if (otherProjectile._isAttached) {

                Debug.Log("OTHER");
                ScoreCounter.Instance.Substract(otherProjectile._price);
            }


            this.Rigidbody.isKinematic = false;
            this._isFailed = true;

            otherProjectile.Rigidbody.isKinematic = false;

            otherProjectile._isAttached = false;
            otherProjectile._isFailed = true;

        }
        else if (!_isAttached && collision.transform.parent != null && collision.transform.parent.TryGetComponent<RotatingPlatform>(out RotatingPlatform rotatingPlatform)) {
            
            
            if (this.IsMassive && GameManager.Instance.GameSessionState != SessionState.FINISHED)
            {
                this.Rigidbody.isKinematic = true;
                ScoreCounter.Instance.Add(this._price);
                _isAttached = true;                         // mb
            }

            rotatingPlatform?.StartRotationAroundTree(this.transform);
        }

    }


    private void OnTriggerEnter(Collider other)
    {

        if (!_isAttached && !_isFailed && other.transform.parent.TryGetComponent<ChristmasTree>(out ChristmasTree christmasTree))
        {
            if (IsMassive || GameManager.Instance.GameSessionState == SessionState.FINISHED)
            {
                this.Rigidbody.velocity = Vector3.zero;
                Vector3 resist = Vector3.Normalize(Thrower.StartPoint.position - this.transform.position) * christmasTree.BranchesForce;
                resist.y = 0.0f;

                this.Rigidbody.AddForce(resist);

                Debug.Log("FORCE: " + resist);
            
            }
            else {


                

                this.Rigidbody.isKinematic = true;

                _isAttached = true;

                ScoreCounter.Instance.Add(this._price);
                christmasTree.Platform.StartRotationAroundTree(this.transform);
            }


            
        }
        




    }


    private void OnCollisionExit(Collision collision){

        if (!_isAttached && collision.transform.parent != null && collision.transform.parent.TryGetComponent<RotatingPlatform>(out RotatingPlatform rotatingPlatform))
        {
            rotatingPlatform.StopRotationAroundTree(this.transform);
            this.Rigidbody.isKinematic = false;

        }
    }

}
