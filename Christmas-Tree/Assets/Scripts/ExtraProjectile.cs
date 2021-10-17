using UnityEngine;

public class ExtraProjectile : Projectile
{
    protected override void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<StarCatcher>(out StarCatcher catcher))
        {
            this.Rigidbody.isKinematic = true;
            _isAttached = true;
            
            ScoreCounter.Instance.Add(this._price);

            catcher.Catch(this.transform);            
            catcher.Tree.Platform.StartRotationAroundTree(this.transform);
        }
        else if (other.TryGetComponent<ChristmasTree>(out ChristmasTree christmasTree)) {

            this.Rigidbody.velocity = Vector3.zero;
            Vector3 resist = Vector3.Normalize(Thrower.StartPoint.position - this.transform.position) * christmasTree.BranchesForce;
            resist.y = 0.0f;
            this.Rigidbody.AddForce(resist);

        }

    }

}