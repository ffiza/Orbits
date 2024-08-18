using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class Orbit : MonoBehaviour
{
    [SerializeField] private float _gravitationalConstant;
    [SerializeField] private List<Particle> _particles;
    private float _kineticEnergy;
    private float _potentialEnergy;
    private float _energy;
    private float _time;
    private float _initialKineticEnergy;
    private float _initialPotentialEnergy;
    private float _initialEnergy;

    private void Awake()
    {
        _time = 0f;
        foreach (Transform child in transform)
        {
            if (child != null)
            {
                _particles.Add(child.GetComponent<Particle>());
            }
        }

        #region Calculate Initial Energies
        _initialKineticEnergy = 0f;
        _initialPotentialEnergy = 0f;
        (List<Vector3> forces, float potentialEnergy) = CalculateForces(_particles, _gravitationalConstant);
        for (int i = 0; i < forces.Count; i++)
        {
            if (_particles[i].IsDynamic())
            {
                _initialKineticEnergy += 0.5f * _particles[i].GetMass() * Mathf.Pow(_particles[i].GetVelocity().magnitude, 2);
            }
        }
        _initialPotentialEnergy = potentialEnergy;
        _initialEnergy = _initialKineticEnergy + _initialPotentialEnergy;
        #endregion
    }

    private void FixedUpdate()
    {
        float kineticEnergy = 0f;
        (List<Vector3> forces, float potentialEnergy) = CalculateForces(_particles, _gravitationalConstant);
        for (int i = 0; i < forces.Count; i++)
        {
            if (_particles[i].IsDynamic())
            {
                kineticEnergy += 0.5f * _particles[i].GetMass() * Mathf.Pow(_particles[i].GetVelocity().magnitude, 2);
                _particles[i].ApplyForce(forces[i]);
            }
        }
        _kineticEnergy = kineticEnergy;
        _potentialEnergy = potentialEnergy;
        _energy = kineticEnergy + potentialEnergy;
        _time += Time.fixedDeltaTime;
    }

    private static (List<Vector3>, float) CalculateForces(List<Particle> particles, float gravitationalConstant)
    {
        List<Vector3> forces = new();
        float potentialEnergy = 0f;
        for (int i = 0; i < particles.Count; i++)
        {
            Particle p = particles[i];
            Vector3 force = Vector3.zero;
            for (int j = 0; j < particles.Count; j++)
            {
                if (i != j)
                {
                    Particle pp = particles[j];
                    Vector3 dr = p.transform.position - pp.transform.position;
                    force += -gravitationalConstant * p.GetMass() * pp.GetMass() * dr / (dr.magnitude * dr.magnitude * dr.magnitude);
                    potentialEnergy += - 0.5f * gravitationalConstant * p.GetMass() * pp.GetMass() / dr.magnitude;
                }
            }
            forces.Add(force);
        }
        return (forces, potentialEnergy);
    }

    public float GetKineticEnergy()
    {
        return _kineticEnergy;
    }

    public float GetPotentialEnergy()
    {
        return _potentialEnergy;
    }

    public float GetEnergy()
    {
        return _energy;
    }

    public float GetTime()
    {
        return _time;
    }

    public void Restart()
    {
        _time = 0f;
        for (int i = 0; i < _particles.Count; i++)
        {
            _particles[i].Reset();
        }
        _kineticEnergy = _initialKineticEnergy;
        _potentialEnergy = _initialPotentialEnergy;
        _energy = _initialEnergy;
    }
}
