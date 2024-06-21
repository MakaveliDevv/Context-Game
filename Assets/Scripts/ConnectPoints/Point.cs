using UnityEngine;

public class Point : MonoBehaviour
{
    public enum PointType 
    {
        DEFAULT,
        CONNECT_POINT,
        DETECT_POINT
    }

    public enum ConnectPointType 
    {
        DEFAULT,
        BRIDGE_TYPE,
        LADDER_TYPE,
        GRAPPLING_TYPE
    }

    public PointType type;
    public ConnectPointType connectPoint;
    public string NameTag;

    private void Update() 
    {
        NameTag = connectPoint switch
        {
            ConnectPointType.BRIDGE_TYPE => "BridgeType",
            ConnectPointType.LADDER_TYPE => "LadderType",
            ConnectPointType.GRAPPLING_TYPE => "GrapplingType",
            _ => tag,
        };

        // Assign the NameTag and update the gameObject's tag
        gameObject.tag = NameTag;
    }
}