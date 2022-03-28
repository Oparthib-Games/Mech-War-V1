using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flame_script : MonoBehaviour
{
    public float normal_damage;
    public float special_damage;

    GameObject spider;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    void OnTriggerEnter(Collider trig)
    {
        trig.gameObject.GetComponent<health_script>().dealDamage(normal_damage);
        if (trig.gameObject.GetComponent<spider_script>())
        {
            trig.gameObject.GetComponent<health_script>().dealDamage(special_damage);
        }
        else
        {
            trig.gameObject.GetComponent<health_script>().dealDamage(normal_damage);
        }
    }
}
