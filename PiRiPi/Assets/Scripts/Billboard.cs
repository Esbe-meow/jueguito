using UnityEngine;

public class Billboard : MonoBehaviour
{
    private Transform cameraTransform;

    private void Start()
    {
        cameraTransform = Camera.main.transform;
    }

    void Update()
    {
        //si es un coleccionable, tambien rotara hacia arriba, si no sera un billboard normal.
        if (this.gameObject.CompareTag("Collectibles"))
        {
            //mantener el sprite siempre mirando a la camara pero tambien gira hacia arriba
            Vector3 camrotate = (cameraTransform.forward).normalized;
            Quaternion targetRotation = Quaternion.LookRotation(camrotate);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 180f * Time.deltaTime);
        }
        else
        {
            //mantener el sprite siempre mirando a la camara
            Vector3 camForward = Vector3.Normalize(new Vector3(cameraTransform.forward.x, 0, cameraTransform.forward.z));
            Vector3 camrotate = (camForward).normalized;
            Quaternion targetRotation = Quaternion.LookRotation(camrotate);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 180f * Time.deltaTime);
        }
        
    }
}
