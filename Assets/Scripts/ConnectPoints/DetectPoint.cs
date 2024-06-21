using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectPoint : Point
{
    Vector2 previousPosition;

#pragma warning disable IDE0052 // Remove unread private members
    [SerializeField] private bool isMoving;
#pragma warning restore IDE0052 // Remove unread private members

    [SerializeField] private float radius = 1f;

#pragma warning disable IDE0052 // Remove unread private members
    private Coroutine movementCoroutine;
#pragma warning restore IDE0052 // Remove unread private members


    void Start() 
    {
        previousPosition = transform.position;
    }


    void Update()
    {
        CheckForCollision();
    }


    private bool CheckForCollision() 
    {
        movementCoroutine = StartCoroutine(CheckMovement());

        if (isMoving)
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, radius);

            foreach (Collider2D collider in colliders)
            {
                // Check if the collider is a connect point
                if (collider.TryGetComponent<ConnectPoint>(out var point) && point.type == PointType.CONNECT_POINT)
                {
                    // Check if the connect point's type matches the player's type
                    PlayerManager playerManager = GetComponentInParent<PlayerManager>();
                    if (playerManager != null)
                    {
                        switch (playerManager.playerType)
                        {
                            case PlayerManager.PlayerType.ARTIST:
                                if (point.connectPoint == ConnectPointType.BRIDGE_TYPE)
                                {
                                    // Freeze object
                                    InputController inputContr = GetComponentInParent<InputController>();
                                    inputContr.Freeze();
                                }
                                break;

                            case PlayerManager.PlayerType.DEVELOPER:
                                if (point.connectPoint == ConnectPointType.LADDER_TYPE)
                                {
                                    // Freeze object
                                    InputController inputContr = GetComponentInParent<InputController>();
                                    inputContr.Freeze();
                                }
                                break;

                            case PlayerManager.PlayerType.DESIGNER:
                                if (point.connectPoint == ConnectPointType.GRAPPLING_TYPE)
                                {
                                    // Freeze object
                                    InputController inputContr = GetComponentInParent<InputController>();
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
    }

    public void OnDrawGizmosSelected() 
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
