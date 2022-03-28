using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attack_mode : MonoBehaviour
{
    [Range(-2f, 4f)]    public float walkSpeed;

    public float seenEverySecond;

    void Start()
    {
        
    }

    void Update()
    {
        transform.position += Vector3.left * walkSpeed * Time.deltaTime;
    }

    public void Set_walk_speed(float passed_walkSpeed)
    {
        walkSpeed = passed_walkSpeed;
    }
}
