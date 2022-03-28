using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blue_tank_script : MonoBehaviour
{
    Animator anim;
    public Transform enemyCheckRayPos;
    public float range;
    public LayerMask enemyLayer;

    public GameObject roundShots;
    public Transform roundShots_pos;

    public Transform target;
    public float x, y, t, g, Vx, Vy, Vx_offset;
    public Vector3 V;

    void Start()
    {
        anim = GetComponent<Animator>();
        InvokeRepeating("EnemyCheck", 0, 0.5f);

        Velocity_Calculation();
    }

    void Update()
    {
        
    }

    void EnemyCheck()
    {
        Ray rayOrigin = new Ray(enemyCheckRayPos.position, Vector3.right); ;
        RaycastHit hitInfo;
        if (Physics.Raycast(rayOrigin, out hitInfo, range, enemyLayer)) { 
            anim.SetBool("isAttacking", true);
            target = hitInfo.transform;
        } else anim.SetBool("isAttacking", false);
    }
    void Fire()
    {
        GameObject roundShot_as_GO = (GameObject)Instantiate(roundShots, roundShots_pos.position, Quaternion.identity);
        roundShot_as_GO.GetComponent<round_shot_script>().spawnOrigin = gameObject;
    }

    void OnDrawGizmos() {
        Gizmos.color = Color.cyan;
        Gizmos.DrawLine(enemyCheckRayPos.position, enemyCheckRayPos.position + enemyCheckRayPos.right * range);
    }

    public Vector3 Velocity_Calculation()
    {
        x = target.position.x - roundShots_pos.position.x;
        g = Physics.gravity.y * -1;

        Vx = x / t;
        Vy = y / t + 0.5f * g * t;

        if (x < 30) { t = 2f; }
        else { Vx_offset = 2.8f; }

        V = new Vector3(Vx - Vx_offset, Vy, 0);

        return V;
    }

}
