using UnityEngine;
using System.Collections;


public class OptionsMenu : MonoBehaviour
{
    [Header("Options Variables")]
    [SerializeField] private PauseMenu pauseMenu;
    [SerializeField] private GameObject optionsMenu;

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

        if (Input.GetKeyDown(KeyCode.Escape))
        StartCoroutine(CloseAfterAnimation());
    }

    private IEnumerator CloseAfterAnimation()
    {
        animator.SetTrigger("Close");

        yield return new WaitForSecondsRealtime(0.3f);
        optionsMenu.SetActive(false);

        pauseMenu.optionsOpen = false;
    }


    public void OnBacktoMenu() => StartCoroutine(CloseAfterAnimation());


}