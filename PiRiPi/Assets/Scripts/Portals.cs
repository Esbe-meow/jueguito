using UnityEngine;

public class Portals : MonoBehaviour
{
    [Header("Parametros")]
    [SerializeField] private Portals linkedPortal;
    [SerializeField] private Camera cam;
    [SerializeField] private RenderTexture texture;

    Camera Camera;

    void Start()
    {
        cam.targetTexture = texture;
        Camera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
