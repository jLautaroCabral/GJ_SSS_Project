using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoverCamara : MonoBehaviour
{

    public float vel;
    public Vector3 startPosition;
    public float limites;
    void Start()
    {
        startPosition = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate( Vector3.right * vel , Space.World);
            
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(  Vector3.left * vel, Space.World);
        }

        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate( Vector3.back * vel , Space.World);
        }

        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate( Vector3.forward * vel, Space.World);
        }
        
        transform.position = new Vector3(
                Mathf.Clamp(transform.position.x, startPosition.x - limites, startPosition.x + limites),
                transform.position.y,
                Mathf.Clamp(transform.position.z, startPosition.z - limites, startPosition.z + limites)
            );
    }
}
