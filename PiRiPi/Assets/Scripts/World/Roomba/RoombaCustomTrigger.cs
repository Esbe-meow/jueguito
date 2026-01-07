using UnityEngine;

public class CustomTrigger : MonoBehaviour
{
    [SerializeField] Player player;
    [SerializeField] GameObject cat;

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player"))
            cat.transform.position = player.spawnpoint;
    }
}
