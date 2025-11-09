using UnityEngine;

public class spawnpoints : MonoBehaviour
{
    [SerializeField] private Vector3 spawnpoint;
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
}
