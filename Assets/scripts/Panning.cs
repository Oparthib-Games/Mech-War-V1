using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Panning : MonoBehaviour
{
    Vector3 touch_start_pos;
    //public static float panning_time = 0f;
    public static bool isPanning = false;

    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            /*** touch_start_pos = Camera.main.ScreenToWorldPoint(Input.mousePosition); */
            touch_start_pos = Get_World_Pos();
        }
        else if(Input.GetMouseButton(0))
        {
            /*** Camera.main.transform.position += touch_start_pos - Camera.main.ScreenToWorldPoint(Input.mousePosition); */

            //panning_time += 1f;

            if (touch_start_pos - Get_World_Pos() != new Vector3(0.0f, 0.0f, 0.0f))
            {
                isPanning = true;
            }
            else
            {
                isPanning = false;
            }

            Camera.main.transform.position += touch_start_pos - Get_World_Pos();
        }
        else if(Input.GetMouseButtonUp(0))
        {
            isPanning = false;
            //panning_time = 0f;
        }
    }

    Vector3 Get_World_Pos()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane xPlane = new Plane(Camera.main.transform.forward * -1, Vector3.zero);
        float distance;
        xPlane.Raycast(ray, out distance);
        return ray.GetPoint(distance);
    }
}
