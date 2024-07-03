using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    // Movement
    public Vector2 inputDirection = Vector2.zero;   
    public Vector2 inputVector = Vector2.zero;
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private float jumpForce = 5f;
    public float groundRadius;
    [HideInInspector] public bool isGrounded;
    [HideInInspector] public bool playerDetected;
    private bool ableToJump, isMoving;

    [HideInInspector] public InputController inputContr;
    private Rigidbody2D rb;
    private new SpriteRenderer renderer;
    public Animator animator; // Public field to reference Animator
    [SerializeField] private LayerMask layermask;
    public Transform castPosition;
    [SerializeField] private int playerIndex = 0;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        inputContr = GetComponent<InputController>();
        renderer = GetComponentInChildren<SpriteRenderer>();
    }

    public int GetPlayerIndex() 
    {
        return playerIndex;
    }

    public void SetInputVector(Vector2 direction) 
    {
        inputVector = direction;
    }

    void Update() 
    {
        Collider2D hit = Physics2D.OverlapCircle(castPosition.position, groundRadius, layermask);
        isGrounded = hit != null;

        // Update isJumping state when player lands
        if (isGrounded && animator.GetBool("isJumping"))
        {
            animator.SetBool("isJumping", false);
        }

        MovePlayer();
    }

    void PlaySoundEffect() 
    {
        var spriteRendere_obj = renderer.gameObject;
        var audioSource = spriteRendere_obj.GetComponent<AudioSource>();
        audioSource.Play();
    }

    void MovePlayer() 
    {
        if (!playerDetected) // Detected by the ladder
            inputDirection.y = 0f; 

        if (inputContr.ableToMove) 
        {
            inputDirection = new Vector2(inputVector.x, 0);
            inputDirection = transform.TransformDirection(inputDirection);
            inputDirection *= moveSpeed;

            // Apply movement
            rb.velocity = new Vector2(inputDirection.x, rb.velocity.y);

            // Flip the sprite based on movement direction
            renderer.flipX = inputDirection.x < 0;

            // Set isWalking only if not jumping
            if (!animator.GetBool("isJumping"))
            {
                isMoving = rb.velocity.sqrMagnitude > 0.03f;
                animator.SetBool("isWalking", isMoving);
            }
        }
    }

    public void Jump() 
    {
        if (isGrounded) 
        {   
            ableToJump = true;
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            animator.SetBool("isJumping", true); // Set isJumping when player jumps
            animator.SetBool("isWalking", false); // Ensure isWalking is false when jumping

            PlaySoundEffect();
        }
    }

    // Designer
    public void TransformInput() 
    {
        inputContr.TransformToObject();
    }

    // Designer
    public void Transform2Input() 
    {
        inputContr.TransformToCharacter();
    }

    public void ExtendInput() 
    { 
        inputContr.ExtendObj();
    }

    public void RetractInput() 
    {
        inputContr.RetractObj();
    }

    public void TeleportInput() 
    {
        inputContr.Teleport();
    }
}
