using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Controller : MonoBehaviour
{
    // Player stuff
    [Header("Some Stuff")]
    [SerializeField] protected ExtendableObj scriptableObj; // Scriptable object
    protected Coroutine coroutine;
    protected GameObject transformObject;
    private GameObject ladder;
    [SerializeField] private AudioSource audioSource;
    public PlayerManager playerManager;
    private GameObject ChildObj;
    // Points
    [Header("Points")]
    [SerializeField] protected Transform instantiatePoint;
    protected Transform extendPoint, collapsePoint, detectPoint;
    [SerializeField] protected Vector3 detectPointOffset;

    // Bools
    [Header("Bools")]
    public bool isExtending, isRetracting, isCollapsing, ableToMove, isNegative, isPositive;
    public bool objectCreated, reached_endPoint, reached_connectPoint, isDestroyed;
    public bool freeze;
    protected bool continueCollapse = true; // Flag to control the collapse process
    private bool isTransformed = false; // Flag to indicate if the artist is transformed
    private bool teleportToLadder = false; // Flag to indicate teleportation to ladder

    // Floats and such
    [Header("Floats And Such")]
    [SerializeField] private Vector3 teleportOffset;
    private Vector3 initlialScale;
    [SerializeField] private float extendDistance = 5f;
    [SerializeField] private float extendSpeed = 5f;
    [SerializeField] private float collapseSpeed;
    [SerializeField] private float rotateMotion = 5f;
    [SerializeField] private float rotateLimit;
    [SerializeField] private int rotateSpeed;
    [SerializeField] private float radius;
    [SerializeField] private Vector3 ladderOffset;

    void Awake()
    {
        playerManager = PlayerManager.instance;
        audioSource = gameObject.GetComponent<AudioSource>();
        initlialScale = scriptableObj.initialScale;

        ableToMove = true;
    }

    protected void CreateObject(ExtendableObj _obj)
    {
        TryGetComponent<PlayerController>(out var player);

        // Check if object exists
        if (transformObject == null && player.isGrounded)
        {
            // Instantiate
            transformObject = Instantiate(_obj.extendableObject, instantiatePoint.transform.position, _obj.extendableObject.transform.rotation) as GameObject;
            
            // Play sound effect
            audioSource.Play();
            
            objectCreated = true;
            isDestroyed = false;

            // Set the object as the child of the player
            transformObject.transform.SetParent(transform);
            transformObject.name = "NEW_GAME_OBJECT!!!";

            // Fetch all points
            Point[] points = _obj.extendableObject.GetComponentsInChildren<Point>();

            // Iterate through the amounts of object points
            for (int i = 0; i < points.Length; i++)
            {
                // Initialize the objects
                extendPoint = GameObject.FindGameObjectWithTag(points[0].NameTag).transform;
                collapsePoint = GameObject.FindGameObjectWithTag(points[1].NameTag).transform;

                Debug.Log(points[i].NameTag);
            }

            // Instantiate the detect point
            GameObject newDetectPoint = Instantiate(_obj.detectPoint, extendPoint.transform.position - detectPointOffset, Quaternion.identity) as GameObject;
            newDetectPoint.TryGetComponent<DetectPoint>(out var point);

            PlayerManager playermanag = GetComponent<PlayerManager>();
            switch (playermanag.playerType)
            {
                case PlayerManager.PlayerType.ARTIST:
                    // Set detect point to Brigde type
                    point.connectPoint = Point.ConnectPointType.BRIDGE_TYPE;
                    point.NameTag = "BridgeType";
                    point.tag = point.NameTag;
                    break;

                case PlayerManager.PlayerType.DEVELOPER:
                    // Set detect point to Ladder type]
                    point.connectPoint = Point.ConnectPointType.LADDER_TYPE;
                    point.NameTag = "LadderType";
                    point.tag = point.NameTag;
                    break;

                case PlayerManager.PlayerType.DESIGNER:
                    // Set detect point to Grappling type
                    point.connectPoint = Point.ConnectPointType.GRAPPLING_TYPE;
                    point.NameTag = "GrapplingType";
                    point.tag = point.NameTag;
                    break;
            }

            // Initalize the point to the detectpoint
            detectPoint = newDetectPoint.transform;
            newDetectPoint.transform.SetParent(transformObject.transform);

            // Trigger the collider
            TryGetComponent<CapsuleCollider2D>(out var collider);
            collider.isTrigger = true;

            // Disable the sprite renderer
            SpriteRenderer sprite = GetComponentInChildren<SpriteRenderer>();
            sprite.enabled = false;

            // Freeze players movement
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
            ableToMove = false;

            // Set the artist as transformed
            isTransformed = true;
        }
    }

    protected IEnumerator ExtendObject(Transform _extendPoint)
    {
        if (isExtending || reached_endPoint || reached_connectPoint || _extendPoint == null)
            yield break; // Exit the coroutine if already extending or object doesnt exist

        isRetracting = false;
        reached_endPoint = false;
        isExtending = true;

        // Calculate the target scale
        float targetScaleX = initlialScale.x + extendDistance;

        audioSource.Play();

        // While the current scale is less than the target scale
        while (_extendPoint != null && _extendPoint.localScale.x < targetScaleX && !isRetracting)
        {
            // Calculate the new scale based on the extend time
            float newScaleX = _extendPoint.localScale.x + extendSpeed * Time.deltaTime;

            // Ensure the new scale doesn't exceed the target scale
            newScaleX = Mathf.Min(newScaleX, targetScaleX);

            // Extend the object
            _extendPoint.localScale = new Vector3(newScaleX, transformObject.transform.localScale.y, transformObject.transform.localScale.z);

            yield return null;
        }

        if (Mathf.Approximately(_extendPoint.localScale.x, targetScaleX))
        {
            reached_endPoint = true;
            isExtending = false;
            ableToMove = false;
            // transformed = true;
        }
    }

    protected IEnumerator RetractObject(Transform _extendPoint)
    {
        if (isRetracting)
            yield break; // Exit the coroutine if already retracting

        if (isExtending || reached_endPoint || reached_connectPoint)
        {
            isExtending = false;
            reached_endPoint = false;
            reached_connectPoint = false;
            freeze = false;

            float targetScaleX = initlialScale.x;

            audioSource.Play();
            
            // While the current scale is greater than the target scale
            while (_extendPoint.localScale.x > targetScaleX)
            {
                isRetracting = true;
                ableToMove = false;

                // Decrement the retract time
                float newScaleX = _extendPoint.localScale.x - extendSpeed * Time.deltaTime;

                // Ensure the new scale doesn't go below the target scale
                newScaleX = Mathf.Max(newScaleX, targetScaleX);

                // Retract the object
                _extendPoint.localScale = new Vector3(newScaleX, transformObject.transform.localScale.y, transformObject.transform.localScale.z);

                yield return null;
            }

            if (_extendPoint.localScale == initlialScale)
            {
                DestroyTransformedObject(transformObject);
                isRetracting = false;

                // Untrigger the collider
                TryGetComponent<CapsuleCollider2D>(out var collider);
                collider.isTrigger = false;

                // Enable the sprite renderer
                SpriteRenderer sprite = GetComponentInChildren<SpriteRenderer>();
                sprite.enabled = true;

                // Unfreeze players movement
                Rigidbody2D rb = GetComponent<Rigidbody2D>();
                rb.constraints = RigidbodyConstraints2D.None;
                rb.constraints = RigidbodyConstraints2D.FreezeRotation;
                ableToMove = true;
                // transformed = false;

                // Reset the transformed flag
                isTransformed = false;
            }
        }
    }

    protected IEnumerator CollapseObject(Transform _collapsePoint)
    {
        if (isCollapsing)
            yield break; // Exit the coroutine if already collapsing

        if (reached_connectPoint && transformObject != null)
        {
            float targetScaleX = 0;
    
            // While the current scale is greater than the target scale and continueCollapse flag is true
            while (_collapsePoint.localScale.x > 0 && continueCollapse)
            {
                isCollapsing = true;

                // Decrement the scale over time
                float newScaleX = _collapsePoint.localScale.x - collapseSpeed * Time.deltaTime;

                // Ensure the new scale doesn't go below the target scale
                newScaleX = Mathf.Max(newScaleX, targetScaleX);

                // Update the scale of the object
                _collapsePoint.localScale = new Vector3(newScaleX, transformObject.transform.localScale.y, transformObject.transform.localScale.z);

                // Add a sphere collider
                var script = transformObject.GetComponent<Swag>();
                var spriteRenderer = script.spriteRenderer;

                // This is for when having to teleport onto the ladder
                if (TryGetComponent<PlayerManager>(out var player) && player.playerType == PlayerManager.PlayerType.ARTIST)
                {
                    var childObj = spriteRenderer.transform.Find("Collider").gameObject;
                    ChildObj = childObj;

                    TryGetComponent<CircleCollider2D>(out var circleCol);
                    if(circleCol == null)
                    {
                        circleCol = childObj.AddComponent<CircleCollider2D>();
                        circleCol.isTrigger = true;
                        circleCol.radius = radius;
                    }

                    Collider2D[] hitColliders = Physics2D.OverlapCircleAll(ChildObj.transform.position, circleCol.radius);

                    foreach (var hitCollider in hitColliders)
                    {
                        if (hitCollider != circleCol && hitCollider.gameObject.CompareTag("Ladder"))
                        {
                            ladder = hitCollider.gameObject;
                            Debug.Log("Made Contact with the ladder while transformed");
                            teleportToLadder = true;
                            continueCollapse = false;
                            isCollapsing = false;

                            audioSource.Play();

                            break;
                        }
                    }
                }

                yield return null;
            }

            // Once the object is collapsed
            if (Mathf.Approximately(_collapsePoint.localScale.x, targetScaleX) || teleportToLadder)
            {
                DestroyTransformedObject(transformObject);
                isCollapsing = false;
                reached_connectPoint = false;
                freeze = false;
                ableToMove = true;

                // Reset continueCollapse and teleportToLadder flags
                continueCollapse = true;

                // Set player position to the collapse point position or the ladder position
                if (!teleportToLadder)
                {
                    transform.position = _collapsePoint.position + teleportOffset;
                }
                else
                {
                    // Use the ladder position and adjust the offset values
                    transform.position = ladder.transform.position + ladderOffset;

                    // Debugging: Visualize the calculated position
                    Debug.Log("Teleporting to Ladder at: " + (ChildObj.transform.position + ladderOffset).ToString());

                    teleportToLadder = false;
                }

                // Untrigger the collider
                TryGetComponent<CapsuleCollider2D>(out var collider);
                collider.isTrigger = false;

                // Enable the sprite renderer
                SpriteRenderer sprite = GetComponentInChildren<SpriteRenderer>();
                sprite.enabled = true;

                // Unfreeze players movement
                Rigidbody2D rb = GetComponent<Rigidbody2D>();
                rb.constraints = RigidbodyConstraints2D.None;
                rb.constraints = RigidbodyConstraints2D.FreezeRotation;
                ableToMove = true;
            }
        }
    }
    
    // Is for the designer
    protected void TransformBack()
    {
        if (!isExtending && !isRetracting && !isCollapsing && !reached_endPoint && !reached_connectPoint)
        {
            // Transform back only when not retracting
            TryGetComponent<CapsuleCollider2D>(out var collider);
            collider.isTrigger = false;

            // Enable the sprite renderer
            SpriteRenderer sprite = GetComponentInChildren<SpriteRenderer>();
            sprite.enabled = true;

            // Unfreeze players movement
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            rb.constraints = RigidbodyConstraints2D.None;
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            ableToMove = true;
            // transformed = false;

            DestroyTransformedObject(transformObject);

            // Reset the transformed flag
            isTransformed = false;
        }
    }

    public void RotateObject(float rotationAmount)
    {
        if (transformObject != null && isTransformed && !reached_connectPoint && !reached_endPoint && !isExtending && !isRetracting)
        {
            // Get the current rotation angle
            float currentRotation = transformObject.transform.localRotation.eulerAngles.z;

            // Adjust the current rotation to be within the range of -180 to 180 degrees
            currentRotation = Mathf.Repeat(currentRotation + 180f, 360f) - 180f;

            // Calculate the target rotation angle after applying the rotation amount
            float targetRotation = currentRotation + (rotationAmount * rotateSpeed);

            // Clamp the target rotation angle within the specified limits
            float clampedRotation = Mathf.Clamp(targetRotation, -70f, 70f);

            // Apply the clamped rotation to the object
            Quaternion newRotation = Quaternion.Euler(0f, 0f, clampedRotation);
            transformObject.transform.localRotation = newRotation;
        }
    }

    public void Freeze()
    {
        // Stop the extending
        if (coroutine != null)
            StopCoroutine(coroutine);

        isExtending = false;
        reached_connectPoint = true;
        freeze = true;
    }

    bool DestroyTransformedObject(GameObject _gameObject)
    {
        Destroy(_gameObject);
        isDestroyed = true;
        objectCreated = false;
        return false;
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        if (ChildObj != null)
            Gizmos.DrawSphere(ChildObj.transform.position, radius);
    }
}
