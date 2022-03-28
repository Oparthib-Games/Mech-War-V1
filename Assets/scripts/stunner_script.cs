using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stunner_script : MonoBehaviour
{
    public Transform enemyCheckRayPos;
    Animator anim;
    public float range;
    public LayerMask enemyLayer;

    public GameObject electricity;
    public Transform L_electricity_pos;
    public Transform R_electricity_pos;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        EnemyCheck();
    }

    void Spawn_Electricity()
    {
        Quaternion L_electricity_rot = Quaternion.Euler(0, -75, 0);
        Quaternion R_electricity_rot = Quaternion.Euler(0, -105, 0);
        Instantiate(electricity, L_electricity_pos.position, L_electricity_rot, L_electricity_pos.parent);
        Instantiate(electricity, R_electricity_pos.position, R_electricity_rot, R_electricity_pos.parent);
    }

    void EnemyCheck()
    {
        Ray rayOrigin = new Ray(enemyCheckRayPos.position, enemyCheckRayPos.right * -1 * range);
        RaycastHit hitInfo;
        if (Physics.Raycast(rayOrigin, out hitInfo, range, enemyLayer)) anim.SetBool("isAttacking", true);
        else anim.SetBool("isAttacking", false);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawLine(enemyCheckRayPos.position, enemyCheckRayPos.position + Vector3.left * range);
    }
}
