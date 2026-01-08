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

    }

    public void Update() 
    {
        player.isPaused = pauseMenu.activeSelf;

        //blocks main menu if theres other above it
        if (!optionsMenu.activeSelf)
            if (Input.GetKeyDown(KeyCode.Escape))
            TogglePauseMenu(); 
    }

    //Pause menu:
    public void TogglePauseMenu()
    {
        //updates yarns to show in menu
        textM.text = player.yarn.ToString();

        if (pauseMenu.activeSelf)
            Resume();
        else
            Pause();
    }

    //pauses time and open the menu
    void Pause()
    {
        if (pauseMenu.activeSelf)
        return;

        AS.PlayOneShot(openMenu);

        //save previous timeScale
        previousTimeScale = Time.timeScale;
        Time.timeScale = 0f;
        
        if (pauseMenu != null) pauseMenu.SetActive(true);

        //unlocks Cursor
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    //resume time and close the menu
    void Resume()
    {
        if (!pauseMenu.activeSelf)
        return;

        AS.PlayOneShot(closeMenu);

        //restore timeScale
        Time.timeScale = previousTimeScale; 

        if (pauseMenu != null) pauseMenu.SetActive(false);

        //locks cursor
        if (Cursor.lockState != CursorLockMode.Locked)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }      
    } 

    //Options menu:
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
    //animacion de cierre
    private IEnumerator CloseAfterAnimation()
    {
        animator.SetTrigger("Close");

        yield return new WaitForSecondsRealtime(0.3f);
        optionsMenu.SetActive(false);
    }

    //Video menu:
    public void ToggleVideoMenu()
    {
        if (!videoMenu.activeSelf)
            videoMenu.SetActive(true);
        else
            videoMenu.SetActive(false);
    }

    //Audio menu:
    public void ToggleAudioMenu()
    {
        if(!audioMenu.activeSelf)
            audioMenu.SetActive(true);
        else
            audioMenu.SetActive(false);
    }

    //Pause buttons
    public void OnSpawnpointButton() 
    {
        player.transform.position = player.spawnpoint; 
        TogglePauseMenu();
    }

    public void OnContinueButton() => TogglePauseMenu();

    public void OnOptionsButton() => ToggleOptionsMenu();

    public void OnMainMenuButton()
    {
        Time.timeScale = previousTimeScale; 
        SceneManager.LoadScene(gameSceneName);
    }

    //Options buttons
    public void OnVideoButton() => ToggleVideoMenu();
    
    public void OnAudioButton() => ToggleAudioMenu();

    public void OnBacktoMenu() => ToggleOptionsMenu();
}