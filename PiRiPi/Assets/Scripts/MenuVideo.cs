using UnityEngine;
using System.Collections;

public class MenuVideo : MonoBehaviour
{
    [SerializeField] private MenuOptions optionsMenu;
    [SerializeField] private GameObject videoMenu;

    public bool test = false;

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)){
            optionsMenu.videoOptionsOpen = false;
        }     

        MenuManaging();
        
    }

    public void MenuManaging()
    {
        if (optionsMenu.videoOptionsOpen)
        videoMenu.SetActive(true);
        else
        videoMenu.SetActive(false);
    }

    public void OnBacktoMenu() => optionsMenu.videoOptionsOpen = false;
}
