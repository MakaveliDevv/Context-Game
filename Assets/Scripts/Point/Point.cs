using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Point : MonoBehaviour
{
    public enum Type 
    {
        PIVOT_POINT,
        CONNECT_POINT,
        DETECT_POINT
    }

    public Type type;
    public string NameTag;

    private void Update() 
    {
        gameObject.tag = NameTag;
    }
}



//  public enum PointType 
//     {
//         PIVOT_POINT,
//         CONNECT_POINT,
//         DETECT_POINT
//     }

//     public enum ConnectPointType 
//     {
//         BRIDGE_TYPE,
//         LADDER_TYPE,
//         GRAPPLING_TYPE
//     }

//     public PointType type;
//     public ConnectPointType connectType;
//     public string NameTag;

//     private void Update() 
//     {
//         if(type != PointType.PIVOT_POINT) 
//         {
//             // Determine the NameTag based on ConnectPointType
//             NameTag = connectType switch
//             {
//                 ConnectPointType.BRIDGE_TYPE => "BridgeType",
//                 ConnectPointType.LADDER_TYPE => "LadderType",
//                 ConnectPointType.GRAPPLING_TYPE => "GrapplingType",
//                 _ => "Default",
//             };

//             // Assign the NameTag and update the gameObject's tag
//             gameObject.tag = NameTag;
//         }
//     }
