using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform _target;

    [Header("Settings")]
    [SerializeField] private float _smoothing = 20f;
    [SerializeField] private float _sensitivity = 2f;
    [SerializeField] private float _vel;

    private float x;
    private float y;
    private float sx;
    private float sy;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private Quaternion Rotation()
    {
        x -= Input.GetAxisRaw("Mouse X") * _sensitivity; // you have to take away to add or subtract an angle
        y -= Input.GetAxisRaw("Mouse Y") * _sensitivity; // you have to take away to add or subtract an angle

        y = Mathf.Clamp(y, -60f, 30f); // clamp the angle, these are magic numbers but you can write vars for them

        sx = Mathf.Lerp(sx, x, Time.deltaTime * _smoothing); // lerps x axis
        sy = Mathf.Lerp(sy, y, Time.deltaTime * _smoothing); // lerps y axis

        return Quaternion.Euler(sy, -sx, 0f); // assign the rotation
    }

    private void Update() => transform.rotation = Rotation();
    private void LateUpdate() => transform.position = _target.position; // due to your code, no smooth positioning for you teto
}
