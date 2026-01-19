using UnityEngine;

public class LevelCollectible : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private string collectibleID;

    void Start()
    {
        if (PlayerPrefs.GetInt(collectibleID, 0) == 1)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            player.collectionable++;
            PlayerPrefs.SetInt(collectibleID, 1);
            PlayerPrefs.Save();
            Destroy(gameObject);
        }
    }
}
