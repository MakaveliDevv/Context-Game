using System.Collections;
using UnityEngine;

public class Controller : MonoBehaviour
{
    // PLAYER STUFF
    [Header("Player Stuff")]
    [SerializeField] protected ScriptObject _scriptObj; // SCRIPTABLE OBJECT
    protected Mover _player; // PLAYER CONTROLLER
    protected Coroutine coroutine; // COROUTINE
    public PlayerManager _playerManag; // PLAYER MANAGER


    // POINTS
    [Header("Points")]
    public GameObject _instantiateObj; // INSTANTIATE OBJECT 
    protected GameObject startPointObj, detectObj; // START AND END POINT GAME OBJECT
    [SerializeField] protected DetectionPoint _detectPoint; // DETECTPOINT SCRIPT

    protected Transform extendPoint1, extendPoint2, toExtandBack; // EXTEND POINTS OBJECT PREFAB
    [SerializeField] protected Transform instantiatePoint, startPoint, endPoint; // INSTANTIATE, START, END POINT


    // BOOLS
    [Header("Bools")]
    public bool isExpanding; // CHECK FOR EXPANDING 
    public bool isExpandingBack; // CHECK FOR EXPANDING BACK
    public bool stopScalingCuzEndPointReached; // STOP SCALING
    public bool teleportedToEndPoint; // TELEPORT TO END POINT
    protected bool objectCreated; // CHECK IF OBJECT CREATED
    public bool isDestroyed; // CHECK IF DESTROYED

    // FLOATS AND SUCH
    [Header("Floats And Such")]
    [SerializeField] private float scaleFactor = 10f;
    [SerializeField] private Vector3 teleportOffset;
    [SerializeField] private float timeToSpawnObject = 1f;


    void Start() 
    {
        _player = GetComponentInParent<Mover>();
        StartCoroutine(CreateObject());
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

    protected IEnumerator CreateObject() 
    {
        isDestroyed = false;
        objectCreated = false;

        yield return new WaitForSeconds(timeToSpawnObject);

        // CHECK IF OBJECT IS NULL
        if (_instantiateObj == null)
        {   
            // INSTANTIATE
            _instantiateObj = Instantiate(_scriptObj.@object, instantiatePoint.transform.position, _scriptObj.@object.transform.rotation);
            
            // SET THE @OBJECT AS CHILD OF PLAYER
            _instantiateObj.transform.SetParent(_player.transform);
            _instantiateObj.name = _scriptObj.name;

            // FETCH ALL 'ObjectPoint'
            ObjectPoint[] objPoints = _instantiateObj.GetComponentsInChildren<ObjectPoint>();

            // ITERATE THROUGH THE OBJECT POINTS LENGTH
            for (int i = 0; i < objPoints.Length; i++)
            {
                // INTIALIZE ALL OBJECT POINTS TO THE EMPTY VARIABLES
                extendPoint1 = GameObject.FindGameObjectWithTag(objPoints[0].NameTag).transform;
                extendPoint2 = GameObject.FindGameObjectWithTag(objPoints[1].NameTag).transform;
                toExtandBack = GameObject.FindGameObjectWithTag(objPoints[2].NameTag).transform; 
                startPoint = GameObject.FindGameObjectWithTag(objPoints[3].NameTag).transform;
                endPoint = GameObject.FindGameObjectWithTag(objPoints[4].NameTag).transform;
                
                Debug.Log(objPoints[i].NameTag);
            }

            // SET FIRST EXTEND POINT SCALE FACTOR TO THE @OBJECT SCALE FACTOR
            extendPoint1.localScale = _scriptObj.initialScale;
            
            // INSTANTIATE START POINT, SET AS CHILD OF PLAYER
            startPointObj = Instantiate(_scriptObj.startObj, extendPoint1.position, Quaternion.identity);
            startPointObj.name = "NewStartPointPrefab";
            startPointObj.transform.SetParent(_player.transform);

            // INSTANTIATE END POINT (detectpoint), SET AS CHILD OF PLAYER
            detectObj = Instantiate(_scriptObj.detectObj, extendPoint2.position, Quaternion.identity) as GameObject;
            detectObj.name = "Detect Point";
            detectObj.transform.SetParent(_player.transform);

            // FLAG OBJECT CREATED            
            objectCreated = true;
        }

    }

    protected IEnumerator CalculateScaling(GameObject _scaleObj, Vector3 _targetDirection)
    {
        while (true) // Continuously scale
        {
            // FETCH INITIAL SCALE
            Vector3 initialScale = new(_scaleObj.transform.localScale.x, _scaleObj.transform.localScale.y, _scaleObj.transform.localScale.z);

            // FETCH TARGET SCALE
            Vector3 targetScale = initialScale + scaleFactor * Time.deltaTime * _targetDirection;

            // SET THE SCALE OF THE OBJECT TO THE TARGET SCALE
            _scaleObj.transform.localScale = targetScale;
            
            // FLAG IS EXPANDING TO TRUE
            isExpanding = true;
            
            // GET SCRIPT COMPONENT FROM THE END POINT (detectpoint)
            _detectPoint = detectObj.GetComponent<DetectionPoint>();

            // CHECK FOR CONNECT POINT WHILE SCALING
            if (_detectPoint.PointDetected())
            {                
                // STOP SCALING ONCE DETECT POINT REACHED
                FreezeScaling(_scaleObj, _scaleObj.transform.localScale);
            } 

            yield return null;
        }
    }

    protected IEnumerator ScaleBack(GameObject _scaleObj, Vector3 _startScale, Vector3 _targetScale)
    {
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
            
            // SET PLAYER MOVEMENT BACK TO THE INPUT DIRECTION AFTER DESTROYED

            // _player.rb.velocity = _player.inputDirection;
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
}
