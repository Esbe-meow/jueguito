using UnityEngine;

public class Roomba : MonoBehaviour
{
    [SerializeField] GameObject cat;
    [SerializeField] private GameObject brushLeft;
    [SerializeField] private GameObject brushRight;
    [SerializeField] Player player;
    [SerializeField] float rotationSpeed;
    [SerializeField] float brushRotationSpeed;
    [SerializeField] float mvmntSpeed;
    [SerializeField] float yAdd;
    [SerializeField] float timer; // Temporizador para cuando esta patruyando"
    [SerializeField] float timer2; // Temporizador para volver a perseguir despues de chocarse con algo
    [Range(-62f, 62f)]
    [SerializeField] float randomNumX;
    [Range(-62f, 62f)]
    [SerializeField] float randomNumZ;
    [SerializeField] Vector3 spawn;
    [SerializeField] Quaternion targetRotation;
    [SerializeField] private bool onTop;
    public bool cantReach;
    public bool tracking;


    void Start()
    {
        spawn = transform.position;
    }

    void Update()
    {
        //rotacion de los cepillos
        brushLeft.transform.Rotate(0, brushRotationSpeed * Time.deltaTime, 0);
        brushRight.transform.Rotate(0, -brushRotationSpeed * Time.deltaTime, 0);
        
        if (cat.transform.position.y <= this.transform.position.y + yAdd && !onTop && !cantReach)
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

        if (cantReach)
        {
            timer2 += Time.deltaTime;
            if (timer2 >= 5f)
            {
                cantReach = false;
                timer2 = 0;
            }
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

    public void randomMoving()
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
            onTop = true;
    }

    private void OnTriggerExit(Collider other) {
        if (other.CompareTag("Player"))
            onTop = false;
    }
}
