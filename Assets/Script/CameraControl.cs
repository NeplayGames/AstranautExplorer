using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    [SerializeField] Transform player;

    Vector3 offset;
    void Start()
    {
       // offset = transform.position - new Vector3(player.position.x, 0);
        offset = transform.position - player.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
       // transform.position =new Vector3( player.position.x,0) + offset;
        transform.position =player.position + offset;
    }
}
