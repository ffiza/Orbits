using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(TrailRenderer))]
public class Particle : MonoBehaviour
{
    [SerializeField] private Vector3 _position = Vector3.zero;
    [SerializeField] private Vector3 _velocity = Vector3.zero;
    [SerializeField] private float _mass = 1f;
    [SerializeField] private bool _isDynamic = true;
    [SerializeField] private Rigidbody2D _rigidBody;
    [SerializeField] private TrailRenderer _trailRenderer;

    private void Awake()
    {
        transform.position = _position;
        _rigidBody.velocity = _velocity;
        _rigidBody.mass = _mass;
    }

    public float GetMass()
    {
        return Mathf.Min(_rigidBody.mass, _mass);
    }

    public bool IsDynamic()
    {
        return _isDynamic;
    }

    public Vector3 GetVelocity()
    {
        return _rigidBody.velocity;
    }

    public void ApplyForce(Vector3 force)
    {
        _rigidBody.AddForce(force);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawRay(transform.position, _velocity);
    }

    public void Reset()
    {
        transform.position = _position;
        _trailRenderer.Clear();
        _rigidBody.velocity = _velocity;
    }

    private void OnValidate()
    {
        transform.position = _position;
        _rigidBody.velocity = _velocity;
        _rigidBody.mass = _mass;
    }
}
