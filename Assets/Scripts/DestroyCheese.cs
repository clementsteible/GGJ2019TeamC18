﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyCheese : MonoBehaviour
{
    public float lifeTime = 20;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        lifeTime -= Time.deltaTime;
        if (lifeTime < 0) {
            Destroy(gameObject);
        }
    }
}
