using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testing_Script : MonoBehaviour
{
    public float speed = 0.5f;

    void Update()
    {
        transform.Translate(Vector3.left * speed);
    }
}
