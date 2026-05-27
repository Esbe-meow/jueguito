using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Threading;
using System.Collections;
using UnityEngine.SceneManagement;

public class MenuPause : MonoBehaviour
{   
    [Header("UI Variables")]
    
    [SerializeField] TextMeshProUGUI textM;
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject optionsMenu;
    [SerializeField] GameObject videoMenu;
    [SerializeField] GameObject audioMenu;
    [SerializeField] GameObject controlsMenu;

    [Header ("Other Variables")]
    [SerializeField] Player player;
    [SerializeField] string gameSceneName = "titleScene";
    
    float previousTimeScale;

    [Header("Audio")]
    [SerializeField] AudioSource AS;
    [SerializeField] AudioClip openMenu;
    [SerializeField] AudioClip closeMenu;

    [Header("Animation")]
    [SerializeField] private Animator animator;

    private void Awake() 
    {
        LoadPlayer();   
    }
    
    public void Start() 
    {
        if (pauseMenu != null) 
        pauseMenu.SetActive(false);

        if  (optionsMenu != null) 
        optionsMenu.SetActive(false);

        if (videoMenu != null)
        videoMenu.SetActive(false);

        if (audioMenu != null)
        audioMenu.SetActive(false);

        if (audioMenu != null)
        controlsMenu.SetActive(false);

        if (Cursor.lockState != CursorLockMode.Locked)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        } 
    }

    public void Update() 
    {
        player.isPaused = pauseMenu.activeSelf;

        // Bloquea el menu principal si hay otro por encima
        if (!optionsMenu.activeSelf)
            if (Input.GetButtonDown("Pause"))
            TogglePauseMenu(); 
    }

    // Pause menu:
    public void TogglePauseMenu()
    {
        // Actualiza el numero de monedas
        textM.text = player.yarn.ToString();

        if (pauseMenu.activeSelf)
            Resume();
        else
            Pause();
    }

    // Pausa el tiempo y abre el menu
    void Pause()
    {
        if (pauseMenu.activeSelf)
        return;

        AS.PlayOneShot(openMenu);

        // Guarda el Timescale anterior (1) y lo pone a 0
        previousTimeScale = Time.timeScale;
        Time.timeScale = 0f;
        
        if (pauseMenu != null) pauseMenu.SetActive(true);

        // Desbloquea el cursor
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    // Cierra el menu y reanuda el tiempo
    void Resume()
    {
        if (!pauseMenu.activeSelf)
        return;

        AS.PlayOneShot(closeMenu);

        // Reestablece el Timescale anterior
        Time.timeScale = previousTimeScale; 

        if (pauseMenu != null) pauseMenu.SetActive(false);

        // Bloquea el cursor
        if (Cursor.lockState != CursorLockMode.Locked)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }      
    } 

    // Options menu:
    public void ToggleOptionsMenu()
    {
        if (!optionsMenu.activeSelf)
            OpenOptions();
        else
            CloseOptions();
    }

    void OpenOptions()
    {
        optionsMenu.SetActive(true);
        animator.SetTrigger("Open");
    }

    void CloseOptions()
    {
        StartCoroutine(CloseAfterAnimation());
    }
    // Animacion de cierre
    private IEnumerator CloseAfterAnimation()
    {
        animator.SetTrigger("Close");

        yield return new WaitForSecondsRealtime(0.3f);
        optionsMenu.SetActive(false);
    }

    // Video menu:
    public void ToggleVideoMenu()
    {
        if (!videoMenu.activeSelf)
            videoMenu.SetActive(true);
        else
            videoMenu.SetActive(false);
    }

    // Audio menu:
    public void ToggleAudioMenu()
    {
        if(!audioMenu.activeSelf)
            audioMenu.SetActive(true);
        else
            audioMenu.SetActive(false);
    }

    public void ToggleControlsMenu()
    {
        if (!controlsMenu.activeSelf)
            controlsMenu.SetActive(true);
        else
            controlsMenu.SetActive(false);
    }

    // Pause buttons
    public void OnSpawnpointButton() 
    {
        TogglePauseMenu();
        player.transform.position = player.spawnpoint; 
    }

    public void OnContinueButton() => TogglePauseMenu();

    public void OnOptionsButton() => ToggleOptionsMenu();

    public void OnMainMenuButton()
    {
        Time.timeScale = previousTimeScale; 
        SavePlayer();
        SceneManager.LoadScene(gameSceneName);
    }

    // Options buttons
    public void OnVideoButton() => ToggleVideoMenu();
    
    public void OnAudioButton() => ToggleAudioMenu();

    public void OnControlsButton() => ToggleControlsMenu();

    public void OnBacktoMenu() => ToggleOptionsMenu();

    // Save player
    public void SavePlayer()
    {
        SaveSystem.SavePlayer(player);
    }

    // Load player
    public void LoadPlayer()
    {
        PlayerData data = SaveSystem.LoadPlayer();

        player.yarn = data.yarn;
        player.collectionable = data.collectibles;

        Vector3 checkpoint;
        checkpoint.x = data.checkpoint[0];
        checkpoint.y = data.checkpoint[1];
        checkpoint.z = data.checkpoint[2];

        player.transform.position = checkpoint;

    }
}