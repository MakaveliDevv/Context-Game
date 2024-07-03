using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectPoint : Point
{
    Vector2 previousPosition;

    [SerializeField] private bool isMoving;
    [SerializeField] private float radius = .2f;

    private CircleCollider2D circleCol;
#pragma warning disable IDE0052 // Remove unread private members
    private Coroutine movementCoroutine;
#pragma warning restore IDE0052 // Remove unread private members

    void Start() 
    {
        previousPosition = transform.position;
        circleCol = gameObject.AddComponent<CircleCollider2D>();
        circleCol.isTrigger = true;
        circleCol.radius = radius;

        // Start the movement check coroutine
        movementCoroutine = StartCoroutine(CheckMovement());
    }

    void Update()
    {
        CheckForCollision();
    }

    private void CheckForCollision() 
    {
        if (isMoving)
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, circleCol.radius);

            foreach (Collider2D hitCollider in colliders)
            {
                if (hitCollider.TryGetComponent<ConnectPoint>(out var point) && point.type == PointType.CONNECT_POINT) 
                {
                    Debug.Log("Made contact with the connect point");

                    PlayerManager playerManager = GetComponentInParent<PlayerManager>();
                    if (playerManager != null)
                    {
                        InputController inputContr = GetComponentInParent<InputController>();
                        switch (playerManager.playerType)
                        {
                            case PlayerManager.PlayerType.ARTIST:
                                if (point.connectPoint == ConnectPointType.BRIDGE_TYPE)
                                {
                                    inputContr.Freeze();
                                }
                                break;
                            case PlayerManager.PlayerType.DEVELOPER:
                                if (point.connectPoint == ConnectPointType.LADDER_TYPE)
                                {
                                    inputContr.Freeze();
                                }
                                break;
                            case PlayerManager.PlayerType.DESIGNER:
                                if (point.connectPoint == ConnectPointType.GRAPPLING_TYPE)
                                {
                                    inputContr.Freeze();
                                }
                                break;
                        }
                    }
                }
            }
        }
    }

    private IEnumerator CheckMovement()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.1f);

            Vector2 currentPosition = transform.position;
            isMoving = currentPosition != previousPosition;
            previousPosition = currentPosition;
        }
    }

    public void OnDrawGizmosSelected() 
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
