using UnityEngine;

public class Roomba : MonoBehaviour
{
    [SerializeField] private GameObject cat;
    [SerializeField] private Player player;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float mvmntSpeed;
    [SerializeField] private float yAdd;
    [SerializeField] private float timer;
    [Range(-62f, 62f)]
    [SerializeField] private float randomNumX;
    [Range(-62f, 62f)]
    [SerializeField] private float randomNumZ;
    [SerializeField] private Vector3 spawn;
    [SerializeField] private Quaternion targetRotation;
    [SerializeField] private bool tracking;


    void Start()
    {
        spawn = transform.position;
    }

    void Update()
    {
        if (cat.transform.position.y <= this.transform.position.y + yAdd)
            tracking = true;
        else
            tracking = false;

        if (tracking) lookAtTarget();
        else randomMoving();
        
        if (transform.position.y < 0)
            transform.position = spawn;

        timer += Time.deltaTime;
        if (timer>=5f)
        {
            randomNumX = Random.Range(-62, 62);
            randomNumZ = Random.Range(-62, 62);
            timer = 0;
        }
    }

    private void lookAtTarget()
    {
        Vector3 dir = cat.transform.position - transform.position;
        dir.y = 0f; 

        if (dir.sqrMagnitude < 0.001f)
            return;

        targetRotation = Quaternion.LookRotation(dir);

        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            targetRotation,
            rotationSpeed * Time.deltaTime
        );

        if (transform.position != dir)
            transform.position += transform.forward * mvmntSpeed * Time.deltaTime;
    }

    private void randomMoving()
    {
        Vector3 dir = new Vector3 (randomNumX, transform.position.y, randomNumZ) - transform.position;
        dir.y = 0f; 

        if (dir.sqrMagnitude < 0.001f)
            return;

        targetRotation = Quaternion.LookRotation(dir);

        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            targetRotation,
            rotationSpeed * Time.deltaTime
        );
        if (transform.position != dir)
            transform.position += transform.forward * mvmntSpeed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player"))
            cat.transform.position = player.spawnpoint;
    }
}
