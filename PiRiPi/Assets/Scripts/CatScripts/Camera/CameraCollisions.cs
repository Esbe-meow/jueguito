using UnityEngine;

public class CameraCollisions : MonoBehaviour
{
    [SerializeField] private Transform _camColHolder; // El objeto hijo que gestiona las colisiones
    [SerializeField] private Vector3 _camOrigin;       // Posicion default de la camara
    [SerializeField] private float _maxDistance = 2.9f;
    [SerializeField] private LayerMask _collisionMask; // ignora las layers

    private void LateUpdate() => CamCollision();

    private void CamCollision()
    {
        Vector3 origin = transform.position; // origen del gato
        Vector3 direction = (_camColHolder.position - origin).normalized; // normaliza la posicion del gato a la de la camara

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