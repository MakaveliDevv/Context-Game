using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    private Vector3 cameraPosition;
    public GameObject player;
    public float cameraSpeed;
    
    void Update()
    {
        FollowPlayer();
    }

    private void SetPosition(Vector2 _position) 
    {
        transform.position = _position;
    }

    private void FollowPlayer() 
    {
        if(player != null) 
        {
            Vector3 targetPosition = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z - 10f);
            transform.position = Vector3.Lerp(transform.position, targetPosition, cameraSpeed * Time.deltaTime);
        }
    }

}
