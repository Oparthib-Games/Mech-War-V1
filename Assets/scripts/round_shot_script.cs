using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class round_shot_script : MonoBehaviour
{

    public GameObject spawnOrigin;

    public Vector3 veclocity;

    public float damageAmount;

    Rigidbody RB;

    public GameObject bullet_shatter_particle;

    void Start()
    {
        RB = GetComponent<Rigidbody>();

        veclocity = spawnOrigin.GetComponent<blue_tank_script>().Velocity_Calculation();

        RB.AddForce(veclocity, ForceMode.Impulse);
    }

    void OnTriggerEnter(Collider trig)
    {
        GameObject spawnAsGO = Instantiate(bullet_shatter_particle, transform.position, Quaternion.identity) as GameObject;

        trig.gameObject.GetComponent<health_script>().dealDamage(damageAmount);

        Destroy(gameObject);
    }
}
