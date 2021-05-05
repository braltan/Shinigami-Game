using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackPlayer : MonoBehaviour
{
    [SerializeField]
    private GameObject player;
    float y;
 
    void Start()
    {
        y = transform.position.y;
    }

    void Update()
    {
        transform.position = new Vector3(player.transform.position.x-4.0f,
            y+player.transform.position.y,
            transform.position.z);
    }
}
