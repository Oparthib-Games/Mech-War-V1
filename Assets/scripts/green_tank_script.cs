using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class green_tank_script : MonoBehaviour
{
    Animator anim;

    public Transform enemyCheckRayPos;
    public float range;
    public LayerMask enemyLayer;

    public GameObject canon;
    public GameObject projectile;
    public Transform projectile_pos;
    public GameObject muzzleFlash;
    public Transform muzzleFlashPos;

    void Start()
    {
        anim = GetComponent<Animator>();

        InvokeRepeating("EnemyCheck", 0, 0.5f);
    }

    void Fire()
    {
        Quaternion tank_proj_rot = Quaternion.Euler(0, 0, -90);
        Instantiate(projectile, projectile_pos.position, tank_proj_rot);
        Instantiate(muzzleFlash, muzzleFlashPos.position, Quaternion.identity);
    }

    void EnemyCheck()
    {
        Ray rayOrigin = new Ray(enemyCheckRayPos.position, Vector3.right); ;
        RaycastHit hitInfo;
        if (Physics.Raycast(rayOrigin, out hitInfo, range, enemyLayer)) anim.SetBool("isAttacking", true);
        else anim.SetBool("isAttacking", false);
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(enemyCheckRayPos.position, enemyCheckRayPos.position + Vector3.right * range);
    }
}
