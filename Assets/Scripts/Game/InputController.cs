using UnityEngine;

public class InputController : Controller
{
    public void Extend() 
    {
        // CHECK IF INSTANTIATED OBJECT IS NOT NULL
        // if(_instantiateObj != null) 
        // {
            // CHECK IF COROUTINE IS ALREADY RUNNING
            // if (coroutine != null) 
            // {
                // StopCoroutine(coroutine); // STOP

                // coroutine = StartCoroutine(ExtendObject(extendPoint1.gameObject));
                if(coroutine != null) 
                    StopCoroutine(coroutine);


                CreateObject(extandableObj);
                coroutine = StartCoroutine(ExtendObject(extendPoint));
            // }


            // CHECK IF SCALING IS STOPPED
            // if(!stopScalingCuzEndPointReached) 
            // {
                // isExpandingBack = false; // Need to set this otherwise it bugs when pressing the button down to fast
            // } 

            // return true;
        // }

        // return false;
    }

    public void ExtendBack() 
    {
        if(coroutine != null)
            StopCoroutine(coroutine);


        StartCoroutine(RetractObject(extandableObj, extendPoint));
    }

    // public void Teleport() 
    // {
    //     // stopScalingCuzEndPointReached = false;
    //     StartCoroutine(CollapseTowardsEndPoint(extendPoint2.gameObject, transform.localScale));
    // }

}