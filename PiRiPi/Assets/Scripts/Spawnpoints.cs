using UnityEngine;

public class spawnpoints : MonoBehaviour
{
    public Vector3 spawnpoint;

    void Start()
    {
        spawnpoint = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.y < -1)
        {
            transform.position = spawnpoint;
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player"))
            spawnpoint = this.gameObject.transform.position;
    }
}
