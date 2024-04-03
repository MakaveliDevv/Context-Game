using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Controller : MonoBehaviour
{
    // PLAYER STUFF
    [Header("Player Stuff")]
    [SerializeField] protected ExtendableObj scriptableObj; // SCRIPTABLE OBJECT
    protected Coroutine coroutine; // COROUTINE
    public PlayerManager _playerManag; // PLAYER MANAGER


    // POINTS
    [Header("Points")]
    public GameObject extandableObj; // INSTANTIATE OBJECT 
    protected GameObject startPointObj, detectObj; // START AND END POINT GAME OBJECT
    [SerializeField] protected DetectionPoint _detectPoint; // DETECTPOINT SCRIPT

    protected Transform extendPoint, extendPoint2, toExtandBack; // EXTEND POINTS OBJECT PREFAB
    [SerializeField] protected Transform instantiatePoint, startPoint, endPoint; // INSTANTIATE, START, END POINT


    // BOOLS
    [Header("Bools")]
    private bool isExtending = false;
    private bool isRetracting;
    private bool stopScalingCuzEndPointReached; // STOP SCALING
    private bool teleportedToEndPoint; // TELEPORT TO END POINT
    private bool objectCreated; // CHECK IF OBJECT CREATED
    private bool isDestroyed; // CHECK IF DESTROYED

    // FLOATS AND SUCH
    [Header("Floats And Such")]
    [SerializeField] private Vector3 teleportOffset;
    public float initialScaleX;
    public float extendDistance = 5f;
    public float extendSpeed = 5f;
    private bool isCollapsing;

    Vector3 initlialScale;

    void Start() 
    {
        initlialScale = scriptableObj.initialScale;
        initialScaleX = initlialScale.x;
    }

    void Update() 
    {
        if(startPointObj != null && startPoint != null 
        && detectObj != null && endPoint != null) 
        {
            startPointObj.transform.position = startPoint.transform.position;
            detectObj.transform.position = endPoint.transform.position;
        }
    }

    public void CreateObject(GameObject _obj) 
    {
        // CHECK IF OBJECT IS NULL
        if (_obj == null)
        {   
            // INSTANTIATE
            _obj = Instantiate(scriptableObj.extendableObj, instantiatePoint.transform.position, scriptableObj.extendableObj.transform.rotation);
            
            // SET THE @OBJECT AS CHILD OF PLAYER
            _obj.transform.SetParent(transform);
            _obj.name = scriptableObj.name;
            // _obj.transform.localScale = initlialScale;

            // FETCH ALL 'ObjectPoint'
            ObjectPoint[] objPoints = _obj.GetComponentsInChildren<ObjectPoint>();

            // Iterate through the amounts of object points
            for (int i = 0; i < objPoints.Length; i++)
            {
                // Initialize the objects
                extendPoint = GameObject.FindGameObjectWithTag(objPoints[0].NameTag).transform;
                extendPoint2 = GameObject.FindGameObjectWithTag(objPoints[1].NameTag).transform;
                toExtandBack = GameObject.FindGameObjectWithTag(objPoints[2].NameTag).transform; 
                startPoint = GameObject.FindGameObjectWithTag(objPoints[3].NameTag).transform;
                endPoint = GameObject.FindGameObjectWithTag(objPoints[4].NameTag).transform;
                
                Debug.Log(objPoints[i].NameTag);
            }

            // SET FIRST EXTEND POINT SCALE FACTOR TO THE extendable object SCALE FACTOR
            extendPoint.localScale = scriptableObj.initialScale;
            
            // INSTANTIATE START POINT, SET AS CHILD OF PLAYER
            startPointObj = Instantiate(scriptableObj.startObj, extendPoint.position, Quaternion.identity);
            startPointObj.name = "NewStartPointPrefab";
            // startPointObj.transform.SetParent(player.transform);
            startPointObj.transform.SetParent(_obj.transform);

            // INSTANTIATE END POINT (detectpoint), SET AS CHILD OF PLAYER
            detectObj = Instantiate(scriptableObj.detectObj, extendPoint2.position, Quaternion.identity) as GameObject;
            detectObj.name = "Detect Point";
            // detectObj.transform.SetParent(player.transform);
            detectObj.transform.SetParent(_obj.transform);

            // FLAG OBJECT CREATED            
            objectCreated = true;
        }
    }

   
    public IEnumerator ExtendObject(Transform _extendPoint)
    {
        if (isExtending)
            yield break; // Exit the coroutine if already extending
    
        isExtending = true;

        // Calculate the target scale
        float targetScaleX = initialScaleX + extendDistance;

        // While the current scale is less than the target scale
        while (_extendPoint.localScale.x < targetScaleX && !isRetracting)
        {
            // Calculate the new scale based on the extend time
            float newScaleX = _extendPoint.localScale.x + extendSpeed * Time.deltaTime;

            // Ensure the new scale doesn't exceed the target scale
            newScaleX = Mathf.Min(newScaleX, targetScaleX);

            // Extend the object
            _extendPoint.localScale = new Vector3(newScaleX, _extendPoint.localScale.y, _extendPoint.localScale.z);

            yield return null;
        }

        isExtending = false;
    }

    public IEnumerator RetractObject(GameObject _obj, Transform _extendPoint)
    {
        if (isRetracting)
            yield break; // Exit the coroutine if already retracting

        if(isExtending) 
        {
            float targetScaleX = initialScaleX;
            
            if(_extendPoint != null) 
            {
                // While the current scale is greater than the target scale
                while (_extendPoint.localScale.x > targetScaleX)
                {
                    isExtending = false;
                    isRetracting = true;

                    // Decrement the retract time
                    float newScaleX = _extendPoint.localScale.x - extendSpeed * Time.deltaTime;

                    // Ensure the new scale doesn't go below the target scale
                    newScaleX = Mathf.Max(newScaleX, targetScaleX);

                    // Retract the object
                    _extendPoint.localScale = new Vector3(newScaleX, _extendPoint.localScale.y, _extendPoint.localScale.z);

                    yield return null;
                }

                if(_extendPoint.localScale == initlialScale) 
                {
                    Destroy(_obj);
                    isRetracting = false;
                }
            }
        } 
    }

        // Fetch the target scale
        // targetScaleX = initialScaleX + extendDistance;

        // Calculate the scaling process
        // float newScaleX = Mathf.Lerp(_scaleObj.transform.localScale.x, targetScaleX, extendSpeed * Time.deltaTime);

        // Extend the object
        // _scaleObj.transform.localScale = new Vector3(newScaleX, _scaleObj.transform.localScale.y, _scaleObj.transform.localScale.z);

        // Flag it to true
        // isExpanding = true;

        // CapsuleCollider2D capsuleCol = GetComponent<CapsuleCollider2D>();
        // capsuleCol.isTrigger = false;

        // Rigidbody2D rb = GetComponent<Rigidbody2D>();
        // rb.constraints = RigidbodyConstraints2D.None;
        // rb.constraints = RigidbodyConstraints2D.FreezeRotation;

        // Get the script compontent
        // _detectPoint = detectObj.GetComponent<DetectionPoint>();

        // while (true) // Continuously scale
        // {
        // Check for a collision while expanding
        // if (_detectPoint.PointDetected())
        // {                
        //     // Stop scaling once reaching a checkpoint
        //     FreezeScaling(_scaleObj, _scaleObj.transform.localScale);
        // } 
        // }

        // yield return null;

    // protected IEnumerator ExtendBack(GameObject _scaleObj)
    // {
    //     if(isExtendingBack) 
    //         yield break;

    //     isExtendingBack = true;

    //     // Fetch the target scale
    //     float targetScaleX = initialScaleX;

    //     // While the current scale is less than the target scale
    //     while (_scaleObj.transform.localScale.x > targetScaleX)
    //     {
    //         // Increment the extend time
    //         extendBackTime += Time.deltaTime;

    //         // Calculate the new scale based on the extend time
    //         float newScaleX = _scaleObj.transform.localScale.x - extendSpeed * Time.deltaTime;

    //         // Ensure the new scale doesn't exceed the target scale
    //         newScaleX = Mathf.Min(newScaleX, targetScaleX);

    //         // Extend the object
    //         _scaleObj.transform.localScale = new Vector3(newScaleX, _scaleObj.transform.localScale.y, _scaleObj.transform.localScale.z);

    //         yield return null;
    //     }
    //     // Reset extend time and flag
    //     extendBackTime = 0f;
    //     isExtendingBack = false;
        // float startTime = Time.time;
        // float journeyLength = Vector3.Distance(_startScale, _targetScale);

        // if(_scaleObj != null && !isDestroyed) 
        // {
        //     while (_scaleObj.transform.localScale != _targetScale)
        //     {
        //         float journeyTime = Time.time - startTime;
        //         float fracJourney = journeyTime * scaleFactor / journeyLength;
        //         _scaleObj.transform.localScale = Vector3.Lerp(_startScale, _targetScale, Mathf.Clamp01(fracJourney));

        //         isExpandingBack = true;
        //         isExpanding = false;

        //         yield return null;
        //     }

        //     // Ensure the scale is exactly the target scale when done
        //     _scaleObj.transform.localScale = _targetScale;
        //     isExpandingBack = false;
        // }           

    // }

    // SCALE BACK TOWARDS THE ENDPOINT
    // protected IEnumerator CollapseTowardsEndPoint(Transform _scalePoint, Transform _point, Vector3 _startPosition)
    // {
    //     if (isExtending || isRetracting)
    //         yield break; // Exit the coroutine if already extending
    
    //     isCollapsing = true;

    //     // Calculate the target scale
    //     float targetScaleX = _point.localScale.x;

    //     // While the current scale is
    //     while ()
    //     {
    //         // Calculate the new scale based on the extend time
    //         float newScaleX = _scaleObj.transform.localScale.x + extendSpeed * Time.deltaTime;

    //         // Ensure the new scale doesn't exceed the target scale
    //         newScaleX = Mathf.Min(newScaleX, targetScaleX);

    //         // Extend the object
    //         _scaleObj.transform.localScale = new Vector3(newScaleX, _scaleObj.transform.localScale.y, _scaleObj.transform.localScale.z);

    //         yield return null;
    //     }

    //     isExtending = false;
        
    //     float elapsedTime = 0f;
    //     float duration = 2f;
    //     Vector3 targetScale = new(0, 1, 1);
    //     Point point = startPointObj.GetComponent<Point>();

    //     if(_scaleObj != null) 
    //     {
    //         while (elapsedTime < duration)
    //         {
    //             // Interpolate between start and target scale
    //             float t = elapsedTime / duration;
    //             _scaleObj.transform.localScale = Vector3.Lerp(_startPosition, targetScale, t);
                
    //             // if(_point.Moving(startPointObj.transform)) 
    //             // {
    //             //     // Debug.Log("We are moving");
    //             //     _player.playerRenderer.SetActive(false);
    //             // }
                
    //             // Increment elapsed time
    //             elapsedTime += Time.deltaTime;

    //             yield return null;
    //         }            
    //     }

    //     // Ensure the scale is exactly the target scale when done
    //     _scaleObj.transform.localScale = targetScale;

    //     if(_scaleObj.transform.localScale == targetScale) 
    //     {
    //         Debug.Log("Reached the endpoint");
    //         transform.position = detectObj.transform.position + teleportOffset;

    //         teleportedToEndPoint = true;
    //         // player.playerRenderer.SetActive(true);
            

    //         CapsuleCollider2D capsuleCol = GetComponent<CapsuleCollider2D>();
    //         capsuleCol.isTrigger = false;

    //         Rigidbody2D rb = GetComponent<Rigidbody2D>();
    //         rb.constraints = RigidbodyConstraints2D.None;
    //         rb.constraints = RigidbodyConstraints2D.FreezeRotation;

    //         if(DestroyGameObject(instantiateObj)) 
    //         {
    //             DestroyGameObject(detectObj);
    //             DestroyGameObject(startPointObj);
                
    //             isDestroyed = true;
    //             objectCreated = false;
    //             _point.isMoving = false;
    //         }
    //     }
        
    //     stopScalingCuzEndPointReached = false;
    //     teleportedToEndPoint = false;

    //     yield return null;
    // }

    // public bool DestroyGameObject(GameObject _object) 
    // {
    //     Destroy(_object);

    //     return true;
    // }

    // public void FreezeScaling(GameObject _scaleObj, Vector3 _currentScale) 
    // {
    //     if(coroutine != null)
    //         StopCoroutine(coroutine);

    //     _scaleObj.transform.localScale = _currentScale;
    //     stopScalingCuzEndPointReached = true;

    //     // Reset expansion state flags
    //     isExpanding = false;
    //     isExpandingBack = false;
    // }
}
