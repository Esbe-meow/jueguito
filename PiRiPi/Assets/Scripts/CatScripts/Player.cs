using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Video;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    #region  Variables

    [SerializeField] private LayerMask groundMask;
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] public Rigidbody rb;
    [SerializeField] private Animator animator;
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private LayerMask groundLayer;

    [Header("States")]
    [SerializeField] private bool isJumping;
    [SerializeField] private bool isGrounded;
    private bool ignoreGroundCheck = false;
    public bool isClimbing;
    [SerializeField] private bool isWalking;
    [SerializeField] private bool isFalling;
    public bool isPaused;
    private bool facingForward = true; // True = espalda a camara
    [SerializeField] private bool fallingBack; // Cayendo al suelo
    [SerializeField] private bool boostedJump; // Activa el salto potenciado
    private bool goingUp; // Subiendo despues de saltar

    [Header("Movility")]
    // Movimiento
    [SerializeField] private float totalSpeed;
    [SerializeField] private float speedCap;

    // Salto
    [SerializeField] private int jumpCount;
    [SerializeField] private int maxJumps = 2; // Doble salto
    [SerializeField] private float jumpMult;
    [SerializeField] private float jumpForce;
    [SerializeField] private float timer;

    // Escalar
    public Vector3 climbVel;

    // Caida rapida
    [SerializeField] private float fallDownSpeed;

    // Sprite de la cara
    [SerializeField] private SpriteRenderer front;

    [Header("Others")]
    public int yarn; // Monedas
    public int collectionable;
    public Vector3 spawnpoint;
    [SerializeField] private float rayDistance = 0.1f;
    [SerializeField] private float gravity;
    [SerializeField] private Vector3 origin;

    private CapsuleCollider capsule;
    
    #endregion

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        capsule = GetComponent<CapsuleCollider>();

        spawnpoint = transform.position;
        cameraTransform = Camera.main.transform;

        rb.useGravity = true; // Usar gravedad de Unity nativa
    }

    void Update()
    {
        if (isPaused) return;

        // Input de caida rapida
        if (Input.GetButtonDown("Fire1") && !isGrounded) FallBack();
        
        // Input de Salto
        if (Input.GetButtonDown("Jump") && jumpCount < maxJumps && !fallingBack){ Jump();}

        // Arreglar esto
        if (fallingBack) BoostJump();
        
            

        // Sprint
        speedCap = Input.GetKey(KeyCode.LeftShift) ? 10 : 6;

        // Estado de caida (cuando llega al punto mas alto de la parabola del salto)
        if (goingUp && rb.linearVelocity.y < -0.1f)
        {
            isFalling = true;
            goingUp = false;
        }

        // Ground check
        if (!ignoreGroundCheck)
        {
            bool groundHit = Physics.Raycast(transform.position, Vector3.down, rayDistance);
            Debug.DrawRay(transform.position, Vector3.down * rayDistance, Color.green);

            if (groundHit)
            {
                if (!isGrounded) jumpCount = 0;

                isGrounded = true;
                isJumping = false;
                isFalling = false;
                goingUp = false;

                animator.SetBool("isJumping", false);
                animator.SetBool("isFalling", false);
            }
            else isGrounded = false;
        }

        animator.SetBool("isFalling", isFalling);
    }

    private void FixedUpdate()
    {
        if (!isClimbing) Correr();
        else
        {
            Escalar();
            animator.SetBool("isWalking", false);
        }

        animator.SetBool("inRope", isClimbing);
    }

    private void Correr()
    {
        float xInput = Input.GetAxisRaw("Horizontal");
        float zInput = Input.GetAxisRaw("Vertical");

        Vector3 camForward = Vector3.Normalize(
            new Vector3(cameraTransform.forward.x, 0, cameraTransform.forward.z)
        );

        Vector3 camRight = Vector3.Normalize(
            new Vector3(cameraTransform.right.x, 0, cameraTransform.right.z)
        );

        Vector3 moveDir = (camForward * zInput + camRight * xInput).normalized;

        Vector3 velocity = rb.linearVelocity;
        velocity.x = moveDir.x * totalSpeed;
        velocity.z = moveDir.z * totalSpeed;
        rb.linearVelocity = velocity;

        // Sprite mirando adelante / atras
        if (zInput > 0.1f) facingForward = true;
        else if (zInput < -0.1f) facingForward = false;

        if (facingForward)
        {
            sr.enabled = true;
            front.gameObject.SetActive(false);
        }
        else
        {
            sr.enabled = false;
            front.gameObject.SetActive(true);
        }

        // Cap de velocidad
        Vector3 flatVel = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);
        if (flatVel.magnitude > speedCap)
        {
            flatVel = flatVel.normalized * speedCap;
            rb.linearVelocity = new Vector3(flatVel.x, rb.linearVelocity.y, flatVel.z);
        }

        bool hasMovementInput = moveDir.sqrMagnitude > 0.001f;
        isWalking = hasMovementInput && !isJumping;

        animator.SetBool("isWalking", isWalking);
        animator.SetBool("isGrounded", isGrounded);
    }

    public void Escalar()
    {
        if (Input.GetAxisRaw("Vertical") >= 0.1f) climbVel = rb.linearVelocity = new Vector3(0, 5, 0);
        else if (Input.GetAxisRaw("Vertical") <= -0.1f) climbVel = rb.linearVelocity = new Vector3(0, -5, 0);
        else climbVel = rb.linearVelocity = Vector3.zero;

        bool hasMovementInput = climbVel.sqrMagnitude > 0.001f;
        isWalking = hasMovementInput && isClimbing;

        animator.SetBool("isClimbing", hasMovementInput);
    }

    private void Jump()
    {
        if (jumpCount >= maxJumps) return;

        ignoreGroundCheck = true;

        isGrounded = false;
        isJumping = true;
        goingUp = true;
        isFalling = false;

        animator.SetBool("isFalling", false);
        animator.SetBool("isJumping", true);

        float finalJumpForce = boostedJump
            ? jumpForce + jumpForce * jumpMult
            : jumpForce;

        boostedJump = false;

        rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);
        rb.AddForce(Vector3.up * finalJumpForce, ForceMode.Impulse);

        jumpCount++;

        StartCoroutine(ResetGroundCheckNextFrame());
    }

    private System.Collections.IEnumerator ResetGroundCheckNextFrame()
    {
        yield return null; // Espera un frame
        ignoreGroundCheck = false;
    }

    private void BoostJump()
    {
        if (fallingBack && isGrounded)
        {
            timer += Time.deltaTime;

            if (timer < 0.5f)
            {
                boostedJump = true;
                fallingBack = false;
                //hola
                timer = 0;
            }
            else
            {
                boostedJump = false;
                timer = 0f;
            }
        }
    }

    void FallBack()
    {
        animator.SetBool("isFalling", true);
        rb.AddForce(-Vector3.up * fallDownSpeed, ForceMode.Impulse);
        fallingBack = true;
    }
}
