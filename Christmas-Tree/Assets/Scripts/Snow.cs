using System.Collections;
using UnityEngine;

public class Snow : MonoBehaviour
{
    [SerializeField] private ParticleSystem _particles = null;
    [SerializeField] private float _startSimulationTime = 10.0f;

    void Start()
    {
        _particles.Simulate(_startSimulationTime);
        _particles.Play();
    }

    void Update()
    {
    }
}