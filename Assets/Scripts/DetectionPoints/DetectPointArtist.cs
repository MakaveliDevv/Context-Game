using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectPointArtist : MonoBehaviour
{
    private DetectionPoint _dPoint;
    private GameManager _gManager;
    [SerializeField] private float detectRadius;
    public PlayerManager _pManager;

    Vector3 previousPosition;
    private bool isMoving = false;


    void Awake() 
    {
        _pManager = PlayerManager.instance;
    }

    void Start()
    {
        _dPoint = this.GetComponent<DetectionPoint>();
        _gManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        previousPosition = transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        if(_dPoint.Moving()) 
        {
            
        }
    }

    public bool Moving() 
    {
        Vector3 currenPosition = transform.position;
        if(currenPosition != previousPosition) 
        {
            previousPosition = currenPosition;
            isMoving = true;
            return true;
        
        } else 
        {
            isMoving = false;
        }
        
        return false;
    }

    public bool PointDetectedd() 
    {
        _gManager.connectPointList.Clear();
        Collider2D hitPoint = Physics2D.OverlapCircle(transform.position, detectRadius);

        if (hitPoint != null)
        {
            Point point = hitPoint.GetComponent<Point>();

            if (point != null && point.pointName == Point.PointName.BRIDGE_POINT)
            {
                _gManager.connectPointList.Add(point);
                Debug.Log("Connect point detected!");
                return true;
            }
        }

        return false;
    }

    public void OnDrawGizmosSelected() 
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectRadius);
    }
}
