using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallShooter : MonoBehaviour
{
    [SerializeField] private GameObject _BulletPrefab;
    [SerializeField] private float _maxDistance = 100f;
    [SerializeField] private float _radius = 0.005f;

    private Camera _cam;

    private Ray _ray;

    private float _hitPointRadius = 0.1f;  

    private void Awake()
    {
        if (_cam == null) _cam = Camera.main;
    }

    void OnDrawGizmos()
    {
        if (_cam == null) return;

        Gizmos.color = Color.red;
        Gizmos.DrawRay(_ray.origin, _ray.direction * _maxDistance);

        if (Physics.Raycast(_ray, out RaycastHit hit, _maxDistance))
        {
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(hit.point, _hitPointRadius);
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _ray = _cam.ScreenPointToRay(Input.mousePosition);

            Vector3 start = _ray.origin;
            Vector3 direction = _ray.direction;            

            if (Physics.SphereCast(start, _radius, direction, out RaycastHit hit))
            {
                if (hit.collider.attachedRigidbody != null)
                {
                    Shoot(direction);
                }
                else
                {
                    //Debug.Log("colpito oggetto non hittabile !!");
                    return;
                }
            }
        }
    }

    private void Shoot(Vector3 direction)
    {
        GameObject cloneBullet = Instantiate(_BulletPrefab, transform.position, Quaternion.identity);
        cloneBullet.gameObject.GetComponent<Bullet>().Shoot(direction);
    }

}