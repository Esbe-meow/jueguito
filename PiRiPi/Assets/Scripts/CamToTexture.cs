using UnityEngine;

public class PlayerCameraOnTV : MonoBehaviour
{
    public Camera playerCamera;
    public Renderer tvRenderer;
    public int width = 1024, height = 576;

    Camera mirrorCam;
    RenderTexture rt;

    void Start()
    {
        if (playerCamera == null) playerCamera = Camera.main;
        if (playerCamera == null || tvRenderer == null)
        {
            Debug.LogError("PlayerCameraOnTV: asigna playerCamera y tvRenderer en el Inspector.");
            enabled = false;
            return;
        }

        rt = new RenderTexture(width, height, 24);
        rt.Create();

        Material mat = tvRenderer.material; // esto instancia si era shared
        mat.mainTexture = rt;
        tvRenderer.material = mat;

        // crear y configurar cámara espejo
        GameObject g = new GameObject("MirrorCam");
        mirrorCam = g.AddComponent<Camera>();

        // copiar ajustes importantes de la cámara del jugador
        mirrorCam.CopyFrom(playerCamera);
        mirrorCam.cullingMask = playerCamera.cullingMask;
        mirrorCam.clearFlags = playerCamera.clearFlags;
        mirrorCam.backgroundColor = playerCamera.backgroundColor;
        mirrorCam.fieldOfView = playerCamera.fieldOfView;
        mirrorCam.allowHDR = playerCamera.allowHDR;
        mirrorCam.allowMSAA = playerCamera.allowMSAA;

        // evitar audio listeners duplicados
        var al = mirrorCam.GetComponent<AudioListener>();
        if (al != null) DestroyImmediate(al);

        // no render automático a pantalla: lo haremos con Render()
        mirrorCam.enabled = false;
        mirrorCam.targetTexture = rt;

        // parentear para sincronizar posición/rotación con la cámara del jugador
        g.transform.SetParent(playerCamera.transform, false);
        g.transform.localPosition = Vector3.zero;
        g.transform.localRotation = Quaternion.identity;
    }

    void LateUpdate()
    {
        if (mirrorCam != null)
        {
            // en caso de no parentearse correctamente, sincronizamos transform
            mirrorCam.transform.position = playerCamera.transform.position;
            mirrorCam.transform.rotation = playerCamera.transform.rotation;

            mirrorCam.Render();
        }
    }

    void OnDisable()
    {
        if (mirrorCam)
        {
            mirrorCam.targetTexture = null;
            Destroy(mirrorCam.gameObject);
        }
        if (rt != null)
        {
            rt.Release();
            Destroy(rt);
        }
    }
}