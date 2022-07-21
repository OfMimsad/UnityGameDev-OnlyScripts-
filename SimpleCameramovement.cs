using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleCameramovement : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] float CameraSpeed;
    [SerializeField] float AheadDistance;
    float lookAhead;
    
   
    void Update()
    {
        transform.position = new Vector3(player.position.x + lookAhead, transform.position.y, transform.position.z);
        lookAhead = Mathf.Lerp(lookAhead, (AheadDistance * player.localScale.x), CameraSpeed * Time.deltaTime);

    }
}
