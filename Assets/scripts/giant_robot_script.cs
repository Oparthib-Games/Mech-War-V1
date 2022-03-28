using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class giant_robot_script : MonoBehaviour
{
    public GameObject bullet;
    public Transform bullet_pos;
    public GameObject canon_bullet;
    public Transform canon_pos;

    public Transform enemyCheckRayPos;
    Animator anim;
    AudioSource audioSORS;
    public float range;
    public LayerMask enemyLayer;

    void Start()
    {
        anim = GetComponent<Animator>();
        audioSORS = GetComponent<AudioSource>();
        //InvokeRepeating("EnemyCheck", 0, 0.5f);
    }

    void FixedUpdate()
    {
        EnemyCheck();
    }

    void EnemyCheck()
    {
        Ray rayOrigin = new Ray(enemyCheckRayPos.position, enemyCheckRayPos.right * -1 * range);
        RaycastHit hitInfo; 
        if (Physics.Raycast(rayOrigin, out hitInfo, range, enemyLayer)) anim.SetBool("isAttacking", true);
        else anim.SetBool("isAttacking", false);
    }

    public void Laser_Shoot()
    {
        audioSORS.Play();
        GameObject spawn_as_GO = (GameObject)Instantiate(bullet, bullet_pos.position, Quaternion.identity);

    }
    public void canon_shot()
    {
        audioSORS.Play();
        GameObject spawn_as_GO_02 = (GameObject)Instantiate(canon_bullet, canon_pos.position, Quaternion.identity);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(enemyCheckRayPos.position, enemyCheckRayPos.position + Vector3.left * range);
    }


}
