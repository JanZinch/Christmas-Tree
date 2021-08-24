using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody = null;

    public Rigidbody Rigidbody { get { return _rigidbody; } private set { _rigidbody = value; } }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.parent.TryGetComponent<ChristmasTree>(out ChristmasTree christmasTree))
        {

            this.Rigidbody.isKinematic = true;
        }
        else { 
        

        
        }



    }


}
