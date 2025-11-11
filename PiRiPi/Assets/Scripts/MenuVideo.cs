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
            optionsMenu.videoOptionsOpen=false;
        }     

        if (optionsMenu.videoOptionsOpen)
        videoMenu.SetActive(true);
        else
        videoMenu.SetActive(false);
    }
}
