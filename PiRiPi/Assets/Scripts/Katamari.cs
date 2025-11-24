using UnityEngine;

public class Katamari : MonoBehaviour
{
    private bool nearKatamari;
    public bool isRolling;

    void Start()
    {
        
    }

    void Update()
    {
        if (nearKatamari && Input.GetKeyDown(KeyCode.E))
        {
            isRolling = true;

        }
    }

    private void OnTriggerEnter(Collider other) 
    {
        if (other.CompareTag("Player"))
        nearKatamari = true;
    }

    private void OnTriggerExit(Collider other) {
        if (other.CompareTag("Player"))
        nearKatamari = false;
    }
}
