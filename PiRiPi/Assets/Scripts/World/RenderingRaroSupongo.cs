using UnityEngine;

public class RenderingRaroSupongo : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private GameObject World1;
    
    private void OnTriggerEnter(Collider other) 
    {
        if (other.CompareTag("Player"))
        {
            World1.SetActive(true);
        }
    }
    
    private void OnTriggerExit(Collider other) 
    {
        if (other.CompareTag("Player"))
        {
            World1.SetActive(false);
        }
    }
}
