using UnityEngine;

public class Rope : MonoBehaviour
{
    [SerializeField] private GameObject cat;
    [SerializeField] private Rigidbody RB;
    [SerializeField] private bool nearRope = false;
    [SerializeField] private bool canExit = false;
    [SerializeField] private bool timerRunning = false;
    [SerializeField] private float timer = 0;
    public bool isClimbing = false; 

    void Start()
    {

    }

    void Update()
    {
        stateManaging();
    }

    void stateManaging()
    {
        if(nearRope && !isClimbing)
            if (Input.GetKeyDown(KeyCode.E))
            {
                isClimbing = true;
                timerRunning = true;
                RB.useGravity = false;
                RB.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
            }

        if (timerRunning)
                timer+=Time.deltaTime;
                if (timer >= 0.5f)
                {
                    canExit = true;
                    timerRunning = false;
                    timer = 0;
                }

        if (isClimbing && canExit)
            if (Input.GetKeyDown(KeyCode.E) || !nearRope)
            {
                canExit = false;
                isClimbing = false;
                RB.useGravity = true;
                RB.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
            }
    }

    private void OnTriggerEnter(Collider other) 
    {
        if (other.CompareTag("Player"))
        nearRope = true;
    }

    private void OnTriggerExit(Collider other) {
        if  (other.CompareTag("Player"))
        {
            nearRope = false;
        }
    }
}
