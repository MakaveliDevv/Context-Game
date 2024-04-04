using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Controller : MonoBehaviour
{
    // Player stuff
    [Header("Some Stuff")]
    [SerializeField] protected ExtendableObj scriptableObj; // Scriptable object
    protected Coroutine coroutine;
    private GameObject newGameObject;

    // Points
    [Header("Points")]
    [SerializeField] protected Transform instantiatePoint;
    protected Transform extendPoint, collapsePoint, detectPoint; 
    [SerializeField] protected Vector3 detectPointOffset; 

    // Bools
    [Header("Bools")]
    public bool isExtending, isRetracting, isCollapsing, ableToMove;
    public bool objectCreated, reached_endPoint, reached_connectPoint, isDestroyed;
    public bool freeze;

    // Floats and such
    [Header("Floats And Such")]
    [SerializeField] private Vector3 teleportOffset;
    private Vector3 initlialScale;
    private float initialScaleX; // Meant for the X axis
    [SerializeField] private float extendDistance = 5f;
    [SerializeField] private float extendSpeed = 5f;
    [SerializeField] private float collapseSpeed;

    void Start() 
    {
        initlialScale = scriptableObj.initialScale;
        initialScaleX = initlialScale.x;

        ableToMove = true;
    }

    protected void CreateObject(ExtendableObj _obj) 
    {
        TryGetComponent<PlayerController>(out var player);

        // Check if object exists
        if (newGameObject == null && player.isGrounded)
        {
            // Instantiate
            newGameObject = Instantiate(_obj.extendableObject, instantiatePoint.transform.position, _obj.extendableObject.transform.rotation) as GameObject;
            
            // Set the object as the child of the player
            newGameObject.transform.SetParent(transform);
            newGameObject.name = "NEW_GAME_OBJECT!!!";

            // Fetch all points
            Point[] pivotPoints = _obj.extendableObject.GetComponentsInChildren<Point>();

            // Iterate through the amounts of object points
            for (int i = 0; i < pivotPoints.Length; i++)
            {
                // Initialize the objects
                extendPoint = GameObject.FindGameObjectWithTag(pivotPoints[0].NameTag).transform;
                collapsePoint = GameObject.FindGameObjectWithTag(pivotPoints[1].NameTag).transform;

                Debug.Log(pivotPoints[i].NameTag);
            }

            // Instantiate the detect point
            GameObject newDetectPoint = Instantiate(_obj.detectPoint, extendPoint.transform.position - detectPointOffset, Quaternion.identity) as GameObject;
           
           // Initalize the point to the detectpoint
            detectPoint = newDetectPoint.transform;
            newDetectPoint.transform.SetParent(newGameObject.transform);

            objectCreated = true;
            isDestroyed = false;
        }
    }
      
    protected IEnumerator ExtendObject(ExtendableObj _obj, Transform _extendPoint)
    {
        if (isExtending || _extendPoint == null)
            yield break; // Exit the coroutine if already extending or object doesnt exist

        isRetracting = false;
        reached_endPoint = false;
        isExtending = true;

        // Calculate the target scale
        float targetScaleX = initialScaleX + extendDistance;

        // While the current scale is less than the target scale
        while (_extendPoint != null && _extendPoint.localScale.x < targetScaleX && !isRetracting)
        {
            // Calculate the new scale based on the extend time
            float newScaleX = _extendPoint.localScale.x + extendSpeed * Time.deltaTime;

            // Ensure the new scale doesn't exceed the target scale
            newScaleX = Mathf.Min(newScaleX, targetScaleX);

            // Extend the object
            _extendPoint.localScale = new Vector3(newScaleX, _obj.extendableObject.transform.localScale.y, _obj.extendableObject.transform.localScale.z);

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

            yield return null;
        }

        if (Mathf.Approximately(_extendPoint.localScale.x, targetScaleX)) 
        {
            reached_endPoint = true;
            isExtending = false;
            ableToMove = false;
        }
    }

    protected IEnumerator RetractObject(ExtendableObj _obj, Transform _extendPoint)
    {
        if (isRetracting)
            yield break; // Exit the coroutine if already retracting

   
        if(isExtending || reached_endPoint || reached_connectPoint) 
        {
            isExtending = false;
            reached_endPoint = false;
            reached_connectPoint = false;
            freeze = false;

            float targetScaleX = initialScaleX;

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
                _extendPoint.localScale = new Vector3(newScaleX, _obj.extendableObject.transform.localScale.y, _obj.extendableObject.transform.localScale.z);

                yield return null;
            }

            if(_extendPoint.localScale == initlialScale) 
            {
                DestroyObject(newGameObject);
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
            }
        }
    }

    protected IEnumerator CollapseObject(ExtendableObj _obj, Transform _collapsePoint) 
    {
        if (isCollapsing)
            yield break; // Exit the coroutine if already retracting

   
        if(reached_connectPoint) 
        {
            float targetScaleX = 0;

            // While the current scale is greater than the target scale
            while (_collapsePoint.localScale.x > 0)
            {
                isCollapsing = true;

                // Decrement the retract time
                float newScaleX = _collapsePoint.localScale.x - collapseSpeed * Time.deltaTime;

                // Ensure the new scale doesn't go below the target scale
                newScaleX = Mathf.Max(newScaleX, targetScaleX);

                // Retract the object
                _collapsePoint.localScale = new Vector3(newScaleX, _obj.extendableObject.transform.localScale.y, _obj.extendableObject.transform.localScale.z);

                yield return null;
            }

            if (Mathf.Approximately(_collapsePoint.localScale.x, targetScaleX)) 
            {
                DestroyObject(newGameObject);
                isCollapsing = false;
                reached_connectPoint = false;
                freeze = false;
                ableToMove = true;

                // Set player position to the collapse point position
                transform.position = _collapsePoint.position + teleportOffset;

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

    public void Freeze() 
    {
        // Stop the extending
        if(coroutine != null) 
            StopCoroutine(coroutine);

        isExtending = false;
        reached_connectPoint = true;
        freeze = true;
    }

    bool DestroyObject(GameObject _gameObject) 
    {
        Destroy(_gameObject);
        isDestroyed = true;
        objectCreated = false;
        return false;
    }
}
