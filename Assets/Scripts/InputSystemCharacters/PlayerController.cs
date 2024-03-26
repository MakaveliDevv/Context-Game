using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{ 
    // private PlayerInput _playerInput;
    public Rigidbody2D rb;
    public float moveSpeed;
    // private PlayerInputActions _playerActions;
    private PlayerManager pManager;
    private Controller controller;
    public float fallMultiplier;
    [HideInInspector] public Vector2 inputDirection;
    public Transform castPosition;
    public float radius;
    public bool playerIsGrounded;
    private Vector2 vecGravity;
    public bool isMoving = false;
    public bool playerDetected;
    public LayerMask layermask;
    public GameObject playerRenderer;
    private Controls _controls;

    Vector2 movementInput;


    void Awake() 
    {
        // _playerInput = GetComponent<PlayerInput>();
        rb = GetComponent<Rigidbody2D>();
        pManager = GetComponent<PlayerManager>();
        controller = GetComponentInChildren<Controller>();

        _controls = new();
        _controls.Gameplay.Enable();
        // _playerActions = new();
        // _playerActions.Player.Enable();
    }
    void Start() 
    {
        vecGravity = new(0f, - Physics2D.gravity.y);
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

        // if (!controller.isExpanding && !controller.isExpandingBack 
        // && !controller.stopScalingCuzEndPointReached && playerIsGrounded) 
        // {
        //     if (!playerDetected) 
        //     {
        //         inputDirection.y = 0f; // Prevent movement along the Y-axis if not on the ladder
        //     }

        //     // Apply movement
        //     rb.velocity = new Vector2(inputDirection.x * moveSpeed, rb.velocity.y);
        //     // transform.Translate(new Vector3(movementInput.x, 0, movementInput.y * moveSpeed * Time.deltaTime));
        // }
    }
    
    // public void OnMove(InputAction.CallbackContext ctx) => inputDirection = ctx.ReadValue<Vector2>();
    // public void OnMove(InputAction.CallbackContext ctx) => movementInput = ctx.ReadValue<Vector2>();

    private void FixedUpdate() 
    {
        // MoveArtist();
        // MoveDeveloper();
        // MoveDesigner();
    }   

    // public void MovePlayer(InputAction.CallbackContext ctx) 
    // {
    //     if(ctx.performed) 
    //     {
    //         if (!controller.isExpanding && !controller.isExpandingBack 
    //         && !controller.stopScalingCuzEndPointReached && playerIsGrounded) 
    //         {
    //             inputDirection = ctx.ReadValue<Vector2>();
                
    //             // Check if the player can move along the Y-axis
    //             if (!playerDetected) 
    //             {
    //                 inputDirection.y = 0f; // Prevent movement along the Y-axis if not on the ladder
    //             }

    //             // Apply movement
    //             rb.velocity = new Vector2(inputDirection.x * moveSpeed, rb.velocity.y);

    //         }

    //     }
    // }

    // private void MoveArtist() 
    // {
    //     if (pManager.playerType == PlayerManager.PlayerType.ARTIST && !controller.isExpanding 
    //         && !controller.isExpandingBack && !controller.stopScalingCuzEndPointReached 
    //         && playerIsGrounded) 
    //     {
    //         inputDirection = _playerActions.Player.MovementArtist.ReadValue<Vector2>();
            
    //         // Check if the player can move along the Y-axis
    //         if (!playerDetected) 
    //         {
    //             inputDirection.y = 0f; // Prevent movement along the Y-axis if not on the ladder
    //         }

    //         // Apply movement
    //         rb.velocity = new Vector2(inputDirection.x * moveSpeed, rb.velocity.y);

    //     }
    // }

    // private void MoveDeveloper() 
    // {
    //     if (pManager.playerType == PlayerManager.PlayerType.DEVELOPER && !controller.isExpanding 
    //         && !controller.isExpandingBack && !controller.stopScalingCuzEndPointReached 
    //         && playerIsGrounded) 
    //     {
    //         inputDirection = _playerActions.Player.MovementDeveloper.ReadValue<Vector2>();
    //         inputDirection.y = 0f; // Prevent movement along the Y-axis if not on the ladder

    //         // Apply movement
    //         rb.velocity = new Vector2(inputDirection.x * moveSpeed, rb.velocity.y);
    //     }
    // }

    // private void MoveDesigner() 
    // {
    //     if (pManager.playerType == PlayerManager.PlayerType.DESIGNER && !controller.isExpanding 
    //         && !controller.isExpandingBack && !controller.stopScalingCuzEndPointReached 
    //         && playerIsGrounded) 
    //     {
    //         inputDirection = _playerActions.Player.MovementDesigner.ReadValue<Vector2>();
            
    //         // Check if the player can move along the Y-axis
    //         if (!playerDetected) 
    //         {
    //             inputDirection.y = 0f; // Prevent movement along the Y-axis if not on the ladder
    //         }

    //         // Apply movement
    //         rb.velocity = new Vector2(inputDirection.x * moveSpeed, rb.velocity.y);
    //     }
    // }
    
}
