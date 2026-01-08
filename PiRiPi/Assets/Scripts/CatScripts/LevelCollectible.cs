using UnityEngine;

public class LevelCollectible : MonoBehaviour
{
    [SerializeField] private Player player;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            player.collectionable++;
            Destroy(this.gameObject);
        }
    }
}
