using System.Collections;
using UnityEngine;

public class ChristmasTree : MonoBehaviour
{
    [SerializeField] private RotatingPlatform _rotatingPlatform = null;
    [SerializeField] private float _branchesForce = default;

    public RotatingPlatform Platform { get { return _rotatingPlatform; } private set { _rotatingPlatform = value; } }
    public float BranchesForce { get { return _branchesForce; } private set { _branchesForce = value; } }

}