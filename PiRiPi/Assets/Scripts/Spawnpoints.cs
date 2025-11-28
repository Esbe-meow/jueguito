using UnityEngine;

public class spawnpoints : MonoBehaviour
{
    public Player player;

    void Start()
    {
        player.spawnpoint = player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(player.transform.position.y < -1)
        {
            player.transform.position = player.spawnpoint;
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player"))
            player.spawnpoint = this.gameObject.transform.position;
    }
}
