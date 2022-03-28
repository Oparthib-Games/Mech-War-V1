using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class double_gear_Ctrl : MonoBehaviour
{
    public GameObject[] all_enemies;
    public GameObject top_pivot;
    public GameObject target;
    public float range;

    public GameObject L_bullet_pos;
    public GameObject R_bullet_pos;
    Ray ray_origin_01;
    Ray ray_origin_02;
    RaycastHit hit_info;
    public LayerMask mask;

    public GameObject bullet_particle;

    Animator anim;
    AudioSource audioSORS;

    public float damageAmount;

    public bool isStunned;

    void Start()
    {
        anim = GetComponent<Animator>();
        audioSORS = GetComponent<AudioSource>();

        InvokeRepeating("Shoot", 0, 1);
    }

    void FixedUpdate()
    {
        Finding_Enemy();
        Facing_enemy();
        if (target == null) return;
    }

//=======================================================Find Enemy========================================================

    void Finding_Enemy()
    {
        all_enemies = GameObject.FindGameObjectsWithTag("Enemy");

        float shortest_distance = Mathf.Infinity;
        GameObject closest_enemy = null;

        foreach(GameObject theEnemy in all_enemies)
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
        }
        else
        {
            target = null;
        }
    }

//=========================================================Facing=======================================================

    void Facing_enemy()
    {
        Vector3 direction_to_look = top_pivot.transform.position - target.transform.position;
        Quaternion look_rot = Quaternion.LookRotation(direction_to_look);
        Vector3 quat2euler = look_rot.eulerAngles;
        Vector3 x = new Vector3(0f, quat2euler.y + 180, 0f);
        top_pivot.transform.rotation = Quaternion.Lerp(top_pivot.transform.rotation, Quaternion.Euler(x), Time.deltaTime * 5);
    }

//=======================================================RayCast========================================================

    void RayCast()
    {
        ray_origin_01 = new Ray(L_bullet_pos.transform.position, L_bullet_pos.transform.forward * range);
        ray_origin_02 = new Ray(R_bullet_pos.transform.position, R_bullet_pos.transform.forward * range);

        if (Physics.Raycast(ray_origin_01, out hit_info, range, mask) || Physics.Raycast(ray_origin_02, out hit_info, range, mask))
        {
            health_script health_Script_access = hit_info.transform.GetComponent<health_script>();
            health_Script_access.dealDamage(damageAmount);
        }
    }

    //=======================================================Shoot========================================================

    void Shoot()
    {
        if (target != null && !isStunned)
        {
            anim.SetBool("isAttacking", true);

            RayCast();
            GameObject L_bullet_particle_GO = (GameObject)Instantiate(bullet_particle, L_bullet_pos.transform.position, L_bullet_pos.transform.rotation);
            L_bullet_particle_GO.transform.parent = L_bullet_pos.transform;
            GameObject R_bullet_particle_GO = (GameObject)Instantiate(bullet_particle, R_bullet_pos.transform.position, R_bullet_pos.transform.rotation);
            R_bullet_particle_GO.transform.parent = R_bullet_pos.transform;
        }
        else anim.SetBool("isAttacking", false);
    }

    //=======================================================Gizmos========================================================

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
        Gizmos.color = Color.gray;
        Gizmos.DrawLine(L_bullet_pos.transform.position, L_bullet_pos.transform.position + L_bullet_pos.transform.forward * range);
        Gizmos.DrawLine(R_bullet_pos.transform.position, R_bullet_pos.transform.position + R_bullet_pos.transform.forward * range);
    }

    void OnTriggerEnter(Collider trig)
    {
        if (trig.gameObject.tag == "electricity")
        {
            isStunned = true;
        }
        else
        {
            isStunned = false;
        }
    }

    public void audio_play()
    {
        audioSORS.Play();
    }
}
