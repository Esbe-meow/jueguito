using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Video;
using UnityEngine.InputSystem;

//hola
public class Player : MonoBehaviour
{
    //variables:
    [SerializeField] Rope cuerda;
    [SerializeField] Katamari katamari;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Animator animator;
    [SerializeField] private Transform cameraTransform;

    [Header("States")]
    private bool isJumping; 
    private bool isGrounded;
    public bool isClimbing;
    private bool fallingBack; //falling to the ground
    private bool boostedJump; //can jump higher
    private bool goingUp; //going up after a jump

    [Header("Movility")]
    //Movement
    [SerializeField] private float totalSpeed;
    [SerializeField] private float speedCap;
    //Jump
    [SerializeField] private int jumpCount;
    [SerializeField] private int maxJumps;
    [SerializeField] private float jumpMult;
    [SerializeField] private float jumpForce;
    [SerializeField] private float timer;
    //Fall Down
    [SerializeField] private float fallDownSpeed;
    //Sprite Managing
    [SerializeField] private SpriteRenderer front;
    
    [Header("others")]
    public int yarn; //coins
    public Vector3 spawnpoint;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

        cameraTransform = Camera.main.transform;
    }

    void Update()
    {   
        if (Input.GetButtonDown("Fire1") && !isGrounded)
            FallBack();

        if (Input.GetButtonDown("Jump") && !fallingBack)
            Jump();
        
        if (fallingBack)
            BoostJump();
        
        if (Keyboard.current.leftShiftKey.isPressed)
            speedCap = 15.5f;
        else 
            speedCap = 8.5f;


        if (goingUp && rb.linearVelocity.y < -0.1f)
        {
            animator.Play("Falling", 0, 0);
            goingUp = false;
        }

        if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, 0.4f) && !isGrounded && rb.linearVelocity.y <= 0)    
        {
            isGrounded = true;
            isJumping = false;
            goingUp = false;
            animator.Play("Idle", 0, 0);
            jumpCount = 0;
        }

        /*if (Physics.CheckSphere(transform.position, 1f, groundMask) && !isGrounded && rb.linearVelocity.y <= 0)
        {
            
        }
        */
    }

    private void FixedUpdate()
    {
        if(!isClimbing)
        Correr();
        else
        Escalar();
    }

    private void Correr()
    {
        float xInput = Input.GetAxisRaw("Horizontal");
        float zInput = Input.GetAxisRaw("Vertical");

        // obtener direccion de la camara pero mantenerla horizontal para que no vuele.
        Vector3 camForward = Vector3.Normalize(new Vector3(cameraTransform.forward.x, 0, cameraTransform.forward.z));
        Vector3 camRight = Vector3.Normalize(new Vector3(cameraTransform.right.x, 0, cameraTransform.right.z));

        // Combinar la direccion de la camara con el movimiento
        Vector3 moveDir = (camForward * zInput + camRight * xInput).normalized;
        rb.linearVelocity += moveDir * totalSpeed * Time.deltaTime;

        //si no se esta pulsando ningun boton se queda quieto para evitar deslizamientos raros
        if (xInput == 0 && zInput == 0)
        {
            rb.linearVelocity = new Vector3(0f, rb.linearVelocity.y, 0f);
            if (isJumping == false && rb.linearVelocity.y >= 0)
            {
                rb.linearVelocity = Vector3.zero;
            }
        }
        else
        {
            if (xInput == 0)
                if (zInput == 1)
                    front.gameObject.SetActive(false);
                else
                    front.gameObject.SetActive(true);
        }

        //cap de velocidad 
        Vector3 flatVel = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);
        if (flatVel.magnitude > speedCap)
        {
            flatVel = flatVel.normalized * speedCap;
            rb.linearVelocity = new Vector3(flatVel.x, rb.linearVelocity.y, flatVel.z);
        }
    }

    public void Escalar()
    {
        if(Input.GetAxisRaw("Vertical") >= 0.1)
            rb.linearVelocity = new Vector3 (0, 5, 0);
        else if (Input.GetAxisRaw("Vertical") <= -0.1)
            rb.linearVelocity = new Vector3 (0, -5, 0);
        else    
            rb.linearVelocity = Vector3.zero;
    }
    private void Jump()
    {
        if (jumpCount == maxJumps) return;
        animator.Play("Jumping", 0, 0);
        isGrounded = false;
        isJumping = true;
        goingUp = true;
        jumpCount++;
        rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0, rb.linearVelocity.z);
        if (boostedJump)
        {
            rb.linearVelocity += Vector3.up * jumpForce * jumpMult;
            boostedJump = false;
            fallingBack = false;
        }
        rb.linearVelocity += Vector3.up * jumpForce;
        //rb.AddForce(Vector3.up * jumpForce);
    }

    private void BoostJump()
    {
        if (fallingBack && isGrounded)
        {
            timer += Time.deltaTime;
            if (timer < 0.5)
            {
                boostedJump = true;
                fallingBack = false;
            }
            if (timer > 0.5)
            {
                boostedJump = false;
                fallingBack = false;
                timer = 0;
            }
        }
    }
    
    void FallBack() 
    {
        rb.linearVelocity -= Vector3.up * fallDownSpeed * Time.deltaTime;  
        fallingBack = true;
        animator.Play("Falling", 0, 0);
    }
}