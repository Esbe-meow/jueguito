using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreenManager : MonoBehaviour
{
    [Header("Nombre de la escena del juego")]
    public string gameSceneName = "GameScene";

    [Header("Paneles del menú")]
    public GameObject mainMenuPanel;

    private void Start()
    {
        mainMenuPanel.SetActive(true);
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
