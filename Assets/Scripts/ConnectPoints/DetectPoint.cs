using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectPoint : Point
{
    Vector2 previousPosition;

    [SerializeField] private bool isMoving;

    [SerializeField] private float radius = .2f;

    private CircleCollider2D circleCol;

    private Coroutine movementCoroutine;
    private bool isMovementCoroutineRunning;

    void Start() 
    {
        previousPosition = transform.position;
        circleCol = gameObject.AddComponent<CircleCollider2D>();
        circleCol.isTrigger = true;
        circleCol.radius = radius;

        // Start the coroutine once
        // movementCoroutine = StartCoroutine(CheckMovement());
    }


    void Update()
    {
        CheckForCollision();
        
    }


    // private bool CheckForCollision() 
    // {
    //     // var circleCol = gameObject.AddComponent<CircleCollider2D>();
    //     // circleCol.isTrigger = true;
    //     // circleCol.radius = radius;

    //     movementCoroutine = StartCoroutine(CheckMovement());

    //     if (isMoving)
    //     {
    //         Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, circleCol.radius);

    //         foreach (Collider2D hitCollider in colliders)
    //         {
    //             // Check if the collider is a connect point
    //             // if (hitCollider != circleCol && hitCollider.gameObject.CompareTag("connectPoint"))
    //             // {
    //                 if(hitCollider.TryGetComponent<ConnectPoint>(out var point) && point.type == PointType.CONNECT_POINT) 
    //                 {
    //                     Debug.Log("Made contact with the connect point");
    //                     // Check if the connect point's type matches the player's type
    //                     PlayerManager playerManager = GetComponentInParent<PlayerManager>();
    //                     if (playerManager != null)
    //                     {
    //                         switch (playerManager.playerType)
    //                         {
    //                             case PlayerManager.PlayerType.ARTIST:
    //                                 if (point.connectPoint == ConnectPointType.BRIDGE_TYPE)
    //                                 {
    //                                     // Freeze object
    //                                     InputController inputContr = GetComponentInParent<InputController>();
    //                                     inputContr.Freeze();
    //                                 }
    //                                 break;

    //                             case PlayerManager.PlayerType.DEVELOPER:
    //                                 if (point.connectPoint == ConnectPointType.LADDER_TYPE)
    //                                 {
    //                                     // Freeze object
    //                                     InputController inputContr = GetComponentInParent<InputController>();
    //                                     inputContr.Freeze();
    //                                 }
    //                                 break;

    //                             case PlayerManager.PlayerType.DESIGNER:
    //                                 if (point.connectPoint == ConnectPointType.GRAPPLING_TYPE)
    //                                 {
    //                                     // Freeze object
    //                                     InputController inputContr = GetComponentInParent<InputController>();
    //                                     inputContr.Freeze();
    //                                 }
    //                                 break;
    //                         }
    //                     }

    //                 }
    //             // }
    //         }
    //     }

    //     return false;
    // }

    private bool CheckForCollision() 
    {
        if (isMoving)
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, circleCol.radius);

            foreach (Collider2D hitCollider in colliders)
            {
                // Check if the collider is a connect point
                if (hitCollider.TryGetComponent<ConnectPoint>(out var point) && point.type == PointType.CONNECT_POINT) 
                {
                    Debug.Log("Made contact with the connect point");
                    // Check if the connect point's type matches the player's type
                    PlayerManager playerManager = GetComponentInParent<PlayerManager>();
                    if (playerManager != null)
                    {
                        InputController inputContr = GetComponentInParent<InputController>();
                        switch (playerManager.playerType)
                        {
                            case PlayerManager.PlayerType.ARTIST:
                                if (point.connectPoint == ConnectPointType.BRIDGE_TYPE)
                                {
                                    // Freeze object
                                    inputContr.Freeze();
                                }
                                break;

                            case PlayerManager.PlayerType.DEVELOPER:
                                if (point.connectPoint == ConnectPointType.LADDER_TYPE)
                                {
                                    // Freeze object
                                    inputContr.Freeze();
                                }
                                break;

                            case PlayerManager.PlayerType.DESIGNER:
                                if (point.connectPoint == ConnectPointType.GRAPPLING_TYPE)
                                {
                                    // Freeze object
                                    inputContr.Freeze();
                                }
                                break;
                        }
                    }
                }
            }
        }
        return false;

    }

    IEnumerator CheckMovement()
    {
        // Check also only if its extending
        InputController inputContr = GetComponentInParent<InputController>();

        while (inputContr.isExtending)
        {
            yield return new WaitForSeconds(0.1f); // Adjust the interval based on your needs

            Vector2 currentPosition = transform.position;

            if (currentPosition != previousPosition)
            {
                isMoving = true;
                previousPosition = currentPosition;
            }
            else
            {
                isMoving = false;
            }
        }

        isMovementCoroutineRunning = true;

    }

    public void OnDrawGizmosSelected() 
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
