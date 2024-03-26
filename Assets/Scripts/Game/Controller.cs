using System.Collections;
using UnityEngine;

public class Controller : MonoBehaviour
{
    [SerializeField] protected ScriptObject _scriptObj;
    protected PlayerController _player;
    protected Coroutine coroutine;
    public PlayerManager _playerManag;
    [SerializeField] protected DetectionPoint _detectPoint;


    // Object Points
    public GameObject _instantiateObj;
    protected GameObject startPointObj, detectObj;

    // Transform
    protected Transform extendPoint1, extendPoint2, toExtandBack;
    [SerializeField] protected Transform instantiatePoint, startPoint, endPoint;


    [SerializeField] public bool isExpanding; 
    [SerializeField] public bool isExpandingBack;
    [SerializeField] public bool stopScalingCuzEndPointReached;
    public bool teleportedToEndPoint;
    protected bool objectCreated;
    public bool isDestroyed;

    protected float timer;
    [SerializeField] protected float timeUntilScaleBack = 4f;
    [SerializeField] private float scaleFactor = 10f;
    [SerializeField] private Vector3 teleportOffset;


    // void Awake() 
    // {
    //     if(!TryGetComponent<PlayerManager>(out _playerManag)) 
    //     {
    //         Debug.LogError("PlayerManager component not found!");
    //     }
    // }

    void Start() 
    {
        _player = GetComponentInParent<PlayerController>();
        StartCoroutine(CreateObject());
    }

    protected IEnumerator CreateObject() 
    {
        isDestroyed = false;
        yield return new WaitForSeconds(2f);

        objectCreated = false;

        if (_instantiateObj == null)
        {   
            // Game Object
            _instantiateObj = Instantiate(_scriptObj.@object, instantiatePoint.transform.position, _scriptObj.@object.transform.rotation);
            
            // Set the @object as a child of the player
            _instantiateObj.transform.SetParent(_player.transform);
            _instantiateObj.name = _scriptObj.name;

            ObjectPoint[] objPoints = _instantiateObj.GetComponentsInChildren<ObjectPoint>();

            for (int i = 0; i < objPoints.Length; i++)
            {

                extendPoint1 = GameObject.FindGameObjectWithTag(objPoints[0].NameTag).transform;
                extendPoint2 = GameObject.FindGameObjectWithTag(objPoints[1].NameTag).transform;
                toExtandBack = GameObject.FindGameObjectWithTag(objPoints[2].NameTag).transform; 
                startPoint = GameObject.FindGameObjectWithTag(objPoints[3].NameTag).transform;
                endPoint = GameObject.FindGameObjectWithTag(objPoints[4].NameTag).transform;
                
                Debug.Log(objPoints[i].NameTag);
            }

            // Set the extendPoint1 scale to the scale of the @object
            extendPoint1.localScale = _scriptObj.initialScale;
            
            // Start Pivot Point
            startPointObj = Instantiate(_scriptObj.startObj, extendPoint1.position, Quaternion.identity);
            startPointObj.name = "NewStartPointPrefab";
            startPointObj.transform.SetParent(_player.transform);

            // Detect point Game Object (End Pivot Point) 
            detectObj = Instantiate(_scriptObj.detectObj, extendPoint2.position, Quaternion.identity) as GameObject;
            detectObj.name = "Detect Point";
            detectObj.transform.SetParent(_player.transform);
            
            // _detectPoint = detectObj.GetComponent<DetectionPoint>();
            objectCreated = true;
        }

    }

    protected IEnumerator Scaling(GameObject _scaleObj, Vector3 _targetDirection)
    {
        while (true) // Continuously scale
        {
            Vector3 initialScale = new(_scaleObj.transform.localScale.x, _scaleObj.transform.localScale.y, _scaleObj.transform.localScale.z);
            Vector3 targetScale = initialScale + scaleFactor * Time.deltaTime * _targetDirection;
            _scaleObj.transform.localScale = targetScale;
            isExpanding = true;
            // Debug.Log("We are scaling!");
            
            _detectPoint = detectObj.GetComponent<DetectionPoint>();

            // // Check for connect points while scaling
            if (_detectPoint.PointDetected())
            {
                // Debug.Log("Point detected!, calling from controller script");
                
                // Stop scaling
                FreezeScaling(_scaleObj, _scaleObj.transform.localScale);
            } 
            yield return null;
        }
    }

