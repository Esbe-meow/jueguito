using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleScreenManager : MonoBehaviour
{
    [Header("Boton Start")] 
    //public GameObject startButton;
    public float alphaThreshold = 0.1f;
    
    [Header("Nombre de la escena del juego")]
    public string gameSceneName = "GameScene";

    [Header("Paneles del menu")]
    public GameObject mainMenuPanel;

    private void Start()
    {
        mainMenuPanel.SetActive(true);
        //startButton.GetComponent<Image>().alphaHitTestMinimumThreshold = alphaThreshold;
    }

    public void OnClickPlayGame()
    {
        SceneManager.LoadScene(gameSceneName);
    }

    public void OnClicOpenOptions()
    {
        mainMenuPanel.SetActive(false);
    }

    // Botón: SALIR
    public void QuitGame()
    {
        Debug.Log("Saliendo del juego...");
        Application.Quit();
    }
}
