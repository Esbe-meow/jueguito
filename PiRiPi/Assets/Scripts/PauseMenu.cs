using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{   
    [Header("UI Variables")]
    [SerializeField] private TextMeshProUGUI textM;
    [SerializeField] private float previousTimeScale = 1f;
    [SerializeField] private bool isPaused = true; 
    [SerializeField] private GameObject pauseMenuUI;
    [SerializeField] private Player player;

    [Header("Audio")]
    [SerializeField] private AudioSource As;
    [SerializeField] private AudioClip openMenu;
    [SerializeField] private AudioClip closeMenu;

    [Header("spawn reset")]
    [SerializeField] private spawnpoints spawn;

    public void Start() 
    {
        if (pauseMenuUI != null) 
        pauseMenuUI.SetActive(false);
        isPaused = false;
    }

    public void Update() 
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePauseMenu();
        }
    }

    public void TogglePauseMenu()
    {
        updateYarns(player.yarn);
        if (isPaused)
            Resume();
        else
            Pause();
    }

    public void Pause()
    {
        if (isPaused)
        return;
        isPaused = true;

        As.PlayOneShot(openMenu);

        //save previous timeScale
        previousTimeScale = Time.timeScale;
        Time.timeScale = 0f;

        if (pauseMenuUI != null) pauseMenuUI.SetActive(true);

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void Resume()
    {
        if (!isPaused)
        return;
        isPaused = false;

        As.PlayOneShot(closeMenu);

        //restore timeScale
        Time.timeScale = previousTimeScale; 

        if (pauseMenuUI != null) pauseMenuUI.SetActive(false);

        if (Cursor.lockState != CursorLockMode.Locked)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }      
    }

    public void OnResumeButton() => Resume();

    public void OnCheckpointButton()
    {
        player.transform.position = spawn.spawnpoint;
        Resume();
    }

    public void OnQuitButton()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else 
            Application.Quit();
        #endif
    }

    public void updateYarns(int a)
    {
        textM.text = a.ToString();
    }
}
