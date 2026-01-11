using UnityEngine;

public class CrackHoleShooter : MonoBehaviour
{
    [SerializeField] private float _impulseForce = 10f;
    [SerializeField] private float _maxDistance = 100f;

    [SerializeField] private LayerMask _targetLayer;

    [SerializeField] GameObject _crackHolePrefab;

    private Camera _cam;
    private Ray _ray;

    private Rigidbody _rb;

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
            Gizmos.DrawSphere(hit.point, 0.1f);
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _ray = _cam.ScreenPointToRay(Input.mousePosition);
            Vector3 start = _ray.origin;
            Vector3 direction = _ray.direction;

            if (Physics.Raycast(start, direction, out RaycastHit hit, _maxDistance, _targetLayer))
            {
                if (hit.collider.attachedRigidbody != null)
                {
                    _rb = hit.collider.GetComponent<Rigidbody>();

                    GameObject _crackHoleClone = Instantiate(_crackHolePrefab, hit.collider.transform);

                    Vector3 quadPos = new Vector3(hit.point.x, hit.point.y, hit.point.z - 0.01f);
                    _crackHoleClone.transform.position = quadPos;

                    Quaternion _rotation = Quaternion.LookRotation(-hit.normal);
                    _crackHoleClone.transform.rotation = _rotation;

                    _rb.AddForceAtPosition(direction * _impulseForce, hit.point, ForceMode.Impulse);
                }
            }
        }
    }

}
