using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class orange_titan_script : MonoBehaviour
{
    public Transform enemyCheckRayPos;
    Animator anim;
    public float range;
    public LayerMask enemyLayer;

    public GameObject laser;
    public Transform laser_pos;

    void Start()
    {
        anim = GetComponent<Animator>();

    }

    void Update()
    {
        EnemyCheck();

    }

    void EnemyCheck()
    {
        Ray rayOrigin = new Ray(enemyCheckRayPos.position, enemyCheckRayPos.right * -1 * range);
        RaycastHit hitInfo;
        if (Physics.Raycast(rayOrigin, out hitInfo, range, enemyLayer))
        {
            anim.SetBool("isAttacking", true);
        }
        else anim.SetBool("isAttacking", false);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawLine(enemyCheckRayPos.position, enemyCheckRayPos.position + Vector3.left * range);
    }

    void Fire_laser()
    {
        Instantiate(laser, laser_pos.position, Quaternion.Euler(0,-90,0), laser_pos.transform.parent);
    }
}
