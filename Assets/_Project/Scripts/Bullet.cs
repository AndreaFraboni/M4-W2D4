using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Bullet : MonoBehaviour
{
    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _lifeSpan = 6f;

    private Rigidbody _rb;

    private Vector3 direction;
    public float _impulseForce;

    private void Awake()
    {
        if (_rb == null) _rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        Destroy(gameObject, _lifeSpan);
    }

    private void FixedUpdate()
    {
        _rb.MovePosition(_rb.position + direction * (_speed * Time.deltaTime));
    }

    public void Shoot(Vector3 dir, float impulseforce)
    {
        direction = dir.normalized;
        _impulseForce = impulseforce;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.attachedRigidbody != null)
        {
            collision.collider.attachedRigidbody.AddForce(direction * _impulseForce, ForceMode.Impulse);
        }
    }
}
