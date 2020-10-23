using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CicloDiayNoche : MonoBehaviour
{
    [SerializeField]
    private float tumble;
   
   


    void Start()
    {
     
    }

    private void Update()
    {
        transform.Rotate(Vector3.left, tumble, Space.World);

    }

}
