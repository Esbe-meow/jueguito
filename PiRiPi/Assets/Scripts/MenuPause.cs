using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MenuPause : MonoBehaviour
{   
    [Header("UI Variables")]
    [SerializeField] private TextMeshProUGUI textM;
    [SerializeField] private GameObject pauseMenuUI;
    [SerializeField] private GameObject optionsMenuUI;
    [SerializeField] private Player player;
    
    private float previousTimeScale = 1f;
    private bool isPaused = true; 
    public bool optionsOpen = false;


    [Header("Audio")]
    [SerializeField] private AudioSource AS;
    [SerializeField] private AudioClip openMenu;
    [SerializeField] private AudioClip closeMenu;

    [Header("spawn reset")]
    [SerializeField] private spawnpoints spawn;

    public void Start() 
    {
        if (pauseMenuUI != null) 
        pauseMenuUI.SetActive(false);
        isPaused = false;

        if (optionsMenuUI != null) 
        optionsMenuUI.SetActive(false);
        optionsOpen = false;
    }

    public void Update() 
    {
        //blocks main menu if theres other above it
        if(!optionsOpen)
            if (Input.GetKeyDown(KeyCode.Escape))
            TogglePauseMenu();    
    }

    public void updateYarns(int a)
    {
        textM.text = a.ToString();
    }

    public void TogglePauseMenu()
    {
        //updates yarns to show in menu
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

        AS.PlayOneShot(openMenu);

        //save previous timeScale
        previousTimeScale = Time.timeScale;
        Time.timeScale = 0f;

        if (pauseMenuUI != null) pauseMenuUI.SetActive(true);

        //unlocks Cursor
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void Resume()
    {
        if (!isPaused)
        return;
        isPaused = false;

        AS.PlayOneShot(closeMenu);

        //restore timeScale
        Time.timeScale = previousTimeScale; 

        if (pauseMenuUI != null) pauseMenuUI.SetActive(false);

        //locks cursor
        if (Cursor.lockState != CursorLockMode.Locked)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }      
    }

    public void openOptionsMenu()
    {
        optionsMenuUI.SetActive(true);
        optionsOpen = true;
    }

    public void OnResumeButton() => Resume();

    public void OnOptionsButton() => openOptionsMenu();

    public void OnCheckpointButton()
    {
        //tp to last checkpoint (check spawnpoint script)
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
}