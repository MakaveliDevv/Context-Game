using UnityEngine;
using UnityEngine.InputSystem;

public class Mover : MonoBehaviour
{
    [SerializeField] float moveSpeed = 3f;
    public Vector2 inputDirection = Vector2.zero;   
    public Vector2 inputVector = Vector2.zero;
    private Rigidbody2D rb;
    public bool isMoving = false;

    private InputController inputContr;

    [SerializeField] private int playerIndex = 0;
    public bool playerIsGrounded;
    public Transform castPosition;
    public float radius;
    private Vector2 vecGravity;
    public LayerMask layermask;
    public GameObject playerRenderer;
    public bool playerDetected;
    public bool ableToJump;
    public float jumpForce = 5f;



    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        inputContr = GetComponent<InputController>();
    }
    void Start() 
    {
        vecGravity = new(0f, - Physics2D.gravity.y);

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
        Collider2D hit = Physics2D.OverlapCircle(castPosition.position, radius, layermask);
        if(hit != null) 
        {
            playerIsGrounded = true;

        } else 
        {
            playerIsGrounded = false;
        }

        MovePlayer();
    }

    void MovePlayer() 
    {
        isMoving = true;
        if(!inputContr.isExpanding && !inputContr.isExpandingBack 
        && !inputContr.stopScalingCuzEndPointReached && playerIsGrounded) 
        {
            if(!playerDetected)
                inputDirection.y = 0f; 


            inputDirection = new(inputVector.x, 0);
            inputDirection = transform.TransformDirection(inputDirection);
            inputDirection *= moveSpeed;
            isMoving = true;
        }

        // Apply movement
        rb.velocity = new Vector2(inputDirection.x, rb.velocity.y);
    }

    public void Jump() 
    {
        if(playerIsGrounded) 
        {   
            ableToJump = true;
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        
        }
    }

    // Extend Ability
    public void UseAbility() 
    {
        // Invoke the scale input
        inputContr.Scale();

        // Turn on the trigger so other players dont collide
        // CapsuleCollider2D capsuleCol = GetComponent<CapsuleCollider2D>();
        // capsuleCol.isTrigger = true;

        // Freeze the players position
        // rb.constraints = RigidbodyConstraints2D.FreezeAll;
        // playerRenderer.SetActive(false);
    }


    // Teleport Ability
    public void UseTeleport() 
    {
        inputContr.Teleport();
    }
}