    // SCALE BACK TO THE INITIAL POINT
    protected IEnumerator ScaleBack(GameObject _scaleObj, Vector3 _startScale, Vector3 _targetScale)
    {
        // isExpandingBack = false;
        float startTime = Time.time;
        float journeyLength = Vector3.Distance(_startScale, _targetScale);

        if(_scaleObj != null && !isDestroyed) 
        {
            while (_scaleObj.transform.localScale != _targetScale)
            {
                float journeyTime = Time.time - startTime;
                float fracJourney = journeyTime * scaleFactor / journeyLength;
                _scaleObj.transform.localScale = Vector3.Lerp(_startScale, _targetScale, Mathf.Clamp01(fracJourney));

                isExpandingBack = true;
                isExpanding = false;

                yield return null;
            }

            // Ensure the scale is exactly the target scale when done
            _scaleObj.transform.localScale = _targetScale;
            isExpandingBack = false;
        }           

    }

    // SCALE BACK TOWARDS THE ENDPOINT
    protected IEnumerator ExpandBackTowardsEndPoint(GameObject _scaleObj, Vector3 _startPosition)
    {
        teleportedToEndPoint = false;
        stopScalingCuzEndPointReached = true;
        
        float elapsedTime = 0f;
        float duration = 2f;
        Vector3 targetScale = new(0, 1, 1);
        Point _point = startPointObj.GetComponent<Point>();

        if(_scaleObj != null) 
        {
            while (elapsedTime < duration)
            {
                // Interpolate between start and target scale
                float t = elapsedTime / duration;
                _scaleObj.transform.localScale = Vector3.Lerp(_startPosition, targetScale, t);
                
                if(_point.Moving(startPointObj.transform)) 
                {
                    // Debug.Log("We are moving");
                    _player.playerRenderer.SetActive(false);
                }
                
                // Increment elapsed time
                elapsedTime += Time.deltaTime;

                yield return null;
            }            
        }

        // Ensure the scale is exactly the target scale when done
        _scaleObj.transform.localScale = targetScale;

        if(_scaleObj.transform.localScale == targetScale) 
        {
            Debug.Log("Reached the endpoint");
            _player.transform.position = detectObj.transform.position + teleportOffset;

            teleportedToEndPoint = true;
            _player.playerRenderer.SetActive(true);
        }

        if(teleportedToEndPoint) 
        {
            Debug.Log("Teleported to the end point");
        }



        if(DestroyGameObject(_instantiateObj)) 
        {
            DestroyGameObject(detectObj);
            DestroyGameObject(startPointObj);
            
            isDestroyed = true;
            objectCreated = false;
            _point.isMoving = false;
            
            _player.rb.velocity = _player.inputDirection;
        }
        
        stopScalingCuzEndPointReached = false;
        teleportedToEndPoint = false;

        yield return CreateObject(); // Start the creation process again
    }

    public bool DestroyGameObject(GameObject _object) 
    {
        Destroy(_object);

        return true;
    }

    public void FreezeScaling(GameObject _scaleObj, Vector3 _currentScale) 
    {
        if(coroutine != null)
            StopCoroutine(coroutine);

        _scaleObj.transform.localScale = _currentScale;
        stopScalingCuzEndPointReached = true;

        // Reset expansion state flags
        isExpanding = false;
        isExpandingBack = false;
    }

        // Now start rotating back to the initial rotation
        // startTime = Time.time;
        // journeyLength = Quaternion.Angle(startRotation, targetRotation);

        // while (transform.rotation != targetRotation)
        // {
        //     float journeyTime = Time.time - startTime;
        //     float fracJourney = journeyTime * player.scaleFactor / journeyLength;
        //     transform.rotation = Quaternion.Slerp(startRotation, targetRotation, Mathf.Clamp01(fracJourney));

        //     yield return null;
        // }

        // Ensure the rotation is exactly the initial rotation when done
        // transform.rotation = targetRotation;
    
        // Allow player to move again
        // player.rb.velocity = new Vector2(player.inputDirection.x, player.rb.velocity.y);
        // player.isMoving = true;
}
