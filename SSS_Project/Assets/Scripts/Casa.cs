﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Casa : MonoBehaviour
{
    public GameObject ind;
    // Start is called before the first frame update
    void Start()
    {
        Instantiate(ind, transform.position, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
