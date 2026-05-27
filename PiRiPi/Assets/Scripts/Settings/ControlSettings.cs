using UnityEngine;
using UnityEngine.UI;

public class ControlSettings : MonoBehaviour
{
    [SerializeField] private CameraController cameraController; // Referencia al controlador de cámara
    [SerializeField] private Slider slider;

    private void Start()
    {
        // Cargar sensibilidad guardada (si existe) y aplicarla
        float savedSensitivity = PlayerPrefs.GetFloat("Sensitivity", 2f); // valor por defecto 2
        slider.value = savedSensitivity;
        SetSensitivity(savedSensitivity);
        
        // Agregar listener para cuando el usuario mueve el slider
        slider.onValueChanged.AddListener(SetSensitivity);
    }

    private void SetSensitivity(float value)
    {
        // Aplicar al controlador de cámara
        if (cameraController != null)
            cameraController.SetSensitivity(value);
        
        // Guardar en PlayerPrefs
        PlayerPrefs.SetFloat("Sensitivity", value);
        PlayerPrefs.Save(); // opcional: fuerza el guardado inmediato
    }
}

