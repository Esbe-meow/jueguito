using UnityEngine;

public class MenuVideo : MonoBehaviour
{
    [SerializeField] private MenuOptions optionsMenu;

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        optionsMenu.videoOptionsOpen = false;        
    }
}
