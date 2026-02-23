using UnityEngine;

public class CameraCollisions : MonoBehaviour
{
    [SerializeField] private Transform _camColHolder; // child object that handles collisions
    [SerializeField] private Vector3 _camOrigin;       // default local position of camera
    [SerializeField] private float _maxDistance = 2.9f;
    [SerializeField] private LayerMask _collisionMask; // optional, to ignore layers

    private void LateUpdate() => CamCollision();

    private void CamCollision()
    {
        Vector3 origin = transform.position; // player orig
        Vector3 direction = (_camColHolder.position - origin).normalized; // to cam

        float desiredDistance = Vector3.Distance(_camOrigin + origin, origin);
        RaycastHit hit;

        if (Physics.Raycast(origin, direction, out hit, _maxDistance, _collisionMask))
        {
            float distance = Mathf.Clamp(hit.distance * 0.9f, 0.1f, _maxDistance);
            _camColHolder.position = origin + direction * distance;
        }
        else
            _camColHolder.localPosition = Vector3.Lerp(_camColHolder.localPosition, _camOrigin, Time.deltaTime * 10f);

        Debug.DrawRay(origin, direction * _maxDistance, Color.green);
    }
}