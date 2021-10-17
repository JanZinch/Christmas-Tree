using UnityEngine;

public class StarCatcher : MonoBehaviour
{
    [SerializeField] private ChristmasTree _christmasTree = null;
    [SerializeField] private Transform _point = null;

    public ChristmasTree Tree { get { return _christmasTree; } private set { _christmasTree = value; } }

    public void Catch(Transform projectile) {

        projectile.position = _point.position;
    } 

}