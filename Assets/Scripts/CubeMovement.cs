using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeMovement : MonoBehaviour
{
    private Rigidbody Rigidbodyrb;
    public float Speed = 5f;
    void Start()
    {
        Rigidbodyrb = GetComponent<Rigidbody>();
    }


    void Update()
    {
        transform.Translate(Vector3.forward * Speed * Time.deltaTime);

    }
}
