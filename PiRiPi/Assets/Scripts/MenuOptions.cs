using UnityEngine;
using System.Collections;


public class MenuOptions : MonoBehaviour
{
    [Header("Options Variables")]
    [SerializeField] private MenuPause pauseMenu;
    [SerializeField] private GameObject optionsMenu;

    [Header("Options Buttons")]
    [SerializeField] private GameObject backtoMenuButton;
    [SerializeField] private GameObject videoButton;

    public bool videoOptionsOpen = false;

    [Header("Audio")]
    [SerializeField] private AudioSource AS;

    [Header("Animation")]
    [SerializeField] private Animator animator;

    void Start()
    {
        if(animator == null)
        animator = GetComponent<Animator>();
    }

    private void Update() 
    {
        if (pauseMenu.optionsOpen)
        animator.SetTrigger("Open");

        if(pauseMenu.optionsOpen)
        menuManaging(); 
        
        if (!videoOptionsOpen)
            if (Input.GetKeyDown(KeyCode.Escape))
            StartCoroutine(CloseAfterAnimation());
    }

    private void FixedUpdate() 
    {
    }

    public void menuManaging()
    {
        if (videoOptionsOpen)
        {
            videoButton.SetActive(false);
            backtoMenuButton.SetActive(false);
        }
        else
        {
            videoButton.SetActive(true);
            backtoMenuButton.SetActive(true);
        }
     }

    private IEnumerator CloseAfterAnimation()
    {
        animator.SetTrigger("Close");

        yield return new WaitForSecondsRealtime(0.3f);
        optionsMenu.SetActive(false);

        pauseMenu.optionsOpen = false;
    }

    public void OnBacktoMenu() => StartCoroutine(CloseAfterAnimation());

    public void OnVideoButton() => videoOptionsOpen = true;
}