using UnityEngine;

public class Billboard : MonoBehaviour
{
    private Transform cameraTransform;

    private void Start()
    {
        cameraTransform = Camera.main.transform;
    }

    private void LateUpdate()
    {        
        Vector3 camForward = new Vector3(cameraTransform.forward.x, 0, cameraTransform.forward.z).normalized;
        Quaternion targetRotation = Quaternion.LookRotation(camForward);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 180f * Time.deltaTime);
    }
}
