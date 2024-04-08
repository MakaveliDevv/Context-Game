using System.Collections;
using System.Collections.Generic;
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
        // gameObject.tag = NameTag;
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

    // When instantiateing an object, instantiate a detect point
    // Set the detect point connect point type based on the type of character
    // So if the character is an artist, set the connect point type to BRIDGE_TYPE and so on for the other characters
    // How do we check which character? By the player type
    // So ife player type is artist then connect point type is bridge
    // Do this with a switch statement
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

    // private void Update() 
    // {
    //     if(type != PointType.PIVOT_POINT) 
    //     {
    //         // Determine the NameTag based on ConnectPointType
    //         NameTag = connectType switch
    //         {
    //             ConnectPointType.BRIDGE_TYPE => "BridgeType",
    //             ConnectPointType.LADDER_TYPE => "LadderType",
    //             ConnectPointType.GRAPPLING_TYPE => "GrapplingType",
    //             _ => "Default",
    //         };

    //         // Assign the NameTag and update the gameObject's tag
    //         gameObject.tag = NameTag;
    //     }
    // }
