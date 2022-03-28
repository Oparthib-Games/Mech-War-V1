using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gear_script : MonoBehaviour
{
    GameObject[] all_enemies;
    public float range;
    public GameObject target = null;
    public GameObject top_part;
    public GameObject bullet_particle;
    public GameObject L_bullet_pos;
    public GameObject R_bullet_pos;
    public float damageAmount;
    Ray ray_origin_01;
    Ray ray_origin_02;
    RaycastHit hit_info;
    Animator anim;



    public LayerMask mask;
    public float ray_distance;

    void Start()
    {
        all_enemies = GameObject.FindGameObjectsWithTag("Enemy");
        anim = GetComponent<Animator>();

        InvokeRepeating("Shoot", 0, 1);
    }

    void FixedUpdate()
    {
        Finding_enemy();
        Facing_enemy();

        if (target = null) return;
    }

    void Finding_enemy()
    {
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

            if (shortest_distance <= range)
            {
                target = closest_enemy;
            }
            else
            {
                target = null;
            }
    }

    void Facing_enemy()
    {
       if(target != null)
        {
            Vector3 direction_to_look = top_part.transform.position - target.transform.position;
            Quaternion look_rot = Quaternion.LookRotation(direction_to_look);
            Vector3 quat2euler = look_rot.eulerAngles;
            Vector3 x = new Vector3(0f, quat2euler.y + 180, 0f);
            top_part.transform.rotation = Quaternion.Lerp(top_part.transform.rotation, Quaternion.Euler(x), Time.deltaTime * 5);
        }
    }

    void Shoot()
    {
        if (target != null)
        {
            anim.enabled = false;
            RayCast();
            GameObject L_bullet_particle_GO = (GameObject)Instantiate(bullet_particle, L_bullet_pos.transform.position, L_bullet_pos.transform.rotation);
            L_bullet_particle_GO.transform.parent = L_bullet_pos.transform;
            GameObject R_bullet_particle_GO = (GameObject)Instantiate(bullet_particle, R_bullet_pos.transform.position, R_bullet_pos.transform.rotation);
            R_bullet_particle_GO.transform.parent = R_bullet_pos.transform;
        }
        else anim.enabled = true;
    }

    void RayCast()
    {
        ray_origin_01 = new Ray(L_bullet_pos.transform.position, L_bullet_pos.transform.forward * ray_distance);
        ray_origin_02 = new Ray(R_bullet_pos.transform.position, R_bullet_pos.transform.forward * ray_distance);

        if (Physics.Raycast(ray_origin_01, out hit_info, ray_distance, mask) || Physics.Raycast(ray_origin_02, out hit_info, ray_distance, mask))
        {
            Debug.DrawLine(ray_origin_01.origin, ray_origin_01.direction, Color.red);
            health_script health_Script_access = hit_info.transform.GetComponent<health_script>();
            health_Script_access.dealDamage(damageAmount);
            print(health_Script_access.health);
        }
    }


    void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, range);
        Gizmos.DrawLine(L_bullet_pos.transform.position, L_bullet_pos.transform.position + L_bullet_pos.transform.forward * ray_distance);
        Gizmos.DrawLine(R_bullet_pos.transform.position, R_bullet_pos.transform.position + R_bullet_pos.transform.forward * ray_distance);
        Gizmos.color = Color.green;
    }
}
