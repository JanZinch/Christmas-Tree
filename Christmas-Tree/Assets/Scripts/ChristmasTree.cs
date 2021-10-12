using System.Collections;
using UnityEngine;

public class ChristmasTree : MonoBehaviour
{
    [SerializeField] private RotatingPlatform _rotatingPlatform = null;

    public RotatingPlatform Platform { get { return _rotatingPlatform; } private set { _rotatingPlatform = value; } }


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}