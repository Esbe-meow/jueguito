using UnityEngine;

public class CustomTrigger : MonoBehaviour
{
    [SerializeField] Player player;
    [SerializeField] Roomba roomba;
    [SerializeField] GameObject cat;

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player"))
            cat.transform.position = player.spawnpoint;

        if (other.CompareTag("Blocking") && roomba.tracking)
        {
            roomba.cantReach = true;
        }
    }
}
