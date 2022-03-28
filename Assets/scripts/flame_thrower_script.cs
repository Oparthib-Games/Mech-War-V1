using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flame_thrower_script : MonoBehaviour
{
    public GameObject[] all_enemies;
    public GameObject cooker_pivot;
    public GameObject target;
    public float range;

    public GameObject flame;
    public Transform flame_pos;

    Animator anim;
    AudioSource audioSORS;

    void Start()
    {
        anim = GetComponent<Animator>();
        audioSORS = GetComponent<AudioSource>();
    }

    void Update()
    {
        Finding_Enemy();
        Facing_enemy();
    }

    void Finding_Enemy()
    {
        all_enemies = GameObject.FindGameObjectsWithTag("Enemy");

        float shortest_distance = Mathf.Infinity;
        GameObject closest_enemy = null;

        foreach (GameObject theEnemy in all_enemies)
        {
            float distance = Vector3.Distance(transform.position, theEnemy.transform.position);
            if (distance < shortest_distance)
            {
                shortest_distance = distance;
                closest_enemy = theEnemy;
            }
        }
        if (closest_enemy != null && shortest_distance <= range)
        {
            target = closest_enemy;
            anim.SetBool("isAttacking", true);
        }
        else
        {
            target = null;
            anim.SetBool("isAttacking", false);
        }
    }
    void Facing_enemy()
    {
        Vector3 direction_to_look = cooker_pivot.transform.position - target.transform.position;
        Quaternion look_rot = Quaternion.LookRotation(direction_to_look);
        Vector3 quat2euler = look_rot.eulerAngles;
        Vector3 x = new Vector3(0f, quat2euler.y + 180, 0f);

        cooker_pivot.transform.rotation = Quaternion.Lerp(cooker_pivot.transform.rotation, Quaternion.Euler(x), Time.deltaTime * 3.5f);
    }

    void throw_flame()
    {
        audioSORS.Play();
        GameObject spawn_as_GO = (GameObject)Instantiate(flame, flame_pos.position, flame_pos.rotation, flame_pos.parent);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, range);
    }


}
