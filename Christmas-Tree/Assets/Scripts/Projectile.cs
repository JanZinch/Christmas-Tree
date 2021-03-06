using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] protected Rigidbody _rigidbody = null;

    [SerializeField] protected float _price = default;

    protected bool _isAttached = false;
    public bool IsFailed { get; protected set; } = false;
    public bool IsMassive { get; set; } = false;

    public Rigidbody Rigidbody { get { return _rigidbody; } private set { _rigidbody = value; } }

    protected void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.TryGetComponent<Projectile>(out Projectile otherProjectile))
        {
            if (this.IsFailed || otherProjectile.IsFailed) return;


            if (this._isAttached)
            {
                ScoreCounter.Instance.Substract(this._price);
                //Debug.Log("THIS");
            }
            else if (otherProjectile._isAttached) {

                //Debug.Log("OTHER");
                ScoreCounter.Instance.Substract(otherProjectile._price);
            }


            this.Rigidbody.isKinematic = false;
            this.IsFailed = true;

            otherProjectile.Rigidbody.isKinematic = false;

            otherProjectile._isAttached = false;
            otherProjectile.IsFailed = true;

        }
        else if (!_isAttached && collision.transform.parent != null && collision.transform.parent.TryGetComponent<RotatingPlatform>(out RotatingPlatform rotatingPlatform)) {
            
            
            if (this.IsMassive && GameManager.Instance.GameSessionState != SessionState.FINISHED)
            {
                this.Rigidbody.isKinematic = true;
                ScoreCounter.Instance.Add(this._price);
                _isAttached = true;                         
            }
            else
            {
                IsFailed = true;
            }

            rotatingPlatform?.StartRotationAroundTree(this.transform);
        }

    }


    protected virtual void OnTriggerEnter(Collider other)
    {

        if (!_isAttached && !IsFailed && other.transform.parent != null && other.transform.parent.TryGetComponent<ChristmasTree>(out ChristmasTree christmasTree))
        {
            if (GameManager.Instance.GameSessionState == SessionState.FINISHED)
            {
                this.Rigidbody.velocity = Vector3.zero;

                Vector3 resist = Vector3.Normalize(Thrower.StartPoint.position - this.transform.position) * christmasTree.BranchesForce;
                resist.y = 0.0f;
                this.Rigidbody.AddForce(resist);
            
            }
            else if (!IsMassive) {
         
                this.Rigidbody.isKinematic = true;

                _isAttached = true;

                ScoreCounter.Instance.Add(this._price);
                christmasTree.Platform.StartRotationAroundTree(this.transform);
            }         
        }

    }


    protected void OnCollisionExit(Collision collision){

        if (!_isAttached && collision.transform.parent != null && collision.transform.parent.TryGetComponent<RotatingPlatform>(out RotatingPlatform rotatingPlatform))
        {
            rotatingPlatform.StopRotationAroundTree(this.transform);
            this.Rigidbody.isKinematic = false;
        }
    }

}
