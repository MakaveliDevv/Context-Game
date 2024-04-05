using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    // Movement
    public Vector2 inputDirection = Vector2.zero;   
    public Vector2 inputVector = Vector2.zero;
    // private Vector2 vecGravity;
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private float jumpForce = 5f;
    public float groundRadius;
    [HideInInspector] public bool isGrounded;
    [HideInInspector] public bool playerDetected;
    private bool ableToJump, isMoving;


    [HideInInspector] public InputController inputContr;
    private Rigidbody2D rb;
    private new SpriteRenderer renderer;
    [SerializeField] private LayerMask layermask;
    public Transform castPosition;
    [SerializeField] private int playerIndex = 0;


    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        inputContr = GetComponent<InputController>();
        renderer = GetComponentInChildren<SpriteRenderer>();
    }

    void Start() 
    {
        // vecGravity = new(0f, - Physics2D.gravity.y);
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
        if(hit != null) 
        {
            isGrounded = true;

        } else 
        {
            isGrounded = false;
        }

        MovePlayer();
    }

    void MovePlayer() 
    {
        if(!playerDetected) // Detected by the ladder
            inputDirection.y = 0f; 

        if(inputContr.ableToMove) 
        {
            inputDirection = new(inputVector.x, 0);
            inputDirection = transform.TransformDirection(inputDirection);
            inputDirection *= moveSpeed;

            // Apply movement
            rb.velocity = new Vector2(inputDirection.x, rb.velocity.y);

            // Check if the input is -1 in the X axis
            if(inputDirection.x < 0) 
                // Then flip the sprite renderer on the X axis
                renderer.flipX = false;

            else if(inputDirection.x > 0)
                renderer.flipX = true;
        

            isMoving = rb.velocity.sqrMagnitude > 0.03f;
        }
    }

    public void Jump() 
    {
        if(isGrounded) 
        {   
            ableToJump = true;
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
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

    // Designer
    // public void ExtendInputDesigner() 
    // {
    //     inputContr.ExtendDesignersObj();
    // }

    public void RetractInput() 
    {
        inputContr.RetractObj();
    }

    public void TeleportInput() 
    {
        inputContr.Teleport();
    }
}
