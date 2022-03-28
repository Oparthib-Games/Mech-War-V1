using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy_script : MonoBehaviour
{

    public float destroy_time;

    public GameObject[] mines;

    public bool isShredder;

    void Update()
    {
        int i = 0;

        if((mines.Length == 0 || mines[i] == null) && !isShredder)
        {
            Destroy(gameObject, destroy_time);
            i++;
        }
    }

    void OnTriggerEnter(Collider trig)
    {
        if (isShredder)
        {
            Destroy(trig.gameObject);
        }
    }
}
