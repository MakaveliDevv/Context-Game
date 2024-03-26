using UnityEngine;

public class InputController : Controller
{
   public bool ScaleInputt(GameObject _objectToScale, Vector2 _direction) 
    {
        if(_instantiateObj != null && !_player.isMoving) 
        {
            if (coroutine != null)
                StopCoroutine(coroutine);

            if(!stopScalingCuzEndPointReached) 
            {
                isExpandingBack = false; // Need to set this otherwise it bugs when pressing the button down to fast
                coroutine = StartCoroutine(Scaling(_objectToScale, _direction));
            } 

            return true;
        }

        return false;
    }

    protected void TeleportInput(GameObject _objectToScale2) 
    {
        // Move player to the endpoint
        stopScalingCuzEndPointReached = false;
        StartCoroutine(ExpandBackTowardsEndPoint(_objectToScale2, transform.localScale));
        timer = 0;
    }

    

    // public bool CheckIfSopScaling(GameObject __objectToScale) 
    // {
    //     if (stopScalingCuzEndPointReached) 
    //     {
    //         timer += Time.deltaTime; // Accumulate elapsed time

    //         if (timer >= timeUntilScaleBack) // Check if the timer has reached or exceeded the desired time
    //         {
    //             // Scale back
    //             coroutine = StartCoroutine(ScaleBack(_objectToScale, _objectToScale.transform.localScale, _scriptObj.initialScale));
    //             stopScalingCuzEndPointReached = false;

    //             if(Input.GetKeyDown(_keyCodeExtend)) // Press down to extend a bridge/ladder
    //             {
    //                 if(coroutine != null)
    //                     StopCoroutine(coroutine);

    //                 StartCoroutine(Scaling(_objectToScale, _direction)); 
    //                 // Debug.Log("Can scale from scaling back after freezing and time ran out");
    //                 return true;
    //             }

    //             // Reset the timer for the next iteration
    //             timer = 0;
    //             return true;

            
    //         } 
        
    //         else if(Input.GetKeyDown(_keyCodeScaleBackToEnd)) // To go to the end point (is for the artist)  && playerManag.playerType == PlayerManager.PlayerType.ARTIST
    //         {
    //             // Move player to the endpoint
    //             stopScalingCuzEndPointReached = false;
    //             StartCoroutine(ExpandBackTowardsEndPoint(_objectToScale2, transform.localScale));
    //             timer = 0;
    //             // Debug.Log("Can scale back from start to end point");
    //             return true;

    //         }
    //     }
    // }

    // public bool ScaleInput
    // (
    //     GameObject _objectToScale,
    //     GameObject _objectToScale2,
    //     Vector2 _direction, 
    //     KeyCode _keyCodeExtend, 
    //     KeyCode _keyCodeScaleBackToEnd,
    //     GameObject _instantiatedObj
    // ) 
    // {
    //     if(_instantiatedObj != null)
    //     {
    //         if (Input.GetKeyDown(_keyCodeExtend) && !_player.isMoving) // Press down to extend a bridge/ladder
    //         {
    //             // If scaling coroutine is running, stop it
    //             if (coroutine != null)
    //                 StopCoroutine(coroutine);

    //             if(!stopScalingCuzEndPointReached) 
    //             {
    //                 isExpandingBack = false; // Need to set this otherwise it bugs when pressing the button down to fast
    //                 coroutine = StartCoroutine(Scaling(_objectToScale, _direction));
    //                 // Debug.Log("Can scale");
    //                 return true;
    //             } 
                
    //         } else if (Input.GetKeyUp(_keyCodeExtend)) // Release to extend back
    //         {            
    //             // If scaling coroutine is running, stop it and start scaling back
    //             if (coroutine != null) 
    //                 StopCoroutine(coroutine);

    //             // If endpoint is detected cant scale back
    //             if(!stopScalingCuzEndPointReached) 
    //             {
    //                 isExpanding = false;
    //                 coroutine = StartCoroutine(ScaleBack(_objectToScale, _objectToScale.transform.localScale, _scriptObj.initialScale));
    //                 // Debug.Log("Can scale back");
    //                 return true;
    //             }
    //         }


    //         if (stopScalingCuzEndPointReached) 
    //         {
    //             timer += Time.deltaTime; // Accumulate elapsed time

    //             if (timer >= timeUntilScaleBack) // Check if the timer has reached or exceeded the desired time
    //             {
    //                 // Scale back
    //                 coroutine = StartCoroutine(ScaleBack(_objectToScale, _objectToScale.transform.localScale, _scriptObj.initialScale));
    //                 stopScalingCuzEndPointReached = false;

    //                 if(Input.GetKeyDown(_keyCodeExtend)) // Press down to extend a bridge/ladder
    //                 {
    //                     if(coroutine != null)
    //                         StopCoroutine(coroutine);

    //                     StartCoroutine(Scaling(_objectToScale, _direction)); 
    //                     // Debug.Log("Can scale from scaling back after freezing and time ran out");
    //                     return true;
    //                 }

    //                 // Reset the timer for the next iteration
    //                 timer = 0;
    //                 return true;

                
    //             } 
           
    //             else if(Input.GetKeyDown(_keyCodeScaleBackToEnd)) // To go to the end point (is for the artist)  && playerManag.playerType == PlayerManager.PlayerType.ARTIST
    //             {
    //                 // Move player to the endpoint
    //                 stopScalingCuzEndPointReached = false;
    //                 StartCoroutine(ExpandBackTowardsEndPoint(_objectToScale2, transform.localScale));
    //                 timer = 0;
    //                 // Debug.Log("Can scale back from start to end point");
    //                 return true;

    //             }
    //         }

    //     } else 
    //     {
    //         Debug.Log("Can't scale because no object in the scene");    
    //         return false;
    //     }
        
    //     return false;
    // }
}

     // else if(Input.GetKeyDown(_keyCodeRelease)) // Scale back to initial positon after reaching end point
                // {
                //     stopScalingCuzEndPointReached = false;
                //     coroutine = StartCoroutine(ScaleBack(_objectToScale, _objectToScale.transform.localScale, _scriptObj.initialScale));

                //     if(Input.GetKeyDown(_keyCodeExtend)) // Release to extend back
                //     {
                //         if(coroutine != null)
                //             StopCoroutine(coroutine);

                //         StartCoroutine(Scaling(_objectToScale, _direction)); 
                //         // Debug.Log("Can scale from scaling back after freezing and pressing scale back button");
                //         return true;

                //     }

                //     // Reset the timer for the next iteration
                //     timer = 0;
                //     return true;

                        
                // } 