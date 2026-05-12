using UnityEngine;

public class AltBillboard : MonoBehaviour
{
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private Sprite[] sprites;
    [SerializeField] private SpriteRenderer sr;

    private const float angleStep = 60f;

    void Start()
    {
        //cameraTransform = Camera.main.transform;
    }

    void LateUpdate()
    {

            Vector3 camForward = new Vector3(cameraTransform.forward.x, 0, cameraTransform.forward.z).normalized;
            Quaternion targetRotation = Quaternion.LookRotation(camForward);
            transform.rotation = targetRotation;

            Vector3 dirToCam = cameraTransform.position - transform.position;
            dirToCam.y = 0f;
            dirToCam.Normalize();

            float signedAngle = Vector3.SignedAngle(transform.forward, dirToCam, Vector3.up);

            if (signedAngle < 0)
                signedAngle += 360f;

            int sector = Mathf.FloorToInt((signedAngle + angleStep * 0.5f) / angleStep) % sprites.Length;

            sr.sprite = sprites[sector];
        
    }
}

        

