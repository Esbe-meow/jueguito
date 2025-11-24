using UnityEngine;

public class Rope : MonoBehaviour
{
    //hola
    [SerializeField] private GameObject cat;
    [SerializeField] private Rigidbody RB;
    [SerializeField] private bool nearRope = false;
    [SerializeField] private bool canExit = false;
    [SerializeField] private bool timerRunning = false;
    [SerializeField] private float timer = 0;
    //[SerializeField] private CinemachineInputAxisController axisController;
    //[SerializeField] private CinemachineCamera CineCam;
    //[SerializeField] private float lockedAngle = -90f;
    public bool isClimbing = false; 
    [SerializeField] private float ropePosX;
    [SerializeField] private float ropePosZ;
    [SerializeField] private float Xdistance;
    [SerializeField] private float Zdistance;
    private void Start() 
    {
        ropePosX = this.gameObject.transform.position.x;
        ropePosZ = this.gameObject.transform.position.z;
    }

    void Update()
    {
        stateManaging();

        //ropePosX = this.gameObject.transform.position.x;
        //ropePosZ = this.gameObject.transform.position.z;
    }

    void stateManaging()
    {
        if(nearRope && !isClimbing)
            if (Input.GetKeyDown(KeyCode.E))
            {
                cat.transform.position = new Vector3 (ropePosX + Xdistance, cat.transform.position.y, ropePosZ + Zdistance);
                //cameraLock();
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
            if (Input.GetKeyDown(KeyCode.E) || Input.GetButtonDown("Jump") || !nearRope)
            {
                //cameraUnlock();
                canExit = false;
                isClimbing = false;
                RB.useGravity = true;
                RB.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
            }
    }

    /*
    public void cameraLock()
    {
        axisController.X.InputMode = InputAxisController.InputMode.None;

        var orbital = vcam.GetCinemachineComponent<CinemachineOrbitalFollow>();
        if (orbital != null)
        {
            orbital.HorizontalAxis.Value = lockedAngle;
        }
    }

    public void cameraUnlock()
    {
        axisController.X.InputMode = InputAxisController.InputMode.Input;
    }
    */

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
