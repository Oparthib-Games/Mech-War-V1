using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mouseCtrl : MonoBehaviour
{
    public float camera_move_speed = 10f;
    public float RTS_screen_offset = 10;

    public float minPosX = 13;
    public float maxPosX = 52;
    public float min_max_PosY = 52;
    public float min_max_PosZ = -20;

    Vector3 cameraPos;
    Vector3 mousePos;

    void Start()
    {
        minPosX = transform.position.x;
        min_max_PosY = transform.position.y;
        min_max_PosZ = transform.position.z;
    }

    void Update()
    {
        cameraPos = this.transform.position;


        if (Input.mousePosition.x >= Screen.width - RTS_screen_offset)
        {
            cameraPos.x += camera_move_speed * Time.deltaTime;
        }
        if(Input.mousePosition.x <= RTS_screen_offset)
        {
            cameraPos.x -= camera_move_speed * Time.deltaTime;
        }
        cameraPos.x = Mathf.Clamp(cameraPos.x, minPosX, maxPosX);
        cameraPos.y = Mathf.Clamp(cameraPos.y, min_max_PosY, min_max_PosY);
        cameraPos.z = Mathf.Clamp(cameraPos.z, min_max_PosZ, min_max_PosZ);

        transform.position = cameraPos;
    }
}
