﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    public float speed;
    public Vector3 randV;

    private void Start()
    {
        GetComponent<Rigidbody>().velocity = transform.forward * speed;
    }
}
