using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mOVEMENT : MonoBehaviour {

    Rigidbody body;

    void Awake()
    {
        body = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            body.MovePosition(transform.position + transform.forward * 1f * Time.deltaTime); 
        }
    }
}
