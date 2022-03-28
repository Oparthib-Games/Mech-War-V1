using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class way_points_movement : MonoBehaviour
{
    public Vector3 random_vector_offset;

    public float way_point_movement_rate;
    float way_point_movement_time;

    void Start()
    {
        
    }

    //void Update()
    //{
    //    random_vector_offset = new Vector3(Random.Range(1f, -1f), Random.Range(1f, -1f), 0);

    //    transform.position = transform.position + random_vector_offset;

    //    Mathf.Clamp(transform.position.x, -0.04f, 0.04f);
    //    Mathf.Clamp(transform.position.y, -0.05f, 0.05f);
    //}

    void Update()
    {
        random_vector_offset = new Vector3(Random.Range(1f, -1f), Random.Range(1f, -1f), 0);

        if (Time.time >= way_point_movement_time)
        {
            way_point_movement_time += way_point_movement_rate;

            transform.position = transform.position + random_vector_offset;
        }
    }

}
